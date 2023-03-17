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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DotNet_FinalProj.ViewModels
{
    class SelectSeatPageViewModel : BaseNotifyPropertyChanged
    {
        ObservableCollection<Tickets> ticket;
        public ObservableCollection<Tickets> Ticket
        {
            get => ticket;
            set
            {
                ticket = value;
                Notify();
            }
        }

        public SelectSeatPageViewModel()
        {
            MyEvent.InitTicket -= InitTicket;
            MyEvent.InitTicket += InitTicket;

            SelectSeatCommad = new RelayCommand(x =>
            {
                Ticket2 ticket = x as Ticket2;

                ticket.Tickett.IsTaken = !ticket.Tickett.IsTaken;

                StaticFields.ListTicket.Add(ticket);

                if (StaticFields.ListTicket.Where(y => y.Tickett.TicketId == ticket.Tickett.TicketId).Count() >= 2)
                    StaticFields.ListTicket.Remove(ticket);


                ticket.Color = new SolidColorBrush(Color.FromArgb(255, 138, 43, 226));
                if (ticket.Tickett.IsTaken)
                    ticket.Color = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

            }, x =>
            {
                Ticket2 ticket = x as Ticket2;

                Brush clr = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));

                if (ticket.Color.ToString() == clr.ToString())
                    return false;

                return true;
            });
        }

        public void InitTicket()
        {
            Ticket = new ObservableCollection<Tickets>();

            using (CinemaContext db = new CinemaContext())
            {
                var ticket = db.Ticket.Where(x => x.Session.SessionId == StaticFields.CurrentSession.SessionId)
                                            .ToList();

                ObservableCollection<Tickets> r = new ObservableCollection<Tickets>();

                int row = ticket.Max(x => x.Row);
                int collumn = ticket.Max(x => x.Column);


                for (int i = 0; i <= row; i++)
                {
                    r.Add(new Tickets());
                    r[i].ListTckt = new List<Ticket2>();
                    for (int j = 0; j <= collumn; j++)
                    {
                        var t = ticket.FirstOrDefault(x => x.Row == i &&
                                                           x.Column == j);



                        r[i].ListTckt.Add(new Ticket2
                        {
                            Tickett = t,
                        });

                        r[i].ListTckt[j].Color = new SolidColorBrush(Color.FromArgb(255, 138, 43, 226));

                        if (r[i].ListTckt[j].Tickett.IsTaken)
                            r[i].ListTckt[j].Color = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));

                    }
                    r[i].Row = i + 1;
                }

                Ticket = new ObservableCollection<Tickets>(r);
            }

        }

        public ICommand SelectSeatCommad { get; set; }
    }

    public class Tickets : BaseNotifyPropertyChanged
    {
        public List<Ticket2> ListTckt { get; set; }
        public int Row { get; set; }   
    }                                  
                                       
    public class Ticket2 : BaseNotifyPropertyChanged
    {                                  
       public Ticket Tickett { get; set; }
       Brush color;         
       public Brush Color
       {
           get => color;
           set
           {
               color = value;
               Notify();
           }
       }
    }
}
