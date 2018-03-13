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
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace HenrikMotors.Areas.Admin.Controllers
{
    public class UitrustingenController : ApiController
    {
        private HenrikMotorsContext db = new HenrikMotorsContext();

        // GET: api/Uitrustingen
        public IQueryable<UitrustingDTO> GetUitrustingen()
        {
            var uitrustingen = db.Uitrustingen.ProjectTo<UitrustingDTO>();
            return uitrustingen;
        }

        // GET: api/Uitrustingen/5
        [ResponseType(typeof(Uitrusting))]
        public async Task<IHttpActionResult> GetUitrusting(int id)
        {
            var uitrusting = await db.Uitrustingen
               .ProjectTo<UitrustingDTO>().SingleOrDefaultAsync(u => u.Id == id);
            if (uitrusting == null)
            {
                return NotFound();
            }

            return Ok(uitrusting);
        }

        // PUT: api/Uitrustingen/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUitrusting(int id, UitrustingDTO uitrustingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uitrustingDTO.Id)
            {
                return BadRequest();
            }

            var uitrusting = Mapper.Map<Uitrusting>(uitrustingDTO);
            db.Set<Uitrusting>().Attach(uitrusting);
            db.Entry(uitrusting).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UitrustingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Uitrustingen
        [ResponseType(typeof(Uitrusting))]
        public async Task<IHttpActionResult> PostUitrusting(UitrustingDTO uitrustingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uitrusting = Mapper.Map<Uitrusting>(uitrustingDTO);
            db.Uitrustingen.Add(uitrusting);
            await db.SaveChangesAsync();
            uitrustingDTO.Id = uitrusting.Id;
            return Ok(uitrustingDTO);
        }

        // DELETE: api/Uitrustingen/5
        [ResponseType(typeof(Uitrusting))]
        public async Task<IHttpActionResult> DeleteUitrusting(int id)
        {
            Uitrusting uitrusting = await db.Uitrustingen.FindAsync(id);
            if (uitrusting == null)
            {
                return NotFound();
            }

            db.Uitrustingen.Remove(uitrusting);
            await db.SaveChangesAsync();

            return Ok(uitrusting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UitrustingExists(int id)
        {
            return db.Uitrustingen.Count(e => e.Id == id) > 0;
        }
    }
}