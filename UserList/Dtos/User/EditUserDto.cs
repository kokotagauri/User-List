using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using UserList.Dtos.Phone;
using UserList.Helpers.Validation;

namespace UserList.Dtos.User
{
    public class EditUserDto
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "First Name must be from 2 to 50 symbols", MinimumLength = 2)]
        [RegularExpression("^[\\u10D0-\\u10F0\\s]*$|^[a-zA-Z\\s]*$", ErrorMessage = "First Name must contain only georgian or only English letters")]
        public virtual string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name must be from 2 to 50 symbols", MinimumLength = 2)]
        [RegularExpression("^[\\u10D0-\\u10F0\\s]*$|^[a-zA-Z\\s]*$", ErrorMessage = "Last Name must contain only georgian or only English letters")]
        public virtual string LastName { get; set; }

        [RegularExpression("ქალი|კაცი", ErrorMessage = "Unknown gender")]
        public string Gender { get; set; }

        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Identity Number must be 11 digits")]
        public virtual string Identity { get; set; }

        [AgeValidation(18)]
        [Required]
        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }
        public IFormFile Image { get; set; }
    }
}
