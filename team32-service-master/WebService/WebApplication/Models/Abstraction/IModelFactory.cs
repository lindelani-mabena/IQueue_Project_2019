using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.Abstraction
{
    public interface IModelFactory
    {
        IModel CreateAddressModel(Address address);
        IModel CreateCityModel(City city);
        IModel CreateUserModel(User user);
        IModel CreateBranchModel(Branch branch);
        IModel CreateProvinceModel(Province province);
        IModel CreateQueueModel(Queue queue);
        IModel CreateServiceModel(Service service);
        IModel CreateServiceTypeModel(Service_Type serviceType);
        IModel CreateTicketModel(Ticket ticket);
        IModel CreateConsultationModel(Consultation consultation);
    }
}
