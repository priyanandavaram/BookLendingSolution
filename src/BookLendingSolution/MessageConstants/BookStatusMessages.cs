namespace BookLendingSolution.MessageConstants
{
    public static class BookStatusMessages
    {
        public const string BookAdded = "Book has been added successfully.";
        public const string BookCheckedOut = "Book is not available for the check out.";
        public const string BookIsAvailable = "Book is already available so can't return the book.";
        public const string CheckoutUserRequired = "Checked-out user must be provided.";
        public const string DuplicateBookTitle = "A book with this title already exists.";
        public const string InvalidBookInfo = "Provided book information is not valid.";
    }
}