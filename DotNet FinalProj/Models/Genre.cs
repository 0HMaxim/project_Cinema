using System.Collections.Generic;

namespace DotNet_FinalProj.Models
{
    public class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }
        public Genre(string genreName)
        {
            Movies = new HashSet<Movie>();
            GenreName = genreName;
        }
        public int GenreId { get; set; }
        public string GenreName { get; set;}
        public virtual ICollection<Movie> Movies { get; set; }
    }
}