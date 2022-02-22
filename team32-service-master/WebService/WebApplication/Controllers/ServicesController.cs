using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/services")]
    public class ServicesController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;
        
        public ServicesController(): this(new DatabaseHelper()) { }

        public ServicesController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/v1/services
        [Route("")]
        public async Task<IHttpActionResult> GetServices()
        {
            var services = await _databaseHelper.GetServices();
            if (services.Count > 0) return Ok(services);
            return Content(HttpStatusCode.NotFound, "Services not found");
        }

        // GET: api/v1/services/types
        [Route("types")]
        public async Task<IHttpActionResult> GetServiceTypes()
        {
            var serviceTypes = await _databaseHelper.GetServiceTypes();
            if (serviceTypes.Count > 0) return Ok(serviceTypes);
            return Content(HttpStatusCode.NotFound, "Service Types not found");
        }

        // GET: api/v1/services/5
        [Route("{serviceId}")]
        public async Task<IHttpActionResult> GetService(int serviceId)
        {
            var service = await _databaseHelper.GetService(serviceId);
            if (service != null) return Ok(service);
            return Content(HttpStatusCode.NotFound, $"Service with id: {serviceId} not found");
        }

        // GET: api/v1/services/types/5
        [Route("types/{typeId}")]
        public async Task<IHttpActionResult> GetServiceType(int typeId)
        {
            var serviceType = await _databaseHelper.GetServiceType(typeId);
            if (serviceType != null) return Ok(serviceType);
            return Content(HttpStatusCode.NotFound, $"Service Type with id: {typeId} not found");
        }

        // POST: api/v1/services
        [Route("")]
        public async Task<IHttpActionResult> PostService(ServiceModel serviceModel)
        {
            var service = await _databaseHelper.AddService(serviceModel);
            if (service != null) return Ok(service);
            return Content(HttpStatusCode.BadRequest, $"Could not add service: {serviceModel}");
        }

        // POST: api/v1/services/types
        [Route("types")]
        public async Task<IHttpActionResult> PostServiceType(ServiceTypeModel serviceTypeModel)
        {
            var serviceType = await _databaseHelper.AddServiceType(serviceTypeModel);
            if (serviceType != null) return Ok(serviceType);
            return Content(HttpStatusCode.BadRequest, $"Could not add service type: {serviceTypeModel}");
        }

        // PUT: api/v1/services/5
        [Route("{serviceId}")]
        public async Task<IHttpActionResult> PutService(int serviceId, ServiceModel serviceModel)
        {
            var service = await _databaseHelper.UpdateService(serviceId, serviceModel);
            if (service != null) return Ok(service);
            return Content(HttpStatusCode.BadRequest,
                $"Could not update service with id: {serviceId} by service: {serviceModel}");
        }

        // PUT: api/v1/services/types/5
        [Route("types/{typeId}")]
        public async Task<IHttpActionResult> PutService(int typeId, ServiceTypeModel serviceTypeModel)
        {
            var serviceType = await _databaseHelper.UpdateServiceType(typeId, serviceTypeModel);
            if (serviceType != null) return Ok(serviceType);
            return Content(HttpStatusCode.BadRequest,
                $"Could not update service with id: {typeId} by service: {serviceTypeModel}");
        }

        // DELETE: api/v1/services/5
        [Route("{serviceId}")]
        public async Task<IHttpActionResult> DeleteService(int serviceId)
        {
            var service = await _databaseHelper.DeleteService(serviceId);
            if (service != null) return Ok(service);
            return Content(HttpStatusCode.BadRequest, $"Could not delete service with id: {serviceId}");
        }

        // DELETE: api/v1/services/types/5
        [Route("types/{typeId}")]
        public async Task<IHttpActionResult> DeleteServices(int typeId)
        {
            var serviceType = await _databaseHelper.DeleteServiceType(typeId);
            if (serviceType != null) return Ok(serviceType);
            return Content(HttpStatusCode.BadRequest, $"Could not delete service type with id: {typeId}");
        }
    }
}
