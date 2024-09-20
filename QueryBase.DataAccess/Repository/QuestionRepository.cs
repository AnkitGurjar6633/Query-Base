using Newtonsoft.Json.Linq;
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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _db;

        public QuestionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void UpdateQuestionDetails(Question question)
        {
            Question questionFromDb = Get(q => q.Id == question.Id);
            if (questionFromDb != null)
            {
                questionFromDb.Title = question.Title;
                questionFromDb.Description = question.Description;
                questionFromDb.ModifiedDateAndTime = question.ModifiedDateAndTime;
                questionFromDb.CategoryId = question.CategoryId;
            }
            return;
        }

        //public void UpdateQuestionAnswersCount(Guid id, int count)
        //{
        //    Question question = Get(q => q.Id == id);
        //    if (question != null)
        //    {
        //        question.AnswerCount += count;
        //    }
        //    return;
        //}

        public void UpdateQuestionViewsCount(Guid id)
        {
            Question question = Get(q => q.Id == id);
            if (question != null)
            {
                question.ViewsCount++;
            }
            return;
        }

        //public void UpdateQuestionVotesCount(Guid id, int count)
        //{
        //    Question question = Get(q => q.Id == id);
        //    if (question != null)
        //    {
        //        question.VotesCount += count;
        //    }
        //}

        public void Add(Question question)
        {
            _db.Questions.Add(question);
            return;
        }

        public void Remove(Question question)
        {
            List<VoteQuestion> voteQuestionsFromDb = _db.VoteQuestions.Where(v => v.QuestionId == question.Id).ToList();
            _db.VoteQuestions.RemoveRange(voteQuestionsFromDb);
            List<VoteAnswer> voteAnswersFromDb = _db.VoteAnswers.Where(v => v.QuestionId == question.Id).ToList();
            _db.VoteAnswers.RemoveRange(voteAnswersFromDb);
            List<Answer> answersFromDb = _db.Answers.Where(a => a.QuestionId == question.Id).ToList();
            _db.Answers.RemoveRange(answersFromDb);
            _db.Questions.Remove(question);
        }
    }
}
