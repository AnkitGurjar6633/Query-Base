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
    public class VoteQuestionRepository : Repository<VoteQuestion>, IVoteQuestionRepository
    {
        private readonly ApplicationDbContext _db;

        public VoteQuestionRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Add(VoteQuestion voteQuestion)
        {
            Question? question = _db.Questions.FirstOrDefault(q => q.Id == voteQuestion.QuestionId);
            if (question != null)
            {
                if (voteQuestion.isUpvote)
                {
                    question.VotesCount++;
                }
                else
                {
                    question.VotesCount--;
                }
                _db.VoteQuestions.Add(voteQuestion);
            }
            return;
        }

        public void Remove(VoteQuestion voteQuestion)
        {
            Question? question = _db.Questions.FirstOrDefault(q => q.Id == voteQuestion.QuestionId);
            if (question != null)
            {
                if (voteQuestion.isUpvote)
                {
                    question.VotesCount--;
                }
                else
                {
                    question.VotesCount++;
                }
                _db.VoteQuestions.Remove(voteQuestion);
            }
            return;
        }

        public void Update(VoteQuestion voteQuestion)
        {
            Question? question = _db.Questions.FirstOrDefault(q => q.Id == voteQuestion.QuestionId);
            if (question != null)
            {
                if (voteQuestion.isUpvote)
                {
                    question.VotesCount += 2;
                }
                else
                {
                    question.VotesCount -= 2;
                }
                _db.VoteQuestions.Update(voteQuestion);
            }
            return;
        }
    }
}
