using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository.IRepository
{
    public interface IVoteAnswerRepository : IRepository<VoteAnswer>
    {
        void Add(VoteAnswer voteAnswer);
        void Update(VoteAnswer voteAnswer);
        void Remove(VoteAnswer voteAnswer);
    }
}
