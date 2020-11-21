using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.Models.RequestModel
{
    public class IdRequestModel
    {
        
        [Required(ErrorMessage = "Email or Username is a required field.")]
        public Guid Id { get; set; }
    }
}
