using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/consultations")]
    public class ConsultationsController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;

        public ConsultationsController(): this(new DatabaseHelper()) { }

        public ConsultationsController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        // GET: api/api/consultations
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var consultations = await _databaseHelper.GetConsultations();
            if (consultations.Count > 0) return Ok(consultations);
            return Content(HttpStatusCode.NotFound, $"No Consultations found");
        }

        // GET: api/v1/consultations/5
        [Route("{consultationId}")]
        public async Task<IHttpActionResult> Get(int consultationId)
        {
            var consultation = await _databaseHelper.GetConsultation(consultationId);
            if (consultation != null) return Ok(consultation);
            return Content(HttpStatusCode.NotFound, $"Could not find consultation with id: {consultationId}");
        }

        // POST: api/v1/consultations
        [Route("")]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(ConsultationModel consultationModel)
        {
            var consultation = await _databaseHelper.AddConsultation(consultationModel);
            if (consultation != null) return Ok(consultation);
            return Content(HttpStatusCode.BadRequest, $"Could not add consultation: {consultationModel}");
        }

        // PUT: api/v1/consultations/5
        [Route("{consultationId}")]
        [ValidateModel]
        public async Task<IHttpActionResult> Put(int consultationId, ConsultationModel consultationModel)
        {
            var consultation = await _databaseHelper.UpdateConsultation(consultationId, consultationModel);
            if (consultation != null) return Ok(consultation);
            return Content(HttpStatusCode.BadRequest,
                $"Could not update consultation with id: {consultationId} by consultation: {consultationModel}");
        }

        // DELETE: api/v1/consultations/5
        [Route("{consultationId}")]
        public async Task<IHttpActionResult> Delete(int consultationId)
        {
            var consultation = await _databaseHelper.DeleteConsultation(consultationId);
            if (consultation != null) return Ok(consultation);
            return Content(HttpStatusCode.BadRequest, $"Could not delete consultation with id: {consultationId}");
        }
    }
}
