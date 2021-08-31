using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.StaticClassField;
using DotNet_FinalProj.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class BookASeatPageViewModel : BaseNotifyPropertyChanged
    {
        UserControl selectMovieView;
        UserControl selectSession;
        UserControl selectSeat;
        UserControl currentView;
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                Notify();
            }
        }
        private string lastButton;
        public string LastButton
        {
            get => lastButton;
            set
            {
                lastButton = value;
                Notify();
            }
        }
        private Ticket currentTicket;
        public Ticket CurrentTicket
        {
            get => currentTicket;
            set
            {
                currentTicket = value;
                Notify();
            }
        }
        public BookASeatPageViewModel()
        {
            CurrentTicket = new Ticket();
            CurrentTicket.Session = new Session();
            StaticFields.CurrentSession = null;
            StaticFields.CurrentMovie = null;

            MyEvent.ChangeViewBookASeat += ChangeViewBookASeat;

            CurrentView = selectMovieView ?? (selectMovieView = new MoviePageView());
            MyEvent.InitMovieInvoke();
            LastButton = "Select Movie";

            SelectMovieCommand = new RelayCommand(x =>
            {
                CurrentView = selectMovieView ?? (selectMovieView = new MoviePageView());

                LastButton = "Select Movie";
            });

            SelectSessionCommand = new RelayCommand(x =>
            {
                CurrentView = selectSession ?? (selectSession = new SelectSessionPageView());

                LastButton = "Select Session";
            });

            SelectSeatCommand = new RelayCommand(x =>
            {
                CurrentView = selectSeat ?? (selectSeat = new SelectSeatPageView());

                LastButton = "Select Seats";
            });

            SetDataCommand = new RelayCommand(x =>
            {
                if (LastButton == "Select Movie")
                {
                    CurrentTicket.Session.Movie = StaticFields.CurrentMovie;
                    CurrentView = selectSession ?? (selectSession = new SelectSessionPageView());
                    LastButton = "Select Session";
                    MyEvent.InitSessionInvoke();
                }
                else if (LastButton == "Select Session")
                {
                    if (StaticFields.CurrentMovie != null)
                    {
                        CurrentTicket.Session = StaticFields.CurrentSession;
                        CurrentView = selectSeat ?? (selectSeat = new SelectSeatPageView());
                        MyEvent.InitTicketInvoke();


                        LastButton = "Select Seats";

                        StaticFields.ListTicket = new List<Ticket2>();
                    }
                }
                else if (LastButton == "Select Seats")
                {
                    if (StaticFields.ListTicket.Count() > 0)
                    {
                        Task.Run(() =>
                        {
                            using (CinemaContext db = new CinemaContext())
                            {
                                Client client = null;
                                if (StaticFields.CurrentClient != null)
                                    client = db.Client.FirstOrDefault(y => y.ClientId == StaticFields.CurrentClient.ClientId);

                                foreach (var item in StaticFields.ListTicket)
                                {
                                    var t = db.Ticket
                                          .FirstOrDefault(y => y.TicketId == item.Tickett.TicketId);

                                    t.IsTaken = true;

                                    if (client != null)
                                        client.Tickets.Add(t);

                                }
                                db.SaveChanges();

                            }
                            StaticFields.ListTicket = new List<Ticket2>();

                        });
                    }
                    CurrentTicket = new Ticket();
                    CurrentTicket.Session = new Session();
                    StaticFields.CurrentSession = null;
                    StaticFields.CurrentMovie = null;
                    MyEvent.InitMovieInvoke();
                    MyEvent.ChangeViewBookASeatInvoke();
                    LastButton = "Select Movie";


                }

                Notify("CurrentTicket");
            });

        }

        public void ChangeViewBookASeat()
        {
            CurrentView = selectMovieView ?? (selectMovieView = new MoviePageView());
            LastButton = "Select Movie";

        }

        public ICommand SelectMovieCommand { get; set; }
        public ICommand SelectSessionCommand { get; set; }
        public ICommand SelectSeatCommand { get; set; }
        public ICommand SetDataCommand { get; set; }
    }
}
