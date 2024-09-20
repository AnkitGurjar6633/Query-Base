using Microsoft.EntityFrameworkCore;
using QueryBase.DataAccess.DatabaseContext;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        private readonly ApplicationDbContext _db;

        public AnswerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Add(Answer entity)
        {
            _db.Answers.Add(entity);
            Question? question = _db.Questions.FirstOrDefault(q => q.Id == entity.QuestionId);
            if(question != null )
            {
                question.AnswerCount += 1;
            }
            return;
        }

        public void Remove(Answer entity)
        {
            Question? question = _db.Questions.FirstOrDefault(q => q.Id == entity.QuestionId);
            if(question != null)
            {
                question.AnswerCount -= 1;
                List<VoteAnswer> voteAnswersFromDb = _db.VoteAnswers.Where(v => v.AnswerId == entity.Id).ToList();
                _db.VoteAnswers.RemoveRange(voteAnswersFromDb);
            }
            _db.Answers.Remove(entity);
            return;
        }

        public void UpdateAnswer(Answer answer)
        {
            Answer answerFromDb = Get(a => a.Id == answer.Id);

            if (answerFromDb != null)
            {
                answerFromDb.AnswerText = answer.AnswerText;
                answerFromDb.PostedDateAndTime = answer.PostedDateAndTime;
            }

            return;
        }

        //public void UpdateAnswerVotesCount(Guid answerId, int value)
        //{
        //    Answer answer = Get(q => q.Id == answerId);
        //    if (answer != null)
        //    {
        //        answer.VotesCount += value;
        //    }
        //    return;
        //}
    }
}
