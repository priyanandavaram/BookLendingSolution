using BookLendingSolution.Models;

namespace BookLendingSolution.Interfaces
{
    public interface IBookOperations
    {
        public (bool, Book) AddBook(Book book);
        void CheckoutBook(int bookId, string checkedoutUser);
        IEnumerable<Book> GetAllBooks();
        bool GetBookByName(string bookName);
        Book GetBookById(int bookId);
        void ReturnBook(int bookId);
    }
}