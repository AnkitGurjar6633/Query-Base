using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository.IRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        void Add(Question question);
        void Remove(Question question);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionViewsCount(Guid id);


        //void UpdateQuestionVotesCount(Guid id,int count);
        //void UpdateQuestionAnswersCount(Guid id,int count);

    }
}
