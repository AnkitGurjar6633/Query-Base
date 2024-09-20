using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QueryBase.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }

        public string? AnswerText { get; set; }

        public DateTime PostedDateAndTime { get; set; }


        public Guid UserId { get; set; }

        public Guid QuestionId { get; set; }


        public int VotesCount { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [ForeignKey("QuestionId")]
        [ValidateNever]
        public Question Question { get; set; }
    }
}
