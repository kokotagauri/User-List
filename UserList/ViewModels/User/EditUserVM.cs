using System.Collections.Generic;
using UserList.Dtos.Location;
using UserList.Dtos.User;

namespace UserList.ViewModels.User
{
    public class EditUserVM
    {
        public GetUserDto User { get; set; }
        public List<GetCityDto> Cities { get; set; }
    }
}
