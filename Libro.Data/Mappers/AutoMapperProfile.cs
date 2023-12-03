using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;

namespace Libro.Data.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookSearchDTO>()
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookID))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));

            CreateMap<BookSearchDTO, Book>()
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookID))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.BookName));

            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
            CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<AuthorDTO, Author>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<User, SignupRequestDTO>();
            CreateMap<SignupRequestDTO, User>();
            CreateMap<BorrowingHistoryDTO, BookBorrowingHistroyUpdateDTO>();
            CreateMap<BookBorrowingHistroyUpdateDTO, BorrowingHistoryDTO>();
            CreateMap<PatronProfileDTO, PatronProfileUpdateDTO>();
            CreateMap<PatronProfileUpdateDTO, PatronProfileDTO>();
        }
    }
}
