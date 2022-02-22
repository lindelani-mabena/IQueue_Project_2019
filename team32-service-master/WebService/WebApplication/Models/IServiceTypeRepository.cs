using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public interface IServiceTypeRepository
    {
        List<ServiceTypeModel> ServiceTypes();
        ServiceTypeModel GetServiceType(int typeId);
        ServiceTypeModel AddServiceType(ServiceTypeModel serviceTypeModel);
        ServiceTypeModel UpdateServiceType(int typeId, ServiceTypeModel serviceTypeModel);
        ServiceTypeModel DeleteServiceType(int typeId);
    }
}
