using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryDbContext _dbContext;
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(RepositoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _bookRepository= new Lazy<IBookRepository>(()=>new BookRepository(_dbContext));
        }

        public IBookRepository Book => _bookRepository.Value;

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
