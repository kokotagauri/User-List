using System.Collections.Generic;
using UserList.Dtos.User;
using UserList.Dtos.User.ConnectedUser;

namespace UserList.ViewModels.ConnectedUser
{
    public class ConnectVM
    {
        public AddConnectedUserDto ConnectedUser { get; set; }
        public List<UserForListDto> Users { get; set; }
    }
}
