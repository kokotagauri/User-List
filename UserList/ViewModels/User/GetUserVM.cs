using System.Collections.Generic;
using UserList.Dtos.User;
using UserList.Dtos.User.ConnectedUser;

namespace UserList.ViewModels.User
{
    public class GetUserVM
    {
        public GetUserDto User { get; set; }
        public List<ConnectedUsersReportDto> ConnectionReport { get; set; }
    }
}
