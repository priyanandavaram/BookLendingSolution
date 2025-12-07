using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using BookLendingSolution.Controllers;
using BookLendingSolution.Interfaces;
using BookLendingSolution.Models;

namespace BookLendingSolution.Tests.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IBookService> _mockBookService;
        private BooksController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockBookService = new Mock<IBookService>();
            _controller = new BooksController(_mockBookService.Object);
        }

        [Test]
        public void AddNewBook_ShouldReturnBadRequest_WhenModelInvalid()
        {
            //_controller.ModelState.AddModelError("BookTitle", "Required");
            //_controller.ModelState.AddModelError("BookAuthor", "Required");
            var book = new Book { BookTitle = "Test Book", BookAuthor = "rerere" };

            var result = _controller.AddNewBook(book);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        //[Test]
        //public void AddNewBook_ShouldReturnConflict_WhenBookAlreadyExists()
        //{
        //    var book = new Book { BookTitle = "Test Book", BookAuthor = "rerere" };
        //    _mockBookService.Setup(s => s.GetBookByName("test book")).Returns(true);

        //    var result = _controller.AddNewBook(book);

        //    result.Should().BeOfType<ConflictObjectResult>();
        //}

        [Test]
        public void AddNewBook_ShouldReturnOk_WhenBookSuccessfullyAdded()
        {
            var book = new Book { BookTitle = "Test Book", BookAuthor = "rerere" };
            _mockBookService.Setup(s => s.GetBookByName("new book")).Returns(false);
            _mockBookService.Setup(s => s.AddBook(book)).Returns((true, book));

            var result = _controller.AddNewBook(book) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public void CheckoutBook_ShouldReturnBadRequest_WhenUserMissing()
        {
            var result = _controller.CheckoutBook(1, "");
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public void CheckoutBook_ShouldReturnNotFound_WhenBookNotFound()
        {
            _mockBookService.Setup(s => s.GetBookById(1)).Returns((Book?)null);

            var result = _controller.CheckoutBook(1, "User1");

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public void CheckoutBook_ShouldReturnConflict_WhenBookUnavailable()
        {
            var book = new Book { BookTitle = "Test Book", BookAuthor = "rerere" ,IsBookAvailable = false };
            _mockBookService.Setup(s => s.GetBookById(1)).Returns(book);

            var result = _controller.CheckoutBook(1, "User1");

            result.Should().BeOfType<ConflictObjectResult>();
        }

        [Test]
        public void CheckoutBook_ShouldReturnOk_WhenSuccess()
        {
            var book = new Book { BookTitle = "Test Book", BookAuthor = "rerere", IsBookAvailable = true };
            _mockBookService.Setup(s => s.GetBookById(1)).Returns(book);

            var result = _controller.CheckoutBook(1, "User1");

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
