using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HenrikMotors.Models;
using HenrikMotors.Areas.Admin.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace HenrikMotors.Areas.Admin.Controllers
{
    public class VoertuigenController : ApiController
    {
        //int CurrentId;
        private HenrikMotorsContext db = new HenrikMotorsContext();
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
            db.Entry(voertuig).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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
        Task<int> t1;
        // POST: api/Voertuigen
        [System.Web.Http.Route("api/voertuigen/PostVoertuig")]
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
            db.Voertuigen.Add(voertuig);
            await db.SaveChangesAsync();
            voertuigDetailDTO.Id = voertuig.Id;

            return Ok(voertuigDetailDTO);
        }

        [System.Web.Http.Route("api/voertuigen/PostFile/{id}/{length}")]
        public async Task<HttpResponseMessage> PostFile(int id,int length)
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/images/Voertuig" + id);

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            while (length > 0)
            {
                await NewFoto(id, "1");
                length--;
            }
            var provider = new CustomMultipartFormDataStreamProvider(root, fotoList);

            await Request.Content.ReadAsMultipartAsync(provider);
            var lol = provider.FileData.Count;
            foreach (MultipartFileData file in provider.FileData)
            {
                //await NewFoto(id, "1");
                string file1 = provider.FileData.First().LocalFileName;
            }
            fotoList.Clear();
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{ }", Encoding.UTF8, "application/json");
            return response;
        }

        // DELETE: api/Voertuigen/5
        [ResponseType(typeof(Voertuig))]
        public async Task<IHttpActionResult> DeleteVoertuig(int id)
        {
            Voertuig voertuig = await db.Voertuigen.FindAsync(id);
            if (voertuig == null)
            {
                return NotFound();
            }

            db.Voertuigen.Remove(voertuig);
            await db.SaveChangesAsync();

            return Ok(voertuig);
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
        public List<string> fotoList;
        async Task NewFoto(int id, string order2)
        {
            Voertuig currentVoertuig = await db.Voertuigen.FindAsync(id);
            if (string.IsNullOrEmpty(currentVoertuig.LijstFotos))
                fotoList = new List<string>();
            else
                fotoList = currentVoertuig.LijstFotos.Split(',').ToList();

            int order = fotoList.Count+1;

            string Foto = $"{order}_{currentVoertuig.ArtikelNummer}";
            fotoList.Add(Foto);
            currentVoertuig.LijstFotos = string.Join(",", fotoList);
            db.Set<Voertuig>().Attach(currentVoertuig);
            db.Entry(currentVoertuig).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<int> CurrentId(int id)
        {
            await Task.Delay(0);
            return id;
        }

        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            List<string> CustomFileName;
            int index = 1;
            public CustomMultipartFormDataStreamProvider(string path, List<string> Voertuig) : base(path)
            {
                CustomFileName = Voertuig;
            }

            public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                //string Extension = headers.ContentDisposition.FileName.Trim('\"');
                string OriginalFileName = $"{CustomFileName[CustomFileName.Count - index]}.png";
                index++;
                return OriginalFileName;
            }
        }
    }
}