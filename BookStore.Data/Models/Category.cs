using System;

namespace BookStore.Data.Models
{
    [Flags]
    public enum Category
    {
        Всички = 1,
        История = 2,
        Криминален = 4,
        Трилър = 8,
        Ужаси = 16
    }
}
