using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Code
{
    public class DatabaseHelper
    {
        private static readonly IqueueDbDataContext Db = new IqueueDbDataContext();

        private readonly CityRepository _cityRepository;
        private readonly ProvinceRepository _provinceRepository;
        private readonly UserRepository _userRepository;
        private readonly AddressRepository _addressRepository;
        private readonly QueueRepository _queueRepository;
        private readonly BranchRepository _branchRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly ConsultationRepository _consultationRepository;

        public DatabaseHelper()
        {
            _cityRepository = new CityRepository(Db);
            _provinceRepository = new ProvinceRepository(Db);
            _userRepository = new UserRepository(Db);
            _addressRepository = new AddressRepository(Db);
            _queueRepository = new QueueRepository(Db);
            _branchRepository = new BranchRepository(Db);
            _serviceRepository = new ServiceRepository(Db);
            _serviceTypeRepository = new ServiceTypeRepository(Db);
            _ticketRepository = new TicketRepository(Db);
            _consultationRepository = new ConsultationRepository(Db);
        }

        /*
         * Province Model Methods
         */
        public async Task<List<ProvinceModel>> GetProvinces() => await Task.Run(() => _provinceRepository.Provinces());
        public async Task<ProvinceModel> GetProvince(int id) =>
            await Task.Run(() => _provinceRepository.GetProvince(id));
        public async Task<ProvinceModel> AddProvince(ProvinceModel provinceModel) =>
            await Task.Run(() => _provinceRepository.AddProvince(provinceModel));
        public async Task<ProvinceModel> UpdateProvince(int id, ProvinceModel provinceModel) =>
            await Task.Run(() => _provinceRepository.UpdateProvince(id, provinceModel));
        public async Task<ProvinceModel> DeleteProvince(int id) =>
            await Task.Run(() => _provinceRepository.DeleteProvince(id));
        /*
         * City Model Methods
         */
        public async Task<List<CityModel>> GetCities() => await Task.Run(() => _cityRepository.Cities());
        public async Task<CityModel> GetCity(int id) => await Task.Run(() => _cityRepository.GetCity(id));
        public async Task<CityModel> AddCity(CityModel cityModel) =>
            await Task.Run(() => _cityRepository.AddCity(cityModel));
        public async Task<CityModel> UpdateCity(int cityId, CityModel cityModel) =>
            await Task.Run(() => _cityRepository.UpdateCity(cityId, cityModel));
        public async Task<CityModel> DeleteCity(int cityId) => await Task.Run(() => _cityRepository.DeleteCity(cityId));

        /*
         * User Model Methods
         */
        public async Task<List<UserModel>> GetUsers() => await Task.Run(() => _userRepository.GetUsers());
        public async Task<UserModel> AddUser(UserModel userModel) =>
            await Task.Run(() => _userRepository.Create(userModel));
        public async Task<UserModel> GetUser(int userId) => await Task.Run(() => _userRepository.GetUser(userId));
        public async Task<UserModel> UpdateUser(int userId, UserModel userModel) =>
            await Task.Run(() => _userRepository.UpdateUser(userId, userModel));
        public async Task<UserModel> DeleteUser(int userId) => await Task.Run(() => _userRepository.DeleteUser(userId));

        /*
         * Address Model Methods
         */
        public async Task<List<AddressModel>> GetAddresses() => await Task.Run(() => _addressRepository.Addresses());
        public async Task<AddressModel> GetAddress(int addressId) =>
            await Task.Run(() => _addressRepository.GetAddress(addressId));
        public async Task<AddressModel> UpdateAddress(int addressId, AddressModel addressModel) =>
            await Task.Run(() => _addressRepository.UpdateAddress(addressId, addressModel));
        public async Task<AddressModel> DeleteAddress(int addressId) =>
            await Task.Run(() => _addressRepository.DeleteAddress(addressId));

        public async Task<AddressModel> AddAddress(AddressModel addressModel) =>
            await Task.Run(() => _addressRepository.AddAddress(addressModel));

        /*
         * Queue Model Methods
         */
        public async Task<List<QueueModel>> GetQueues() => await Task.Run(() => _queueRepository.Queues());
        public async Task<QueueModel> GetQueue(int queueId) => await Task.Run(() => _queueRepository.GetQueue(queueId));
        public async Task<QueueModel> UpdateQueue(int queueId, QueueModel queueModel) =>
            await Task.Run(() => _queueRepository.UpdateQueue(queueId, queueModel));
        public async Task<QueueModel> AddQueue(QueueModel queueModel) =>
            await Task.Run(() => _queueRepository.AddQueue(queueModel));
        public async Task<QueueModel> DeleteQueue(int queueId) =>
            await Task.Run(() => _queueRepository.DeleteQueue(queueId));

        /*
         * Ticket Model Methods
         */
        public async Task<List<TicketModel>> GetTickets() => await Task.Run(() => _ticketRepository.Tickets());
        public async Task<TicketModel> GetTicket(int ticketId) =>
            await Task.Run(() => _ticketRepository.GetTicket(ticketId));
        public async Task<TicketModel> AddTicket(TicketModel ticketModel) =>
            await Task.Run(() => _ticketRepository.AddTicket(ticketModel));
        public async Task<TicketModel> DeleteTicket(int ticketId) =>
            await Task.Run(() => _ticketRepository.DeleteTicket(ticketId));
        public async Task<TicketModel> UpdateTicket(int ticketId, TicketModel ticketModel) =>
            await Task.Run(() => _ticketRepository.UpdateTicket(ticketId, ticketModel));

        /*
         * Consultation Model Methods
         */
        public async Task<List<ConsultationModel>> GetConsultations() =>
            await Task.Run(() => _consultationRepository.Consultations());
        public async Task<ConsultationModel> GetConsultation(int consultationId) =>
            await Task.Run(() => _consultationRepository.GetConsultation(consultationId));
        public async Task<ConsultationModel> AddConsultation(ConsultationModel consultationModel) =>
            await Task.Run(() => _consultationRepository.AddConsultation(consultationModel));
        public async Task<ConsultationModel> UpdateConsultation(int consultationId, ConsultationModel consultationModel) => await Task.Run(() => _consultationRepository.UpdateConsultation(consultationId, consultationModel));
        public async Task<ConsultationModel> DeleteConsultation(int consultationId) =>
            await Task.Run(() => _consultationRepository.DeleteConsultation(consultationId));
        /*
         * Branch Model Methods
         */
        public async Task<List<BranchModel>> GetBranches() => 
            await Task.Run(() => _branchRepository.Branches());
        public async Task<BranchModel> GetBranch(int branchId) =>
            await Task.Run(() => _branchRepository.GetBranch(branchId));
        public async Task<BranchModel> AddBranch(BranchModel branchModel) =>
            await Task.Run(() => _branchRepository.AddBranch(branchModel));
        public async Task<BranchModel> UpdateBranch(int branchId, BranchModel branchModel) =>
            await Task.Run(() => _branchRepository.UpdateBranch(branchId, branchModel));
        public async Task<BranchModel> DeleteBranch(int branchId) =>
            await Task.Run(() => _branchRepository.DeleteBranch(branchId));
        /*
         * Services and Service Types Methods
         */
        public async Task<List<ServiceModel>> GetServices() => 
            await Task.Run(() => _serviceRepository.Services());
        public async Task<List<ServiceTypeModel>> GetServiceTypes() =>
            await Task.Run(() => _serviceTypeRepository.ServiceTypes());
        public async Task<ServiceModel> GetService(int serviceId) =>
            await Task.Run(() => _serviceRepository.GetService(serviceId));
        public async Task<ServiceTypeModel> GetServiceType(int typeId) =>
            await Task.Run(() => _serviceTypeRepository.GetServiceType(typeId));
        public async Task<ServiceModel> AddService(ServiceModel serviceModel) =>
            await Task.Run(() => _serviceRepository.AddService(serviceModel));
        public async Task<ServiceTypeModel> AddServiceType(ServiceTypeModel serviceTypeModel) =>
            await Task.Run(() => _serviceTypeRepository.AddServiceType(serviceTypeModel));
        public async Task<ServiceModel> UpdateService(int serviceId, ServiceModel serviceModel) =>
            await Task.Run(() => _serviceRepository.UpdateService(serviceId, serviceModel));
        public async Task<ServiceTypeModel> UpdateServiceType(int typeId, ServiceTypeModel serviceTypeModel) =>
            await Task.Run(() => _serviceTypeRepository.UpdateServiceType(typeId, serviceTypeModel));
        public async Task<ServiceModel> DeleteService(int serviceId) =>
            await Task.Run(() => _serviceRepository.DeleteService(serviceId));
        public async Task<ServiceTypeModel> DeleteServiceType(int typeId) =>
            await Task.Run(() => _serviceTypeRepository.DeleteServiceType(typeId));
    }
}