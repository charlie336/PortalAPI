using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LIMSWebPortalAPIApp.Data.DTOs
{
    public class AppUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Password is limited to {1} characters"), MinLength(6)]
        public string Password { get; set; }
    }
}
