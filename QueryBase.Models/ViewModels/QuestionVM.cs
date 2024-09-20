using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models.ViewModels
{
    public class QuestionVM
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
