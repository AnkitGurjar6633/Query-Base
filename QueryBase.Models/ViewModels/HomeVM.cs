using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models.ViewModels
{
    public class HomeVM
    {
        public List<Question> questions { get; set; }

        public DateTime timeElapsed { get; set; }
    }
}
