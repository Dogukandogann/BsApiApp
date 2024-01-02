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
        BookDto GetBookById(int id, bool trackChanges);
        BookDto CreateOneBook(BookDtoForInsertion book);
        void DeleteOneBook(int id, bool trackChanges);
        void UpdateBook(BookDtoForUpdate bookDto, int id, bool trackChanges);
        (BookDtoForUpdate bookDtoForUpdate, Book book) GetOneBookForPatch(int id, bool trackChanges);
        void SaveChangesForPach(BookDtoForUpdate bookDtoForUpdate,Book book);
    }
}
