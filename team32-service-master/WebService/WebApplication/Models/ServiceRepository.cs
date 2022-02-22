using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class ServiceRepository: IServiceRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public ServiceRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(db);
        }
        public List<ServiceModel> Services()
        {
            var serviceModels = new List<ServiceModel>();

            var queryServices = _db.Services;
            foreach (var service in queryServices)
            {
                serviceModels.Add((ServiceModel)_modelFactory.CreateServiceModel(service));
            }

            return serviceModels;

        }
        public ServiceModel GetService(int serviceId)
        {
            var service = _db.Services.FirstOrDefault(x => x.Service_Id == serviceId);
            if (service == null) return null;
            return (ServiceModel) _modelFactory.CreateServiceModel(service);
        }
        public ServiceModel AddService(ServiceModel serviceModel)
        {
            var service = new Service()
            {
                Branch_Id = serviceModel.BranchId,
                User_Id = serviceModel.UserId,
                Active = serviceModel.Active,
                Type_Id = serviceModel.TypeId,
                Last_Update = serviceModel.LastUpdate,
                Initial_Date = serviceModel.InitialDate
            };

            _db.Services.InsertOnSubmit(service);
            _db.SubmitChanges();

            return (ServiceModel) _modelFactory.CreateServiceModel(service);
        }
        public ServiceModel DeleteService(int serviceId)
        {
            var service = _db.Services.FirstOrDefault(x => x.Service_Id == serviceId);
            if (service == null) return null;
            _db.Services.DeleteOnSubmit(service);
            _db.SubmitChanges();
            return (ServiceModel) _modelFactory.CreateServiceModel(service);
        }
        public ServiceModel UpdateService(int serviceId, ServiceModel serviceModel)
        {
            var service = _db.Services.FirstOrDefault(x => x.Service_Id == serviceId);

            if (service == null) return null;

            service.Branch_Id = serviceModel.BranchId;
            service.User_Id = serviceModel.UserId;
            service.Type_Id = serviceModel.TypeId;
            service.Active = serviceModel.Active;
            serviceModel.LastUpdate = serviceModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (ServiceModel) _modelFactory.CreateServiceModel(service);
        }
    }
}