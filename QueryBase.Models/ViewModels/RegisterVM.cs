using Microsoft.AspNetCore.Mvc;
using QueryBase.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name can't be empty.")]
        [MinLength(10, ErrorMessage = "User Name must include 10 or more characters.")]
        [Remote(action: "ValidateUserName", controller: "Account", areaName: "Identity", ErrorMessage = "User already exists.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password can't be empty.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password can't be empty.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Email can't be empty.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber can't be empty.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid phone number.")]
        [Phone]
        public string PhoneNumber { get; set; }

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
    }
}
