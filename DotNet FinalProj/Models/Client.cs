using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Models
{
    public class Client
    {
        public Client()
        {
            Tickets = new HashSet<Ticket>();
        }
        public int ClientId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
