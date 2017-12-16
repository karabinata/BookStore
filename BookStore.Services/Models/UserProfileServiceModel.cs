using AutoMapper;
using BookStore.Common.Mapping;
using BookStore.Data.Models;
using System;

namespace BookStore.Services.Models
{
    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public string UserName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string PhoneNumber { get; set; }

        public int BooksCount { get; set; } 

        public int OrdersCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<User, UserProfileServiceModel>()
                .ForMember(u => u.BooksCount, cfg => cfg.MapFrom(b => b.Books.Count))
                .ForMember(u => u.OrdersCount, cfg => cfg.MapFrom(o => o.Books.Count));
    }
}
