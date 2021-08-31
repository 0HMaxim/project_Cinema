using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Models
{
    public class Session
    {
        public Session() { }
        public int SessionId{ get; set; }
        public DateTime Date { get; set; }
        public virtual CinemaHall CinemaHall { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
