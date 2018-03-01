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
    public class MerkenController : ApiController
    {
        private HenrikMotorsContext db = new HenrikMotorsContext();

        // GET: api/Merken
        public IQueryable<MerkDTO> GetMerken()
        {
            var merken = db.Merken.ProjectTo<MerkDTO>();
            return merken;
        }

        // GET: api/Merken/5
        [ResponseType(typeof(MerkDTO))]
        public async Task<IHttpActionResult> GetMerk(int id)
        {
            var merk = await db.Merken
                .ProjectTo<MerkDTO>().SingleOrDefaultAsync(m => m.Id == id);
            if (merk == null)
            {
                return NotFound();
            }

            return Ok(merk);
        }

        // PUT: api/Merken/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMerk(int id, MerkDTO merkDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var merk = Mapper.Map<Merk>(merkDTO);
            db.Set<Merk>().Attach(merk);
            db.Entry(merk).State = EntityState.Modified;

            return Ok(merkDTO);
        }

        // POST: api/Merken
        [ResponseType(typeof(MerkDTO))]
        public async Task<IHttpActionResult> PostMerk(MerkDTO merkDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var merk = Mapper.Map<Merk>(merkDTO);
            db.Merken.Add(merk);
            await db.SaveChangesAsync();
            merkDTO.Id = merk.Id;
            return Ok(merkDTO);
        }

        // DELETE: api/Merken/5
        [ResponseType(typeof(Merk))]
        public async Task<IHttpActionResult> DeleteMerk(int id)
        {
            Merk merk = await db.Merken.FindAsync(id);
            if (merk == null)
            {
                return NotFound();
            }

            db.Merken.Remove(merk);
            await db.SaveChangesAsync();

            return Ok(merk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MerkExists(int id)
        {
            return db.Merken.Count(e => e.Id == id) > 0;
        }
    }
}