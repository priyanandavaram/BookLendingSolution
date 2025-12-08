using Microsoft.AspNetCore.Mvc;
using BookLendingSolution.Models;
using BookLendingSolution.Interfaces;
using BookLendingSolution.MessageConstants;

namespace BookLendingSolution.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult AddNewBook([FromBody] Book bookInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = BookStatusMessages.InvalidBookInfo });
            }
            else
            {
                var bookExists = _bookService.GetBookByName(bookInfo.BookTitle.ToLower());

                if (bookExists)
                {
                    return Conflict(new { message = BookStatusMessages.BookTitleExists });
                }
                else
                {
                    var (bookAdded, addedBookInfo) = _bookService.AddBook(bookInfo);

                    if (bookAdded)
                    {
                        return Ok(new { message = BookStatusMessages.BookAdded, book = addedBookInfo });
                    }
                    else
                    {
                        return StatusCode(500, new { message = "An error occurred while adding this book." });
                    }
                }
            }
        }

        [HttpPost]
        [Route("{id:int:min(1)}/checkout")]
        public IActionResult CheckoutBook(int id, [FromBody] string checkedoutUser)
        {
            if (string.IsNullOrWhiteSpace(checkedoutUser))
            {
                return BadRequest(new { message = BookStatusMessages.CheckoutUserRequired });
            }

            Book bookInfo = _bookService.GetBookById(id);

            if (bookInfo == null)
            {
                return NotFound(new { message = $"Book with the id {id} is not found." });
            }

            if (!bookInfo.IsBookAvailable)
            {
                return Conflict(new { message = BookStatusMessages.BookCheckedOut });
            }
            else
            {
                _bookService.CheckoutBook(id, checkedoutUser);

                return Ok(new { message = $"Book '{bookInfo.BookTitle}' has been checked out by the user {checkedoutUser}." });
            }
        }

        [HttpGet]
        public IEnumerable<Book> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }

        [HttpPost]
        [Route("{id:int:min(1)}/return")]
        public IActionResult ReturnBook(int id)
        {
            var bookInfo = _bookService.GetBookById(id);

            if (bookInfo == null)
            {
                return NotFound(new { message = $"Book with the id {id} is not found." });
            }

            if (bookInfo.IsBookAvailable)
            {
                return Conflict(new { message = BookStatusMessages.BookIsAvailable });
            }
            else
            {
                _bookService.ReturnBook(id);

                return Ok(new { message = $"Book '{bookInfo.BookTitle}' has been returned successfully." });
            }
        }
    }
}