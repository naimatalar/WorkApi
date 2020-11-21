using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.Models.RequestModel
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email or Username is a required field.")]
        public string Key { get; set; }
       [Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; }

    }
}
