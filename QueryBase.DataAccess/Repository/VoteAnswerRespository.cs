using QueryBase.DataAccess.DatabaseContext;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository
{
    public class VoteAnswerRespository : Repository<VoteAnswer>, IVoteAnswerRepository
    {
        private readonly ApplicationDbContext _db;

        public VoteAnswerRespository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Add(VoteAnswer voteAnswer)
        {
            Answer? answer = _db.Answers.FirstOrDefault(a => a.Id == voteAnswer.AnswerId);
            if (answer != null)
            {
                if (voteAnswer.isUpvote)
                {
                    answer.VotesCount++;
                }
                else
                {
                    answer.VotesCount--;
                }
                _db.VoteAnswers.Add(voteAnswer);
            }
            return;
        }

        public void Remove(VoteAnswer voteAnswer)
        {
            Answer? answer = _db.Answers.FirstOrDefault(a => a.Id == voteAnswer.AnswerId);
            if (answer != null)
            {
                if (voteAnswer.isUpvote)
                {
                    answer.VotesCount--;
                }
                else
                {
                    answer.VotesCount++;
                }
                _db.VoteAnswers.Remove(voteAnswer);
            }
            return;
        }

        public void Update(VoteAnswer voteAnswer)
        {
            Answer? answer = _db.Answers.FirstOrDefault(a => a.Id == voteAnswer.AnswerId);
            if (answer != null)
            {
                if (voteAnswer.isUpvote)
                {
                    answer.VotesCount += 2;
                }
                else
                {
                    answer.VotesCount -= 2;
                }
                _db.VoteAnswers.Update(voteAnswer);
            }
            return;
        }
    }
}
