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
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels.DataContext;

namespace WebServiceSecured.Controllers
{
    public class ConsultationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Consultations
        public List<Consultation> GetConsultations()
        {
            return db.Consultations.ToList();
        }

        // GET: api/Consultations/5
        [ResponseType(typeof(Consultation))]
        public async Task<IHttpActionResult> GetConsultation(string id)
        {
            Consultation consultation = await db.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            return Ok(consultation);
        }

        // PUT: api/Consultations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutConsultation(string id, Consultation consultation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultation.Id)
            {
                return BadRequest();
            }

            db.Entry(consultation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultationExists(id))
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

        // POST: api/Consultations
        [ResponseType(typeof(Consultation))]
        public async Task<IHttpActionResult> PostConsultation(Consultation consultation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Consultations.Add(consultation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConsultationExists(consultation.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = consultation.Id }, consultation);
        }

        // DELETE: api/Consultations/5
        [ResponseType(typeof(Consultation))]
        public async Task<IHttpActionResult> DeleteConsultation(string id)
        {
            Consultation consultation = await db.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            db.Consultations.Remove(consultation);
            await db.SaveChangesAsync();

            return Ok(consultation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsultationExists(string id)
        {
            return db.Consultations.Count(e => e.Id == id) > 0;
        }
    }
}