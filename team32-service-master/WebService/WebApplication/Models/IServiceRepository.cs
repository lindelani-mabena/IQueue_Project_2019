using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public interface IServiceRepository
    {
        List<ServiceModel> Services();
        ServiceModel GetService(int serviceId);
        ServiceModel AddService(ServiceModel serviceModel);
        ServiceModel DeleteService(int serviceId);
        ServiceModel UpdateService(int serviceId, ServiceModel serviceModel);
    }
}
