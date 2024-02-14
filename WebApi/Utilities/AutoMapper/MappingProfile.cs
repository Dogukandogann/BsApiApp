using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Entities;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();
            CreateMap<Book,BookDto>();
            CreateMap<BookDtoForInsertion,Book>();
            CreateMap<UserForRegistiration,User>();
        }
    }
}
