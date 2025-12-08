namespace BookLendingSolution.Models
{
    public class Book
    {
        private DateTime _checkedOutTime;
        public int Id { get; set; }
        public required string BookTitle { get; set; } = string.Empty;
        public required string BookAuthor { get; set; } = string.Empty;
        public string CheckedOutUser { get; set; } = string.Empty;
        public DateTime CheckedOutTime
        {
            get => _checkedOutTime;
            set
            {
                _checkedOutTime = DateTime.Now
                                .AddHours(DateTime.Now.Hour)
                                .AddMinutes(DateTime.Now.Minute);
            }
        }
        public bool IsBookAvailable { get; set; } = true;

    }
}