using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookServices
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges);
        Book GetBookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void DeleteOneBook(int id, bool trackChanges);
        void UpdateBook(Book book, int id, bool trackChanges);
    }
}
