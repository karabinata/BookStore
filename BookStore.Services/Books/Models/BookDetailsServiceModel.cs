﻿using AutoMapper;
using BookStore.Common.Mapping;
using BookStore.Data.Models;
using System.Linq;

namespace BookStore.Services.Books.Models
{
    public class BookDetailsServiceModel : IMapFrom<Book>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorNames { get; set; }

        public Condition Condition { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Publisher { get; set; }

        public int YearOfPublishing { get; set; }

        public int NumberOfPages { get; set; }

        public int BooksAvailable { get; set; }

        public Coverage Coverage { get; set; }

        public byte[] CoverPicture { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.AuthorNames, cfg => cfg.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.Author.Name).ToList())))
                .ForMember(b => b.Publisher, cfg => cfg.MapFrom(b => b.Publisher.Name));
        }
    }
}
