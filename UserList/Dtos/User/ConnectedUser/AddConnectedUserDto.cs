using System.ComponentModel.DataAnnotations;

namespace UserList.Dtos.User.ConnectedUser
{
    public class AddConnectedUserDto
    {
        [RegularExpression("კოლეგა|ნაცნობი|ნათესავი|სხვა", ErrorMessage = "Unknown Connection Type")]
        public string Type { get; set; }
        public int UserId { get; set; }
        public int ConnectedUserId { get; set; }
    }
}
