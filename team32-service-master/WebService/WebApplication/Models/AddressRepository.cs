using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class AddressRepository: IAddressRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public AddressRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(db);
        }
        public List<AddressModel> Addresses()
        {
            var addressModels = new List<AddressModel>();

            var queryAddresses = _db.Addresses.ToList();

            foreach (var queryAddress in queryAddresses)
            {
                addressModels.Add((AddressModel)_modelFactory.CreateAddressModel(queryAddress));
            }

            return addressModels;
        }
        public AddressModel AddAddress(AddressModel addressModel)
        {
            var address = new Address()
            {
                Address1 = addressModel.Address1,
                Address2 = addressModel.Address2,
                City_Id = addressModel.CityId,
                Postal_Code = addressModel.Zip,
                Latitude = addressModel.Latitude,
                Longitude = addressModel.Longitude,
                Initial_Date = addressModel.InitialDate,
                Last_Update = addressModel.LastUpdate
            };

            _db.Addresses.InsertOnSubmit(address);
            _db.SubmitChanges();

            return (AddressModel) _modelFactory.CreateAddressModel(address);
        }
        public AddressModel GetAddress(int addressId)
        {
            var queryAddress = _db.Addresses.FirstOrDefault(
                x => x.Address_Id == addressId);
            return queryAddress == null ? null : 
                (AddressModel) _modelFactory.CreateAddressModel(queryAddress);
        }
        public AddressModel UpdateAddress(int addressId, AddressModel addressModel)
        {
            var queryAddress = _db.Addresses.FirstOrDefault(
                x => x.Address_Id == addressId);

            if (queryAddress == null) return null;

            queryAddress.Address1 = addressModel.Address1;
            queryAddress.Address2 = addressModel.Address2;
            queryAddress.City_Id = addressModel.CityId;
            queryAddress.Postal_Code = addressModel.Zip;
            queryAddress.Latitude = addressModel.Latitude;
            queryAddress.Longitude = addressModel.Longitude;
            queryAddress.Last_Update = addressModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (AddressModel) _modelFactory.CreateAddressModel(queryAddress);
        }
        public AddressModel DeleteAddress(int addressId)
        {
            var queryAddress = _db.Addresses.FirstOrDefault(
                x => x.Address_Id == addressId);

            if (queryAddress == null) return null;

            _db.Addresses.DeleteOnSubmit(queryAddress);

            return (AddressModel) _modelFactory.CreateAddressModel(queryAddress);
        }
    }
}