using LibraryApi.Domain;

namespace LibraryApi
{
    public class Book
    {
        public String Name { get; set; }

        public int Id { get; set; }

        public int AuthorId { get; set; }

        public Book() 
        {
            this.Name = "";
            this.Id = 0;
            this.AuthorId = 0;
        }

        public Book(int id, Author author, String name)
        {
            this.Name = name;
            this.Id = id;
            this.AuthorId = author.Id;
        }
    }
}