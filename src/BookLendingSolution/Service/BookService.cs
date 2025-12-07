using BookLendingSolution.Interfaces;
using BookLendingSolution.Models;

namespace BookLendingSolution.Service
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public (bool, Book) AddBook(Book book)
        {
            book.IsBookAvailable = true;

            return _bookRepository.AddBook(book);
        }

        public void CheckoutBook(int bookId, string checkedoutUser)
        {
            _bookRepository.CheckoutBook(bookId, checkedoutUser);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int bookId)
        {
            return _bookRepository.GetBookById(bookId);
        }

        public bool GetBookByName(string bookName)
        {
            return _bookRepository.GetBookByName(bookName);
        }

        public void ReturnBook(int bookId)
        {
            _bookRepository.ReturnBook(bookId);
        }
    }
}