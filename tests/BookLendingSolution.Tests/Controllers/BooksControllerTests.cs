using BookLendingSolution.Controllers;
using BookLendingSolution.Interfaces;
using BookLendingSolution.MessageConstants;
using BookLendingSolution.Models;
using BookLendingSolution.Tests.SampleTestData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookLendingSolution.Tests.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IBookService> _mockBookService;

        private BooksController _booksController;
     
        [SetUp]
        public void SetUp()
        {
            _mockBookService = new Mock<IBookService>();

            _booksController = new BooksController(_mockBookService.Object);             
        }

        [Test]
        public void AddNewBook_ShouldReturnBadRequest_WhenInvalidModelProvided()
        {
            _booksController.ModelState.AddModelError("BookTitle", "Required");

            Book invalidBookData = new Book 
            {
                BookTitle = "",
                BookAuthor = ""             
            };

            var result = _booksController.AddNewBook(invalidBookData) as BadRequestObjectResult;

            result.Should().NotBeNull();

            extractResultMessage(result).Should().Be(BookStatusMessages.InvalidBookInfo);
        }

        [Test]
        public void AddNewBook_ShouldReturnConflict_WhenBookAlreadyExists()
        {
            _mockBookService.Setup(s => s.GetBookByName(BookTestData.bookTestData.BookTitle.ToLower())).Returns(true);

            var result = _booksController.AddNewBook(BookTestData.bookTestData) as ConflictObjectResult;            

            result.Should().NotBeNull();

            extractResultMessage(result).Should().Be(BookStatusMessages.BookTitleExists);
        }

        [Test]
        public void AddNewBook_ShouldReturnOk_WhenBookSuccessfullyAdded()
        {
            _mockBookService.Setup(s => s.GetBookByName(BookTestData.bookTestData.BookTitle)).Returns(false);

            _mockBookService.Setup(s => s.AddBook(BookTestData.bookTestData)).Returns((true, BookTestData.bookTestData));

            var result = _booksController.AddNewBook(BookTestData.bookTestData) as OkObjectResult;

            result.StatusCode.Should().Be(200);           
        }

        [Test]
        public void CheckoutBook_ShouldReturnBadRequest_WhenCheckoutUserMissing()
        {
            var result = _booksController.CheckoutBook(1, "") as BadRequestObjectResult;

            var getMessage = extractResultMessage(result);

            result.StatusCode.Should().Be(400);

            extractResultMessage(result).Should().Be(BookStatusMessages.CheckoutUserRequired);            
        }

        [Test]
        public void CheckoutBook_ShouldReturnNotFound_WhenBookNotFound()
        {
            _mockBookService.Setup(s => s.GetBookById(1)).Returns((Book?)null);

            var result = _booksController.CheckoutBook(1, "Thomson");

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public void CheckoutBook_ShouldReturnConflict_WhenBookUnavailable()
        {
            _mockBookService.Setup(s => s.GetBookById(1)).Returns(BookTestData.bookTestData);

            var result = _booksController.CheckoutBook(1, "Thomson") as ConflictObjectResult;

            result.StatusCode.Should().Be(409);

            extractResultMessage(result).Should().Be(BookStatusMessages.BookCheckedOut);
        }

        [Test]
        public void CheckoutBook_ShouldReturnOk_WhenSuccess()
        {
            _mockBookService.Setup(s => s.GetBookById(1)).Returns(BookTestData.bookTestDataWithAvailability);

            var result = _booksController.CheckoutBook(1, "Thomson") as OkObjectResult;

            result.StatusCode.Should().Be(200);
        }

        [Test]
        public void GetAllBooks_ShouldReturnListOfBooks()
        {            
            _mockBookService.Setup(s => s.GetAllBooks()).Returns(BookTestData.booksList);

            var result = _booksController.GetAllBooks();

            result.Should().NotBeNull();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void ReturnBook_ShouldReturnNotFound_WhenBookNotFound()
        {
            _mockBookService.Setup(s => s.GetBookById(1)).Returns((Book)null);

            var result = _booksController.ReturnBook(1) as NotFoundObjectResult;

            result.Should().NotBeNull();

            result.StatusCode.Should().Be(404);

            extractResultMessage(result).Should().Be("Book with the id 1 is not found.");
        }

        [Test]
        public void ReturnBook_ShouldReturnConflict_WhenBookIsAlreadyAvailable()
        {
            _mockBookService.Setup(s => s.GetBookById(1)).Returns(BookTestData.bookTestDataWithAvailability);

            var result = _booksController.ReturnBook(1) as ConflictObjectResult;

            extractResultMessage(result).Should().Be(BookStatusMessages.BookIsAvailable);
        }

        [Test]
        public void ReturnBook_ShouldReturnOk_WhenBookSuccessfullyReturned()
        {           
            _mockBookService.Setup(s => s.GetBookById(1)).Returns(BookTestData.bookTestData);

            var result = _booksController.ReturnBook(1) as OkObjectResult;

            result.StatusCode.Should().Be(200);

            _mockBookService.Verify(s => s.ReturnBook(1), Times.Once);
        }

        private string extractResultMessage(ObjectResult result)
        {
            var parseResult = result.Value;

            return parseResult
                            .GetType()
                            .GetProperty("message")
                            .GetValue(parseResult, null)
                            .ToString();
        }
    }
}
