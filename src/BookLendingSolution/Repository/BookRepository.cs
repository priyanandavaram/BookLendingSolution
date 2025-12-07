using BookLendingSolution.Interfaces;
using BookLendingSolution.Models;
using System.Collections.Concurrent;

namespace BookLendingSolution.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ConcurrentDictionary<int, Book> _books = new();

        private int _idCounter = 0;

        public (bool, Book) AddBook(Book book)
        {
            book.Id = Interlocked.Increment(ref _idCounter);

            bool bookStatus = _books.TryAdd(book.Id, book);

            if(bookStatus)
            {
                return (bookStatus, book);
            }
            else
            {
                return (bookStatus, null);
            }
        }

        public void CheckoutBook(int bookId, string checkedoutUser)
        {
            var bookInfo = GetBookById(bookId);

            bookInfo.IsBookAvailable = false;
            bookInfo.CheckedOutUser = checkedoutUser;
            bookInfo.CheckedOutTime = DateTime.Now;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books.Values.ToList();
        }

        public Book GetBookById(int bookId)
        {
            _books.TryGetValue(bookId, out var book);
            return book;
        }

        public bool GetBookByName(string bookName)
        {
            bool checkIfBookExists = _books.Values.Any(bookInfo => bookInfo.BookTitle.
                                                   Equals(bookName, StringComparison.OrdinalIgnoreCase));

            return checkIfBookExists;
        }

        public void ReturnBook(int bookId)
        {
            var book = GetBookById(bookId);

            if (book != null)
            {
                if (!book.IsBookAvailable)
                {
                    book.IsBookAvailable = true;
                }
            }

        }
    }
}