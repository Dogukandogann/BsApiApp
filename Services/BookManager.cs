using Entities.Entities;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;


        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
        }

        public Book CreateOneBook(Book book)
        {
            _repositoryManager.Book.CreateOneBook(book);
            _repositoryManager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
           var entity= _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if(entity is null)
            {
                string message = $"The Book with id:{id} could not found";
                _loggerService.LogInfo(message);
                throw new Exception(message);
                
            }
            _repositoryManager.Book.DeleteOneBook(entity);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _repositoryManager.Book.GetAllBooks(trackChanges);
            
        }

        public Book GetBookById(int id, bool trackChanges)
        {
            return _repositoryManager.Book.GetOneBookById(id,trackChanges);
        }

        public void UpdateBook(Book book, int id, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                string message = $"Book with id:{id} is not found";
                _loggerService.LogInfo(message);
                throw new Exception(message);
            }
           
            entity.Title = book.Title;
            entity.Price = book.Price;
            _repositoryManager.Save();

        }
    }
}
