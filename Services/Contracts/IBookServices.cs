using Entities.DataTransferObjects;
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
        IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        Book GetBookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void DeleteOneBook(int id, bool trackChanges);
        void UpdateBook(BookDtoForUpdate bookDto, int id, bool trackChanges);
    }
}
