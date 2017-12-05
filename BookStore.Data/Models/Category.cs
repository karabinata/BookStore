using System;

namespace BookStore.Data.Models
{
    [Flags]
    public enum Category
    {
        All = 1,
        Astrology = 2,
        TechnicalLiterature = 4
    }
}
