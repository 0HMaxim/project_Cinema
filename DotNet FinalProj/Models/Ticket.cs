using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Models
{
    public class Ticket
    {
        public Ticket(int row, int column, Session session)
        {
            Row = row;
            Column = column;
            Session = session;
        }
        public Ticket() { }
        public int TicketId { get; set; }
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Session Session { get; set; }
        public virtual bool IsTaken { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
