using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HenrikMotors.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Text;
using System.IO;
using System.Web;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using ChangeFileName;

namespace HenrikMotors.Areas.Admin.Controllers
{
    public class VoertuigenController : ApiController
    {
        //int CurrentId;
        private HenrikMotorsContext db = new HenrikMotorsContext();
        static int currentId;
        // GET: api/Voertuigen
        public IQueryable<VoertuigDTO> GetVoertuigen()
        {
            IQueryable<VoertuigDTO> voertuigen = db.Voertuigen.ProjectTo<VoertuigDTO>();

            return voertuigen;
        }

        // GET: api/Voertuigen/5
        [ResponseType(typeof(VoertuigDetailDTO))]
        public async Task<IHttpActionResult> GetVoertuig(int id)
        {
            VoertuigDetailDTO voertuigDetailDTO = await db.Voertuigen
                .ProjectTo<VoertuigDetailDTO>().SingleOrDefaultAsync(v => v.Id == id);

            if (voertuigDetailDTO == null)
            {
                return NotFound();
            }

            return Ok(voertuigDetailDTO);
        }

        // PUT: api/Voertuigen/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVoertuig(int id, VoertuigDetailDTO voertuigDetailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (voertuigDetailDTO.MerkId == 0 || voertuigDetailDTO.CarrosserietypeId == 0)
            {
                await NewCategorie(voertuigDetailDTO);
            }


            Voertuig voertuig = Mapper.Map<Voertuig>(voertuigDetailDTO);
            db.Set<Voertuig>().Attach(voertuig);
            await ChangeUitrusting(voertuig, voertuigDetailDTO.VoertuigUitrusting);
            db.Entry(voertuig).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                currentId = voertuig.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoertuigExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(voertuigDetailDTO);
        }

        // POST: api/Voertuigen
        [Authorize]
        [Route("api/voertuigen/PostVoertuig")]
        [ResponseType(typeof(Voertuig))]
        public async Task<IHttpActionResult> PostVoertuig(VoertuigDetailDTO voertuigDetailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (voertuigDetailDTO.MerkId == 0 || voertuigDetailDTO.CarrosserietypeId == 0)
            {
                await NewCategorie(voertuigDetailDTO);
            }
            Voertuig voertuig = Mapper.Map<Voertuig>(voertuigDetailDTO);
            voertuig.VoertuigUitrusting = null;
            db.Voertuigen.Add(voertuig);
            await db.SaveChangesAsync();

            voertuigDetailDTO.Id = voertuig.Id;
            voertuigDetailDTO.ArtikelNummer = voertuig.ArtikelNummer;
            currentId = voertuig.Id;

            var voertuigcopy = Mapper.Map<Voertuig>(voertuigDetailDTO);
            voertuigcopy.Id = voertuig.Id;
            await ChangeUitrusting(voertuigcopy, voertuigDetailDTO.VoertuigUitrusting);
            await db.SaveChangesAsync();

            return Ok(voertuigDetailDTO);
        }
        [Route("api/voertuigen/PostFile/{length}")]
        public async Task<HttpResponseMessage> PostFile(int length)
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/images/Voertuig" + currentId);

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            while (length > 0)
            {
                await NewFoto(currentId, "1");
                length--;
            }
            var provider = new CustomMultipartFormDataStreamProvider(root, fotoList);

            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (MultipartFileData file in provider.FileData)
            {
                //await NewFoto(id, "1");
                string file1 = provider.FileData.First().LocalFileName;
            }
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{ }", Encoding.UTF8, "application/json");
            return response;
        }

        // DELETE: api/Voertuigen/5
        [Authorize]
        [ResponseType(typeof(Voertuig))]
        public async Task<IHttpActionResult> DeleteVoertuig(int id)
        {
            Voertuig voertuig = await db.Voertuigen.FindAsync(id);
            if (voertuig == null)
            {
                return NotFound();
            }
            string Fotomap = HttpContext.Current.Server.MapPath("~/Content/images/Voertuig" + id);
            if (Directory.Exists(Fotomap))
            {
                Directory.Delete(Fotomap, true);
            }

            db.Voertuigen.Remove(voertuig);
            await db.SaveChangesAsync();

            return Ok(voertuig);
        }
        [HttpPost]
        [Route("api/voertuigen/DeleteFile")]
        public async Task<HttpResponseMessage> DeleteFile(FormDataCollection formData)
        {
            string foto = formData["fileName"];
            int voertuigid = Convert.ToInt32(formData["id"]);
            string Fotomap = HttpContext.Current.Server.MapPath($"~/Content/images/Voertuig{voertuigid}/{foto}.png");
            DirectoryInfo directoryInfo = new DirectoryInfo(Fotomap);
            if (File.Exists(Fotomap))
            {
                await DeleteFoto(voertuigid, foto);
                File.Delete(Fotomap);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{ }", Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("api/voertuigen/ChangeFileName")]
        public async Task<HttpResponseMessage> ChangeFileName(List<SortFileName> sortFileName)
        {
            int Id = sortFileName[0].Extra.Id;
            List<string> Temporary = new List<string>();
            List<int> NewPosition = new List<int>();
            string Fotomap = HttpContext.Current.Server.MapPath($"~/Content/images/Voertuig{Id}/");

            for (int i = 0; i < sortFileName.Count; i++)
            {
                int Position = i + 1;
                if (Position != sortFileName[i].Key)
                {
                    string OldFileName = $"{Fotomap}{sortFileName[i].Filename}";
                    string tem = $"{Fotomap}{Position}_Ghost_{sortFileName[i].Filename}";
                    NewPosition.Add(Position);
                    Temporary.Add(tem);
                    File.Move(OldFileName, tem);
                }

            }
            for (int i = 0; i < Temporary.Count; i++)
            {
                string NewFileName = $"{Fotomap}{NewPosition[i]}_HMPV-{Id}.png";
                File.Move(Temporary[i], NewFileName);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{ }", Encoding.UTF8, "application/json");
            return response;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoertuigExists(int id)
        {
            return db.Voertuigen.Count(e => e.Id == id) > 0;
        }

        async Task NewCategorie(VoertuigDetailDTO voertuigDetailDTO)
        {
            if (voertuigDetailDTO.MerkId == 0)
            {
                MerkDTO merkDTO = new MerkDTO
                {
                    Id = 1,
                    Naam = voertuigDetailDTO.MerkNaam
                };
                var merk = Mapper.Map<Merk>(merkDTO);
                db.Merken.Add(merk);
                await db.SaveChangesAsync();
                voertuigDetailDTO.MerkId = merk.Id;
            }

            if (voertuigDetailDTO.CarrosserietypeId == 0)
            {
                CarrosserietypeDTO carrosserietypeDTO = new CarrosserietypeDTO
                {
                    Id = 1,
                    Naam = voertuigDetailDTO.CarrosserietypeNaam
                };
                var carrosserietype = Mapper.Map<Carrosserietype>(carrosserietypeDTO);
                db.Carrosserietypes.Add(carrosserietype);
                await db.SaveChangesAsync();
                voertuigDetailDTO.CarrosserietypeId = carrosserietype.Id;
            }
        }

        async Task ChangeUitrusting(Voertuig voertuig, ICollection<VoertuigUitrustingDTO> voertuigUitrustingDTO)
        {
            //Toevoegen
            List<VoertuigUitrusting> UitrustingCompare = new List<VoertuigUitrusting>();
            for (int i = 0; i < voertuig.VoertuigUitrusting.Count; i++)
            {
                var CurrentUitr = voertuig.VoertuigUitrusting.ElementAtOrDefault(i);
                if (CurrentUitr.UitrustingId <= 0)
                {
                    CurrentUitr = await NewUitrusting(voertuigUitrustingDTO.
                        First(nu => nu.UitrustingId == CurrentUitr.UitrustingId),voertuig.Id);
                }
                var ExistsInDb = db.VoertuigUitrusting.FirstOrDefault(vu => vu.UitrustingId == CurrentUitr.UitrustingId
                && vu.VoertuigId == voertuig.Id);
                if (ExistsInDb != null)
                    UitrustingCompare.Add(ExistsInDb);
                if (ExistsInDb?.UitrustingId == null)
                {
                    CurrentUitr.VoertuigId = voertuig.Id;
                    db.VoertuigUitrusting.Add(CurrentUitr);
                    UitrustingCompare.Add(CurrentUitr);
                }
            }

            //Verwijderen
            List<VoertuigUitrusting> UitrustingToDelete = db.VoertuigUitrusting.Where(vu => vu.VoertuigId == voertuig.Id).ToList();
            for (var i = 0; i < UitrustingToDelete.Count; i++)
            {
                if (UitrustingCompare.FindIndex(u => u.UitrustingId.Equals(UitrustingToDelete[i].UitrustingId)) == -1)
                {
                    db.VoertuigUitrusting.Remove(UitrustingToDelete[i]);
                }
            }
        }
        async Task<VoertuigUitrusting> NewUitrusting(VoertuigUitrustingDTO voertuigUitrustingDTO, int id)
        {
            Uitrusting uitrusting = new Uitrusting
            {
                Id = 1,
                Naam = voertuigUitrustingDTO.UitrustingNaam,
                Categorie = 4
            };
            db.Uitrustingen.Add(uitrusting);
            await db.SaveChangesAsync();
            voertuigUitrustingDTO.UitrustingId = uitrusting.Id;
            voertuigUitrustingDTO.UitrustingNaam = uitrusting.Naam;
            //voertuigUitrustingDTO.VoertuigId = id;
            var voertuigUitrusting = Mapper.Map<VoertuigUitrusting>(voertuigUitrustingDTO);
            return voertuigUitrusting;
        }

        public List<string> fotoList;
        async Task NewFoto(int id, string order2)
        {
            Voertuig currentVoertuig = await db.Voertuigen.FindAsync(id);
            if (string.IsNullOrEmpty(currentVoertuig.LijstFotos))
                fotoList = new List<string>();
            else
                fotoList = currentVoertuig.LijstFotos.Split(',').ToList();

            int order = fotoList.Count + 1;

            string Foto = $"{order}_{currentVoertuig.ArtikelNummer}";
            fotoList.Add(Foto);
            currentVoertuig.LijstFotos = string.Join(",", fotoList);
            db.Set<Voertuig>().Attach(currentVoertuig);
            db.Entry(currentVoertuig).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        async Task DeleteFoto(int id, string naam)
        {
            Voertuig currentVoertuig = await db.Voertuigen.FindAsync(id);
            fotoList = currentVoertuig.LijstFotos.Split(',').ToList();
            //var currentItem = fotoList.IndexOf(naam);
            fotoList.RemoveAt(fotoList.Count - 1);
            currentVoertuig.LijstFotos = string.Join(",", fotoList);
            db.Set<Voertuig>().Attach(currentVoertuig);
            db.Entry(currentVoertuig).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            List<string> CustomFileName;
            int index = 1;
            public CustomMultipartFormDataStreamProvider(string path, List<string> Voertuig) : base(path)
            {
                CustomFileName = Voertuig;
            }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                //string Extension = headers.ContentDisposition.FileName.Trim('\"');
                string OriginalFileName = $"{CustomFileName[CustomFileName.Count - index]}.png";
                index++;
                return OriginalFileName;
            }
        }
    }
}