using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Models;
using WebApi.Models.Users;

namespace SampleWebAPI.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            //get samurai with quote
            //CreateMap<SamuraiWithQuotesDTO, Samurai>();
            CreateMap<Samurai, SamuraiWithQuotesDTO>();
            CreateMap<QuoteDTO, Quote>();
            CreateMap<Quote, QuoteDTO>();

            //get samurai with sword 
            //CreateMap<SamuraiWithSwordsDTO, Samurai>();
            CreateMap<Samurai, SamuraiWithSwordsDTO>();
            CreateMap<SwordWithElementTypeDTO, Sword>();
            CreateMap<Sword, SwordWithElementTypeDTO>();
            CreateMap<Element, ElementReadDTO>();
            CreateMap<ElementReadDTO, Element>();
            CreateMap<SType, TypeReadDTO>();
            CreateMap<TypeReadDTO, SType>();

            //samurai read
            CreateMap<Samurai, SamuraiReadDTO>();
            CreateMap<SamuraiReadDTO, Samurai>();

            //add samurai
            CreateMap<SamuraiCreateDTO, Samurai>();

            //add samurai with Sword
            CreateMap<SamuraiCreateWithSwordsDTO, Samurai>();                             

            //Sword read
            CreateMap<Sword, SwordReadDTO>();
            CreateMap<SwordReadDTO, Sword>();

            //add sword
            //CreateMap<SwordCreateDTO, Sword>();
            CreateMap<SwordCDTO, Sword>();

            //add sword with type
            CreateMap<SwordCreateWithType, Sword>();
            CreateMap<TypeCreateDTO, SType>();

            //add Element
            CreateMap<ElementCreateDTO, Element>();      

            //user
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterRequest>();
            CreateMap<User, AuthenticateResponse>();
        }
    }
}
