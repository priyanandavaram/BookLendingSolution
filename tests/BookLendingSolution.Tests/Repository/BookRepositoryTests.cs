using BookLendingSolution.Models;
using BookLendingSolution.Repository;
using BookLendingSolution.Tests.SampleTestData;
using FluentAssertions;

namespace BookLendingSolution.Tests.Repository
{
    [TestFixture]
    public class BookRepositoryTests
    {
        private BookRepository _bookRepository;

        [SetUp]
        public void SetUp()
        {
            _bookRepository = new BookRepository();
        }

        [Test]
        public void AddBook_ShouldAssignId_AndStoreBook()
        {
            var (bookAdded, addedBookInfo) = _bookRepository.AddBook(BookTestData.bookTestDataWithAvailability);

            bookAdded.Should().BeTrue();

            addedBookInfo.Should().NotBeNull();

            addedBookInfo.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void GetBookById_ShouldReturnBook_WhenExists()
        {
            var (bookAdded, addedBookInfo) = _bookRepository.AddBook(BookTestData.bookTestData);

            var result = _bookRepository.GetBookById(1);

            result.Should().NotBeNull();

            result.BookTitle.Should().Be("Harrypotter Part-1");
        }

        [Test]
        public void GetBookById_ShouldReturnNull_WhenBookNotFound()
        {
            var result = _bookRepository.GetBookById(3);

            result.Should().BeNull();
        }

        [Test]
        public void GetAllBooks_ShouldReturnAllAddedBooks()
        {
            _bookRepository.AddBook(BookTestData.bookTestDataWithAvailability);

            _bookRepository.AddBook(BookTestData.bookTestData);

            var result = _bookRepository.GetAllBooks();

            result.Should().HaveCount(2);

            result.Should().Contain(b => b.BookTitle == "Horror Stories");
        }

        [Test]
        public void GetBookByName_ShouldReturnTrue_WhenNameMatches()
        {
            _bookRepository.AddBook(BookTestData.bookTestData);

            var bookExists = _bookRepository.GetBookByName("Harrypotter Part-1");

            bookExists.Should().BeTrue();
        }

        [Test]
        public void GetBookByName_ShouldReturnFalse_WhenNoMatch()
        {
            var bookExists = _bookRepository.GetBookByName("Hound of Baskerville");

            bookExists.Should().BeFalse();
        }

        [Test]
        public void CheckoutBook_ShouldUpdateBookAvailability()
        {
            var (bookAdded, addedBookInfo) = _bookRepository.AddBook(BookTestData.bookTestDataWithAvailability);

            _bookRepository.CheckoutBook(addedBookInfo.Id, "James Jennet");

            var updated = _bookRepository.GetBookById(addedBookInfo.Id);

            updated.IsBookAvailable.Should().BeFalse();

            updated.CheckedOutUser.Should().Be("James Jennet");

            updated.CheckedOutTime.Should().BeWithin(TimeSpan.FromSeconds(2));
        }

        [Test]
        public void ReturnBook_ShouldSetAvailabilityTrue_WhenBookIsCheckedOut()
        {
            var (bookAdded, addedBookInfo) = _bookRepository.AddBook(BookTestData.bookTestData);

            _bookRepository.ReturnBook(addedBookInfo.Id);

            var updated = _bookRepository.GetBookById(addedBookInfo.Id);

            updated.IsBookAvailable.Should().BeTrue();
        }
    }
}