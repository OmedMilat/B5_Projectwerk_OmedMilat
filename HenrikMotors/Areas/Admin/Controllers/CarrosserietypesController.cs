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
    public class CarrosserietypesController : ApiController
    {
        private HenrikMotorsContext db = new HenrikMotorsContext();

        // GET: api/Carrosserietypes
        public IQueryable<CarrosserietypeDTO> GetCarrosserietypes()
        {
            var carrosserietypes = db.Carrosserietypes.ProjectTo<CarrosserietypeDTO>();
            return carrosserietypes;
        }

        // GET: api/Carrosserietypes/5
        [ResponseType(typeof(CarrosserietypeDTO))]
        public async Task<IHttpActionResult> GetCarrosserietype(int id)
        {
            var carrosserietype = await db.Carrosserietypes
                .ProjectTo<CarrosserietypeDTO>().SingleOrDefaultAsync(c => c.Id == id);

            if (carrosserietype == null)
            {
                return NotFound();
            }

            return Ok(carrosserietype);
        }

        // PUT: api/Carrosserietypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCarrosserietype(int id, CarrosserietypeDTO carrosserietypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carrosserietype = Mapper.Map<Carrosserietype>(carrosserietypeDTO);
            db.Set<Carrosserietype>().Attach(carrosserietype);                        
            db.Entry(carrosserietype).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrosserietypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(carrosserietypeDTO);
        }

        // POST: api/Carrosserietypes
        [ResponseType(typeof(CarrosserietypeDTO))]
        public async Task<IHttpActionResult> PostCarrosserietype(CarrosserietypeDTO carrosserietypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carrosserietype = Mapper.Map<Carrosserietype>(carrosserietypeDTO);
            db.Carrosserietypes.Add(carrosserietype);
            await db.SaveChangesAsync();
            carrosserietypeDTO.Id = carrosserietype.Id;

            return Ok(carrosserietype);
        }

        // DELETE: api/Carrosserietypes/5
        [ResponseType(typeof(Carrosserietype))]
        public async Task<IHttpActionResult> DeleteCarrosserietype(int id)
        {
            Carrosserietype carrosserietype = await db.Carrosserietypes.FindAsync(id);
            if (carrosserietype == null)
            {
                return NotFound();
            }

            db.Carrosserietypes.Remove(carrosserietype);
            await db.SaveChangesAsync();

            return Ok(carrosserietype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarrosserietypeExists(int id)
        {
            return db.Carrosserietypes.Count(e => e.Id == id) > 0;
        }
    }
}