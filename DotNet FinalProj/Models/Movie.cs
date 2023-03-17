using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Models
{
    public class Movie 
    {
        public Movie()
        {
            Genres = new HashSet<Genre>();
            Session = new HashSet<Session>();
        }
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public string PosterURL { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Session> Session { get; set; }
        //public virtual ICollection<Producer> Producer { get; set; }
    }
}
