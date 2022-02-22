using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/queues")]
    public class QueuesController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;

        public QueuesController(): this(new DatabaseHelper()) { }
        
        public QueuesController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/v1/queues
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var queueModels = await _databaseHelper.GetQueues();
            if (queueModels.Count > 0) return Ok(queueModels);
            return Content(HttpStatusCode.NotFound, "No Queues Found");
        }

        // GET: api/v1/queues/5
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var queue = await  _databaseHelper.GetQueue(id);
            if (queue != null) return Ok(queue);
            return Content(HttpStatusCode.NotFound, $"Could not find queue with id: {id}");
        }

        // POST: api/v1/queues
        [Route("")]
        public async Task<IHttpActionResult> Post(QueueModel queueModel)
        {
            var queue = await _databaseHelper.AddQueue(queueModel);
            if (queue != null) return Ok(queue);
            return Content(HttpStatusCode.BadRequest, $"Could not add queue: {queueModel}");
        }

        // PUT: api/v1/queues/5
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, QueueModel queueModel)
        {
            var queue = await _databaseHelper.UpdateQueue(id, queueModel);
            if (queue != null) return Ok(queue);
            return Content(HttpStatusCode.BadRequest, $"Could not update queue with id: {id} by queue: {queueModel}");
        }

        // DELETE: api/v1/queues/5
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var queue = await _databaseHelper.DeleteQueue(id);
            if (queue != null) return Ok(queue);
            return Content(HttpStatusCode.BadRequest, $"Could not delete queue with id: {id}");
        }
    }
}
