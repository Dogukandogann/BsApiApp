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

        public BookManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
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
            if (book is null)  throw new ArgumentNullException($"Book with id:{id} is not found");
            entity.Title = book.Title;
            entity.Price = book.Price;
            _repositoryManager.Save();

        }
    }
}
