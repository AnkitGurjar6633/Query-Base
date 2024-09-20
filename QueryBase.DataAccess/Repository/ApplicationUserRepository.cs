using QueryBase.DataAccess.DatabaseContext;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryBase.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRespository
    {
        readonly ApplicationDbContext _db;
        readonly IAnswerRepository _answerRepository;
        readonly IQuestionRepository _questionRepository;
        readonly IVoteAnswerRepository _voteAnswerRepository;
        readonly IVoteQuestionRepository _voteQuestionRepository;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
            _answerRepository = new AnswerRepository(_db);
            _questionRepository = new QuestionRepository(_db);
            _voteAnswerRepository = new VoteAnswerRespository(_db);
            _voteQuestionRepository = new VoteQuestionRepository(_db);
        }

        public void Remove(ApplicationUser user)
        {
            List<VoteAnswer> voteAnswersFromDb = _voteAnswerRepository.GetAll(v => v.UserId == user.Id).ToList();
            foreach (VoteAnswer voteAnswer in voteAnswersFromDb)
            {
                _voteAnswerRepository.Remove(voteAnswer);
            }
            List<VoteQuestion> voteQuestionsFromDb = _voteQuestionRepository.GetAll(v => v.UserId == user.Id).ToList();
            foreach (VoteQuestion voteQuestion in voteQuestionsFromDb)
            {
                _voteQuestionRepository.Remove(voteQuestion);
            }
            List<Answer> answersFromDb = _answerRepository.GetAll(a => a.UserId == user.Id).ToList();
            foreach (Answer answer in answersFromDb)
            {
                _answerRepository.Remove(answer);
            }
            List<Question> questionsFromDb = _questionRepository.GetAll(q => q.UserId == user.Id).ToList();
            foreach (Question question in questionsFromDb)
            {
                _questionRepository.Remove(question);
            }
            _db.Remove(user);
        }
    }
}
