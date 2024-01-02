using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Entities;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Exceptions.NotFoundException;

namespace Services
{
    public class BookManager : IBookServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;


        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper = null)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
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
            if(entity is null) throw new BookNotFoundException(id);
            _repositoryManager.Book.DeleteOneBook(entity);
            _repositoryManager.Save();
        }

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var books = _repositoryManager.Book.GetAllBooks(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public Book GetBookById(int id, bool trackChanges)
        {
            var book = _repositoryManager.Book.GetOneBookById(id,trackChanges);
            if (book is null) throw new BookNotFoundException(id);
            return book;
        }

        public void UpdateBook(BookDtoForUpdate bookDto, int id, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (entity is null) throw new BookNotFoundException(id);
            entity = _mapper.Map<Book>(entity);
            _repositoryManager.Save();
        }
    }
}
