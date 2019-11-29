using System;
using System.Collections.Generic;
using UserList.Dtos.Phone;
using UserList.Dtos.User.ConnectedUser;

namespace UserList.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Identity { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public List<PhoneDto> Phones { get; set; }
        public List<GetConnectedUserDto> ConnectedUsers { get; set; }
    }
}
