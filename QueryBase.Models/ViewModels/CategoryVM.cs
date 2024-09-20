using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models.ViewModels
{
    public class CategoryVM
    {
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        
    }
}
