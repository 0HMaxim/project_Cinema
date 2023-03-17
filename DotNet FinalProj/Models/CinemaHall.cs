using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Models
{
    public class CinemaHall
    {
        public CinemaHall()
        {
            Session = new HashSet<Session>();
        }
        public int CinemaHallId { get; set; }
        public string HallName { get; set; }
        public int CountRow { get; set; }
        public int CountColumn { get; set; }
        public string ImageURL { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
