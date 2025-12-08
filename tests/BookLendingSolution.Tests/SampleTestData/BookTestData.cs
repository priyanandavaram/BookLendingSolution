using BookLendingSolution.Models;

namespace BookLendingSolution.Tests.SampleTestData
{
    public static class BookTestData
    {
        public static Book bookTestData = new Book 
            { 
                BookTitle = "Harrypotter Part-1", BookAuthor = "David Simpson", IsBookAvailable = false 
            };

        public static Book bookTestDataWithAvailability = new Book 
            { 
                BookTitle = "Horror Stories", BookAuthor = "Paul Hanry", IsBookAvailable = true 
            };

        public static List<Book> booksList = new List<Book>
            {
                new Book { BookTitle = "Mummy Return Part 1", BookAuthor = "Mark Spencer", IsBookAvailable = true },
                new Book { BookTitle = "Mummy Return Part 2", BookAuthor = "Spencer Spruce", IsBookAvailable = false }
            };
    }
}
