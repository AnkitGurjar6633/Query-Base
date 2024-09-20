using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PostedDateAndTime { get; set; }
        public DateTime ModifiedDateAndTime { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public int ViewsCount { get; set; }
        public int AnswerCount { get; set; }
        public int VotesCount { get; set; }


        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? User { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
    }
}
