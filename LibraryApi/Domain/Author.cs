namespace LibraryApi.Domain
{
    public class Author
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Id { get; set; }

        public Author()
        {
            Id = 0;
            Surname = "";
            Name = "";
        }

        public Author(int id, string surname, string name)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

        public ICollection<Book> Books { get; set; } = null!;
    }
}
