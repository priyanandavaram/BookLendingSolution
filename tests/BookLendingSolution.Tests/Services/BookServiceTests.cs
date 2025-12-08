using BookLendingSolution.Interfaces;
using BookLendingSolution.Service;
using BookLendingSolution.Tests.SampleTestData;
using FluentAssertions;
using Moq;

namespace BookLendingSolution.Tests.Service
{
    [TestFixture]
    public class BookServiceTests
    {
        private Mock<IBookRepository> _mockBookRepository;

        private BookService _bookService;

        [SetUp]
        public void SetUp()
        {
            _mockBookRepository = new Mock<IBookRepository>();

            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Test]
        public void AddBook_ShouldMarkBookAsAvailable()
        {           
            _mockBookRepository.Setup(r => r.AddBook(BookTestData.bookTestDataWithAvailability)).Returns((true, BookTestData.bookTestDataWithAvailability));

            var result = _bookService.AddBook(BookTestData.bookTestDataWithAvailability);

            BookTestData.bookTestDataWithAvailability.IsBookAvailable.Should().BeTrue();

            _mockBookRepository.Verify(r => r.AddBook(BookTestData.bookTestDataWithAvailability), Times.Once);           
        }       

        [Test]
        public void CheckoutBook_ShouldCallRepository()
        {
            _bookService.CheckoutBook(1, "Paul Cooper");

            _mockBookRepository.Verify(r => r.CheckoutBook(1, "Paul Cooper"), Times.Once);
        }

        [Test]
        public void GetAllBooks_ShouldReturnBooksFromRepository()
        {
            _mockBookRepository.Setup(r => r.GetAllBooks()).Returns(BookTestData.booksList);

            var result = _bookService.GetAllBooks();

            result.Should().BeEquivalentTo(BookTestData.booksList);

            result.Should().HaveCount(2);
        }

        [Test]
        public void GetBookById_ShouldReturnBookFromRepository()
        {
            _mockBookRepository.Setup(r => r.GetBookById(1)).Returns(BookTestData.bookTestData);

            var result = _bookService.GetBookById(1);

            result.Should().Be(BookTestData.bookTestData);
        }

        [Test]
        public void GetBookByName_ShouldReturnBoolFromRepository()
        {
            _mockBookRepository.Setup(r => r.GetBookByName("harrypotter part-1")).Returns(true);

            var result = _bookService.GetBookByName("harrypotter part-1");

            result.Should().BeTrue();
        }

        [Test]
        public void ReturnBook_ShouldCallRepository()
        {
            _bookService.ReturnBook(2);

            _mockBookRepository.Verify(r => r.ReturnBook(2), Times.Once);
        }
    }
}