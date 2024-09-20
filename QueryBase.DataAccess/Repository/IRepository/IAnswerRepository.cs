using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository.IRepository
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        void Add(Answer answer);
        void Remove(Answer answer);
        void UpdateAnswer(Answer answer);

        //void UpdateAnswerVotesCount(Guid answerId, int value);
    }
}
