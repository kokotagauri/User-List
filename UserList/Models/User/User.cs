using System;
using System.Collections.Generic;

namespace UserList.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Identity { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string Image { get; set; }

        public ICollection<Phone.Phone> Phones { get; set; }
        public ICollection<ConnectedUser> ConnectedUsers { get; set; }
    }
}
