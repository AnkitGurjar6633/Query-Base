using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository.IRepository
{
    public interface IVoteQuestionRepository : IRepository<VoteQuestion>
    {
        void Add(VoteQuestion voteQuestion);
        void Update(VoteQuestion voteQuestion);
        void Remove(VoteQuestion voteQuestion);
    }
}
