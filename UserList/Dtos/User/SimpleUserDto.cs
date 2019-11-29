using System;

namespace UserList.Dtos.User
{
    public class SimpleUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Identity { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
    }
}
