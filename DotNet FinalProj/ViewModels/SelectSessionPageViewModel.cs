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
using System.Windows;

namespace DotNet_FinalProj.ViewModels
{
    class SelectSessionPageViewModel : BaseNotifyPropertyChanged
    {
        ObservableCollection<Session> session;
        public ObservableCollection<Session> Session
        {
            get => session;
            set
            {
                session = value;
                Notify();
            }
        }

        private Session selectedSession;
        public Session SelectedSession
        {
            get => selectedSession;
            set
            {
                selectedSession = value;
                StaticFields.CurrentSession = SelectedSession;
                Notify();
            }
        }

        public SelectSessionPageViewModel()
        {
            MyEvent.InitSession -= InitSession;
            MyEvent.InitSession += InitSession;
        }

        public async void InitSession()
        {
            using (CinemaContext db = new CinemaContext())
            {
                await db.Gengre.ToListAsync();
                await db.CinemaHall.ToListAsync();
                await db.Movie.ToListAsync();
                await db.Session.ToListAsync();

                try
                {
                    var session = await db.Session.Where(x => x.Movie.MovieID == StaticFields.CurrentMovie.MovieID)
                                                               .ToListAsync();

                    Session = new ObservableCollection<Session>(session);

                    if (Session.Count() == 0)
                    {
                        MessageBox.Show("No sessions, choose another movie");
                        MyEvent.ChangeViewBookASeatInvoke();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Choose movie");
                }

               
            }
        }
    }
}
