using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using java.util;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Models.Abstraction
{
    public class ModelFactory: IModelFactory
    {
        private readonly IqueueDbDataContext _db;
        public ModelFactory(IqueueDbDataContext db)
        {
            _db = db;
        }
        public IModel CreateAddressModel(Address address)
        {
            var addressModel = new AddressModel(address.Address1, address.Address2,
                address.Postal_Code)
            {
                AddressId = address.Address_Id,
                CityId = address.City_Id,
                City = address.City.Name,
                Province = address.City.Province.Name,
                Latitude = address.Latitude,
                Longitude = address.Longitude,
                InitialDate = address.Initial_Date,
                LastUpdate = address.Last_Update
            };

            return addressModel;
        }
        public IModel CreateCityModel(City city)
        {
            var cityModel = new CityModel(city.Name)
            {
                CityId = city.City_Id,
                Province = city.Province.Name,
                ProvinceId = city.Province_Id
            };

            var addresses = from x in _db.Addresses
                where x.City_Id == city.City_Id
                select x;

            foreach (var address in addresses)
            {
                var addressModel = ((AddressModel) CreateAddressModel(address));
                cityModel.Addresses.Add(addressModel);
            }
            
            return cityModel;
        }
        public IModel CreateUserModel(User user)
        {
            // Missing UserType Variable.
            var userModel = new UserModel(user.FirstName, user.Title, 
                user.Email, user.Password_Hash)
            {
                UserId = user.User_Id,
                LastName = user.LastName,
                AccountActive = user.ActiveAccount,
                Phone = user.Phone,
                Online = user.Online,
                LoginCount = user.LoginCount,
                InitialDate = user.Initial_Date,
                LastUpdate = user.Last_Update
            };

            if (user.Dob != null) userModel.Dob = (DateTime) user.Dob;

            var tickets = from x in _db.Tickets
                where x.User_Id == user.User_Id
                select x;

            foreach (var ticket in tickets)
            {
                userModel.Tickets.Add((TicketModel) CreateTicketModel(ticket));
            }

            var addresses = from x in _db.Users
                where x.User_Id == user.User_Id
                join y in _db.User_Addresses
                    on x.User_Id equals y.User_Id
                join z in _db.Addresses
                    on y.Address_Id equals z.Address_Id
                select z;

            foreach(var address in addresses)
            {
                userModel.Addresses.Add((AddressModel)
                    CreateAddressModel(address));
            }

            return userModel;
        }
        public IModel CreateBranchModel(Branch branch)
        {
            // Address Model not Stored...
            var branchModel = new BranchModel(branch.Name, branch.Code, 
                branch.Code)
            {
                BranchId = branch.Branch_Id,
                Phone = branch.Phone,
                Email = branch.Email,
                LastUpdate = branch.Last_Update,
                InitialDate = branch.Initial_Date
            };

            if (branch.Address_Id != null) branchModel.AddressId = (int) branch.Address_Id;
            var address = _db.Addresses.FirstOrDefault(x => x.Address_Id == branch.Address_Id);
            branchModel.Address = (AddressModel) CreateAddressModel(address);

            return branchModel;
        }
        public IModel CreateProvinceModel(Province province)
        {
            var provinceModel = new ProvinceModel(province.Name)
            {
                ProvinceId = province.Province_Id,
                LastUpdate = province.Last_Update,
                InitialDate = province.Initial_Date,
            };

            // Needs to be tested. Tested and buggy...

            var cities = from x in _db.Cities
                where x.Province_Id == province.Province_Id
                select x;
            
            foreach (var city in cities)
            {
                var cityModel = (CityModel) CreateCityModel(city);
                provinceModel.Cities.Add(cityModel);
            }

            return provinceModel;
        }
        public IModel CreateQueueModel(Queue queue)
        {
            var queueModel = new QueueModel(queue.Service_Id, queue.Status)
            {
                QueueId = queue.Queue_Id,
                LastUpdate = queue.Last_Update,
                InitialDate = queue.Initial_Date
            };

            if (queue.Average_Waiting_Time != null) queueModel.AverageWaitingTime = (TimeSpan) queue.Average_Waiting_Time;

            var tickets = from x in _db.Tickets
                where x.Queue_Id == queue.Queue_Id
                select x;

            foreach (var ticket in tickets)
            {
                queueModel.Tickets.Add((TicketModel)
                    CreateTicketModel(ticket));
            }

            return queueModel;
        }
        public IModel CreateServiceModel(Service service)
        {
            // Needs to be tested on Updates for Branch and Staff
            var serviceModel = new ServiceModel(service.Service_Id,
                service.Branch_Id, service.User_Id, service.Type_Id,
                service.Active)
            {
                Branch = (BranchModel) CreateBranchModel(service.Branch),
                Staff = (UserModel) CreateUserModel(service.User),
            };

            return serviceModel;

        }
        public IModel CreateServiceTypeModel(Service_Type serviceType)
        {
            var serviceTypeModel = new ServiceTypeModel(serviceType.Name)
            {
                TypeId = serviceType.Type_Id,
                Description = serviceType.Description,
                InitialDate = serviceType.Initial_Date,
                LastUpdate = serviceType.Last_Update,
            };

            var queryServices = from x in _db.Services
                where x.Type_Id == serviceType.Type_Id
                select x;

            foreach (var service in queryServices)
            {
                serviceTypeModel.Services.Add((ServiceModel) 
                    CreateServiceModel(service));
            }

            return serviceTypeModel;
        }
        public IModel CreateTicketModel(Ticket ticket)
        {
            var ticketModel = new TicketModel(ticket.User_Id, ticket.Queue_Id,
                ticket.Consultation_Id, ticket.Token, ticket.Status, ticket.Type)
            {
                LastUpdate = ticket.Last_Update,
                InitialDate = ticket.Initial_Date
            };

            if (ticket.Average_Waiting_Time != null) ticket.Average_Waiting_Time = ticket.Average_Waiting_Time;

            // Needs to be tested.
            //ticketModel.User = (UserModel) CreateUserModel(ticket.User);
            //ticketModel.Queue = (QueueModel) CreateQueueModel(ticket.Queue);
            //ticketModel.Consultation = (ConsultationModel) CreateConsultationModel(ticket.Consultation);

            return ticketModel;
        }
        public IModel CreateConsultationModel(Consultation consultation)
        {
            var consultationModel = new ConsultationModel(consultation.Service_Id,
                consultation.Teller, consultation.Status)
            {
                ConsultationId = consultation.Consultation_Id,
                LastUpdate = consultation.Last_Update,
                InitialDate = consultation.Initial_Date
            };

            if (consultation.Start_At != null) consultationModel.StartAt = (TimeSpan) consultation.Start_At;
            if (consultation.End_At != null) consultationModel.EndAt = (TimeSpan) consultation.End_At;
            if (consultation.Duration != null) consultationModel.Duration = (TimeSpan) consultation.Duration;

            var tickets = from x in _db.Tickets
                where x.Consultation_Id == consultation.Consultation_Id
                select x;

            foreach (var ticket in tickets)
            {
                consultationModel.Tickets.Add((TicketModel)
                    CreateTicketModel(ticket));
            }

            return consultationModel;
        }
    }
}