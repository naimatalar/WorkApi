using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.Models.RequestModel
{
    public class UserUpdateRequestModel
    {
        [Required(ErrorMessage = "UserId is a required field.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Username is a required field.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        public string Phone { get; set; }

        public string Website { get; set; }
    }
}
