using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class ServiceTypeRepository: IServiceTypeRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public ServiceTypeRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(_db);
        }

        public List<ServiceTypeModel> ServiceTypes()
        {
            var serviceTypeModels = new List<ServiceTypeModel>();
            var queryServiceTypes = _db.Service_Types;

            foreach (var serviceType in queryServiceTypes)
            {
                serviceTypeModels.Add((ServiceTypeModel) 
                    _modelFactory.CreateServiceTypeModel(serviceType));
            }
            return serviceTypeModels;
        }

        public ServiceTypeModel GetServiceType(int typeId)
        {
            var queryServiceType = _db.Service_Types.FirstOrDefault(x => x.Type_Id == typeId);
            if (queryServiceType == null) return null;

            return (ServiceTypeModel) _modelFactory.CreateServiceTypeModel(queryServiceType);
        }

        public ServiceTypeModel AddServiceType(ServiceTypeModel serviceTypeModel)
        {
            var serviceType = new Service_Type()
            {
                Name = serviceTypeModel.Name,
                Description = serviceTypeModel.Description,
                Initial_Date = serviceTypeModel.InitialDate,
                Last_Update = serviceTypeModel.LastUpdate
            };

            _db.Service_Types.InsertOnSubmit(serviceType);
            _db.SubmitChanges();

            return (ServiceTypeModel) _modelFactory.CreateServiceTypeModel(serviceType);
        }

        public ServiceTypeModel UpdateServiceType(int typeId, ServiceTypeModel serviceTypeModel)
        {
            var serviceType = _db.Service_Types.FirstOrDefault(x => x.Type_Id == typeId);

            if (serviceType == null) return null;

            serviceType.Name = serviceTypeModel.Name;
            serviceType.Description = serviceTypeModel.Description;
            serviceType.Last_Update = serviceTypeModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (ServiceTypeModel)_modelFactory.CreateServiceTypeModel(serviceType);
        }

        public ServiceTypeModel DeleteServiceType(int typeId)
        {
            var serviceType = _db.Service_Types.FirstOrDefault(x => x.Type_Id == typeId);
            if (serviceType == null) return null;
            _db.Service_Types.DeleteOnSubmit(serviceType);
            _db.SubmitChanges();
            return (ServiceTypeModel)_modelFactory.CreateServiceTypeModel(serviceType);
        }
    }
}