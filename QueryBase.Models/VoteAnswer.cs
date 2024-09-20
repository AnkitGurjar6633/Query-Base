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
    public class VoteAnswer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public bool isUpvote { get; set; }



        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [ForeignKey("AnswerId")]
        [ValidateNever]
        public Answer Answer { get; set; }

        [ForeignKey("QuestionId")]
        [ValidateNever]
        public Question Question { get; set; }
    }
}
