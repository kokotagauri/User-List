using System.ComponentModel.DataAnnotations;

namespace UserList.Dtos.User
{
    public class AddUserValidatedDto : AddUserDto
    {
        [Required]
        public override string FirstName { get; set; }
        [Required] 
        public override string LastName { get; set; }
        [Required]
        public override string Identity { get; set; }
    }
}
