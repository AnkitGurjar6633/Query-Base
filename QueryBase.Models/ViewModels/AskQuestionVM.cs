using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models.ViewModels
{
    public class AskQuestionVM
    {
        [Required]
        [StringLength(150, ErrorMessage = "Too Long")]
        public string? Title { get; set; }
        
        [Required]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Select a Category.")]
        public Guid CategoryId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
