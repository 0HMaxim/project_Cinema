using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.StaticClassField;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.ViewModels
{
    class ClientTicketsViewModel : BaseNotifyPropertyChanged
    {
        ObservableCollection<Ticket> tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get => tickets;
            set
            {
                tickets = value;
                Notify();
            }
        }

        public ClientTicketsViewModel()
        {
            MyEvent.InitClientTickets -= InitClientTickets;
            MyEvent.InitClientTickets += InitClientTickets;
        }

        public async void InitClientTickets()
        {
            using (CinemaContext db = new CinemaContext())
            {
                await db.Session.ToListAsync();
                await db.Movie.ToListAsync();
                var ticket = await db.Ticket.Where(x => x.ClientId == StaticFields.CurrentClient.ClientId).ToListAsync();
                Tickets = new ObservableCollection<Ticket>(ticket);
            }
        }
    }
}
