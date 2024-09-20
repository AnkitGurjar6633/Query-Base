using QueryBase.DataAccess.DatabaseContext;
using QueryBase.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _db;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IApplicationUserRespository ApplicationUserRespository { get; private set; }
        public IQuestionRepository QuestionRepository { get; private set; }
        public IAnswerRepository AnswerRepository { get; private set; }
        public IVoteQuestionRepository VoteQuestionRepository { get; private set; }
        public IVoteAnswerRepository VoteAnswerRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
            ApplicationUserRespository = new ApplicationUserRepository(_db);
            QuestionRepository = new QuestionRepository(_db);
            AnswerRepository = new AnswerRepository(_db);
            VoteQuestionRepository = new VoteQuestionRepository(_db);
            VoteAnswerRepository = new VoteAnswerRespository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
