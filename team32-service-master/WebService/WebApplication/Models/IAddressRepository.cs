using System.Collections.Generic;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    interface IAddressRepository
    {
        List<AddressModel> Addresses();
        AddressModel AddAddress(AddressModel addressModel);
        AddressModel GetAddress(int addressId);
        AddressModel UpdateAddress(int addressId, AddressModel addressModel);
        AddressModel DeleteAddress(int addressId);
    }
}
