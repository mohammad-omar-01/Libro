using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;

namespace Libro.Data.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookID))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.AuthorID,
                    opt => opt.MapFrom(src => src.Authors.FirstOrDefault().AuthorID)
                )
                .ForMember(
                    dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Authors.FirstOrDefault().Name)
                );

            CreateMap<BookDTO, Book>()
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookID))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.BookName));
            CreateMap<User, SignupRequestDTO>();
            CreateMap<SignupRequestDTO, User>();
        }
    }
}
