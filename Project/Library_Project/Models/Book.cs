namespace Library_Project.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int PublishYear { get; set; }
        public int GenreId { get; set; }
        public string? Description { get; set; }
        public int CopiesCount { get; set; }

        public Author? Author { get; set; }
        public Genre? Genre { get; set; }
        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}
