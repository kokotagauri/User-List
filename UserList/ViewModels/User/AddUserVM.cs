using System.Collections.Generic;
using UserList.Dtos.Location;
using UserList.Dtos.Phone;
using UserList.Dtos.User;

namespace UserList.ViewModels.User
{
    public class AddUserVM
    {
        public AddUserDto User { get; set; }
        public List<PhoneDto> Phones { get; set; }
        public List<GetCityDto> Cities { get; set; }
    }
}
