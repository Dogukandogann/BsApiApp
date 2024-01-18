﻿using Entities.Entities;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
        {
        }

        public void CreateOneBook(Book book) => Create(book);
       

        public void DeleteOneBook(Book book) => Delete(book);


        public async Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges) =>
           await FindAll(trackChanges).OrderBy(b => b.Id).Skip((bookParameters.PageNumber-1)*bookParameters.PageSize).Take(bookParameters.PageSize).ToListAsync();


        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
           await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
            
        

        public void UpdateOneBook(Book book) => Update(book);
       
    }
}
