    using System.ComponentModel.DataAnnotations;

namespace UserList.Dtos.Phone
{
    public class PhoneDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [RegularExpression("სახლის|ოფისის|მობილური", ErrorMessage = "Unknown phone type")]
        public string Type { get; set; }

        [StringLength(50, ErrorMessage = "Phone number must be 4 to 50 symbols", MinimumLength = 4)]
        public string Number { get; set; }
    }
}
