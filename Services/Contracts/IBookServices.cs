using Entities.DataTransferObjects;
using Entities.Entities;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookServices
    {
        Task<(IEnumerable<ExpandoObject> books,MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges);
        Task<BookDto> GetBookByIdAsync(int id, bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book);
        Task DeleteOneBookAsync(int id, bool trackChanges);
        Task UpdateBookAsync(BookDtoForUpdate bookDto, int id, bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForPachAsync(BookDtoForUpdate bookDtoForUpdate,Book book);
    }
}
