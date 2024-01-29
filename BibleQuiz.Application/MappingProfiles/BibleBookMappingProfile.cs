using AutoMapper;
using BibleQuiz.Application.Features.BibleBooks.Query.GetBibleBooks;
using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Application.MappingProfiles;
public class BibleBookMappingProfile : Profile
{
    public BibleBookMappingProfile()
    {
        CreateMap<BibleBook, GetBibleBooksResponse>();
    }
}
