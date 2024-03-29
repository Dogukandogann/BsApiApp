﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Entities;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private readonly IBookLinks _bookLinks;
        private readonly ICategoryService _categoryService;

        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper, IBookLinks bookLinks, ICategoryService categoryService)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
            _bookLinks = bookLinks;
            _categoryService = categoryService;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var category = await _categoryService.GetOneCategoryByIdAsync(bookDto.CategoryId,false);
            var entity =  _mapper.Map<Book>(bookDto);
            _repositoryManager.Book.CreateOneBook(entity);
           await _repositoryManager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
           var entity= await _repositoryManager.Book.GetOneBookByIdAsync(id, trackChanges);
            if(entity is null) throw new BookNotFoundException(id);
            _repositoryManager.Book.DeleteOneBook(entity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> 
            GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges)
        {
            if (!linkParameters.BookParameters.ValidPriceRange)
                throw new PriceOutOfRangeBadRequestException();
            
            var booksWithMetaData = await _repositoryManager.
                Book.
                GetAllBooksAsync(linkParameters.BookParameters, trackChanges);
              
           var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
            var links = _bookLinks.TryGenerateLinks(booksDto,linkParameters.BookParameters.Fields,linkParameters.HttpContent);
            return (linkResponse: links, metaData: booksWithMetaData.MetaData);
        }

        public Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
           var books  = _repositoryManager.Book.GetAllBooksAsync(trackChanges);
            return books;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _repositoryManager.Book.GetAllBooksWithDetailsAsync(trackChanges);
        }

        public async Task<BookDto> GetBookByIdAsync(int id, bool trackChanges)
        {
            var book = await _repositoryManager.Book.GetOneBookByIdAsync(id,trackChanges);
            if (book is null) throw new BookNotFoundException(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await _repositoryManager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (book is null)
            {
                throw new BookNotFoundException(id);
            }
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookDtoForUpdate, book);
        }

        public async Task SaveChangesForPachAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateBookAsync(BookDtoForUpdate bookDto, int id, bool trackChanges)
        {
            var category = await _categoryService.GetOneCategoryByIdAsync(bookDto.CategoryId,false);

            var entity = await _repositoryManager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null) throw new BookNotFoundException(id);
            entity = _mapper.Map<Book>(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}
