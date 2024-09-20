using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IApplicationUserRespository ApplicationUserRespository { get; }
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IVoteQuestionRepository VoteQuestionRepository { get; }
        IVoteAnswerRepository VoteAnswerRepository { get; }

        void Save();
    }
}
