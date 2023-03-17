using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.StaticClassField;
using DotNet_FinalProj.Views;

namespace DotNet_FinalProj.ViewModels
{
    class MainViewModel : BaseNotifyPropertyChanged
    {
        UserControl currentView;
        UserControl profilePageView;
        UserControl bookAMoviePageView;



        UserControl settingsPageView;
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                CurrentView.Resources = StaticFields.Temp;
                StaticFields.CurrentMainView = currentView;
                Notify();
            }
        }


        public MainViewModel()
        {
         
            Task.Run(() =>
            {
                using (CinemaContext db = new CinemaContext())
                {
                    if ((db.Gengre.FirstOrDefault(x => x.GenreName == "Drama")) == null)
                        db.Gengre.Add(new Genre("Drama"));
                    if ((db.Gengre.FirstOrDefault(x => x.GenreName == "Comedy")) == null)
                        db.Gengre.Add(new Genre("Comedy"));
                    if ((db.Gengre.FirstOrDefault(x => x.GenreName == "Romance")) == null)
                        db.Gengre.Add(new Genre("Romance"));
                    if ((db.Gengre.FirstOrDefault(x => x.GenreName == "Detective")) == null)
                        db.Gengre.Add(new Genre("Detective"));
                    if ((db.Gengre.FirstOrDefault(x => x.GenreName == "Action")) == null)
                        db.Gengre.Add(new Genre("Action"));
                    if ((db.Gengre.FirstOrDefault(x => x.GenreName == "Thriller")) == null)
                        db.Gengre.Add(new Genre("Thriller"));
                    if ((db.Gengre.FirstOrDefault(x => x.GenreName == "Fantasy")) == null)
                        db.Gengre.Add(new Genre("Fantasy"));

                    string password = Sha256Crypt.Sha256("admin", "admin");
                    if (db.Client.FirstOrDefault(x => x.Login == "admin" && x.Password == password) == null)
                        db.Client.Add(new Client { Login = "admin", Password = password });

                    db.SaveChanges();
                }
            });

            StaticFields.Temp.Source = new Uri(Environment.CurrentDirectory + "\\LightTheme.xaml");
            StaticFields.Window.Resources = StaticFields.Temp;


            profilePageView = new ProfilePageView();
            CurrentView = profilePageView;



            ProfilePageCommand = new RelayCommand(x =>
            {
                CurrentView = profilePageView ?? (profilePageView = new ProfilePageView());
            });

            ShowBookAMoviePageViewPageCommand = new RelayCommand(x =>
            {
                CurrentView = bookAMoviePageView ?? (bookAMoviePageView = new  BookASeatPageView());
            });

            ShowSettingsPageCommand = new RelayCommand(x =>
            {
                CurrentView = settingsPageView ?? (settingsPageView = new SettingsPageView());
            });

        }
        public ICommand ProfilePageCommand { get; set; }
        public ICommand ShowBookAMoviePageViewPageCommand { get; set; }
        public ICommand ShowSettingsPageCommand { get; set; }
    }
}
