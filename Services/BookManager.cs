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

        public BookDto CreateOneBook(BookDtoForInsertion bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _repositoryManager.Book.CreateOneBook(entity);
            _repositoryManager.Save();
            return _mapper.Map<BookDto>(entity);
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

        public BookDto GetBookById(int id, bool trackChanges)
        {
            var book = _repositoryManager.Book.GetOneBookById(id,trackChanges);
            if (book is null) throw new BookNotFoundException(id);
            return _mapper.Map<BookDto>(book);
        }

        public (BookDtoForUpdate bookDtoForUpdate, Book book) GetOneBookForPatch(int id, bool trackChanges)
        {
            var book = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
            {
                throw new BookNotFoundException(id);
            }
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookDtoForUpdate, book);
        }

        public void SaveChangesForPach(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            _repositoryManager.Save();
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
