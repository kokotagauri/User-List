using System.Collections.Generic;
using UserList.Dtos.User;

namespace UserList.Dtos.Pagination
{
    public class UsersFilteredDto
    {
        public List<SimpleUserDto> Users { get; set; }
        public int Count { get; set; }
    }
}
