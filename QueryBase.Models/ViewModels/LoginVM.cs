using Microsoft.Extensions.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "UserName is empty.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is empty.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
