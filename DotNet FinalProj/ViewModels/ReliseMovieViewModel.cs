using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.StaticClassField;
using DotNet_FinalProj.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class ReliseMovieViewModel : BaseNotifyPropertyChanged
    {
        UserControl selectMovieView;
        UserControl selectDateTimeView;
        UserControl selectCinemaHallView;
        UserControl currentReliseMovieView;
        public UserControl CurrentReliseMovieView
        {
            get => currentReliseMovieView;
            set
            {
                currentReliseMovieView = value;
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
        private Session currentSession;
        public Session CurrentSession
        {
            get => currentSession;
            set
            {
                currentSession = value;
                Notify();
            }
        }
        public ReliseMovieViewModel()
        {
            CurrentSession = new Session();
            CurrentSession.Date = DateTime.Now;

            StaticFields.CurrentSession = null;
            StaticFields.CurrentMovie = null;

            CurrentReliseMovieView = selectMovieView ?? (selectMovieView = new MoviePageView());
            MyEvent.InitMovieInvoke();
            LastButton = "Select Movie";

            SelectMovieCommand = new RelayCommand(x =>
            {
                CurrentReliseMovieView = selectMovieView ?? (selectMovieView = new MoviePageView());

                LastButton = "Select Movie";
            });

            SelectCinemaHallCommand = new RelayCommand(x =>
            {
                CurrentReliseMovieView = selectCinemaHallView ?? (selectCinemaHallView = new CinemaHallPageView());

                LastButton = "Select CinemaHall";
            });

            SelectDateTimeCommand = new RelayCommand(x =>
            {
                CurrentReliseMovieView = selectDateTimeView ?? (selectDateTimeView = new SelectDateTimeView());

                LastButton = "Select date time";
            });

            ReliseMovieCommand = new RelayCommand(x =>
            {
                if (CheckOnCorrectness())
                {
                    Task.Run(() =>
                    {
                        using (CinemaContext db = new CinemaContext())
                        {
                            Movie m = db.Movie.Find(CurrentSession.Movie.MovieID);
                            CurrentSession.Movie = m;
                            CurrentSession.CinemaHall = db.CinemaHall.Find(CurrentSession.CinemaHall.CinemaHallId);

                            for (int i = 0; i < CurrentSession.CinemaHall.CountRow; i++)
                            {
                                for (int j = 0; j < CurrentSession.CinemaHall.CountColumn; j++)
                                    db.Ticket.Add(new Ticket(i, j, CurrentSession));
                            }


                            m.Session.Add(CurrentSession);
                            db.Session.Add(CurrentSession);
                            db.SaveChanges();
                        }
                        CurrentSession = new Session();
                        CurrentSession.Date = DateTime.Now;
                    });
                    CurrentReliseMovieView = selectMovieView ?? (selectMovieView = new MoviePageView());
                    lastButton = "Select Movie";
                }
                else
                {
                    CurrentReliseMovieView = selectDateTimeView ?? (selectDateTimeView = new SelectDateTimeView());
                    lastButton = "Select date time";
                }
            });

            SetDataCommand = new RelayCommand(x =>
            {
                if (LastButton == "Select Movie")
                {
                    CurrentSession.Movie = StaticFields.CurrentMovie;
                    CurrentReliseMovieView = selectCinemaHallView ?? (selectCinemaHallView = new CinemaHallPageView());
                    LastButton = "Select CinemaHall";
                    MyEvent.InitCinemaHallInvoke();
                }
                else if (LastButton == "Select CinemaHall")
                {
                    if (StaticFields.CurrentMovie != null)
                    {
                        CurrentSession.CinemaHall = StaticFields.CurrentCinemaHall;



                        CurrentReliseMovieView = selectDateTimeView ?? (selectDateTimeView = new SelectDateTimeView());
                        LastButton = "Select date time";

                    }
                }

                else if (LastButton == "Select date time")
                    CurrentSession.Date = StaticFields.CurrentDateTime;


                Notify("CurrentSession");

            });
        }


        private bool CheckOnCorrectness()
        {
            using (CinemaContext db = new CinemaContext())
            {
                List<Session> sess = db.Session
                                       .Where(y => y.Date.Day == CurrentSession.Date.Day &&
                                              y.CinemaHall.CinemaHallId == CurrentSession.CinemaHall.CinemaHallId)
                                       .ToList();

                for (int i = 0; i < sess.Count; i++)
                {

                     if (((sess[i].Movie.Duration.Hours + sess[i].Date.Hour) >=
                         (CurrentSession.Date.Hour)) &&
                         ((sess[i].Movie.Duration.Minutes + sess[i].Date.Minute) >=
                         (CurrentSession.Date.Minute))
                         )
                    {
                        MessageBox.Show("Choose a different time, at this time another movie has already been installed");
                        return false;
                    }

                }


            }
            return true;
        }


        public ICommand ReliseMovieCommand { get; set; }
        public ICommand SelectCinemaHallCommand { get; set; }
        public ICommand SelectDateTimeCommand { get; set; }
        public ICommand SelectMovieCommand { get; set; }
        public ICommand SetDataCommand { get; set; }
    }
}
