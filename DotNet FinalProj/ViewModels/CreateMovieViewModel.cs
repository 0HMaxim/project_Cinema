using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DotNet_FinalProj.ViewModels
{
    class CreateMovieViewModel : BaseNotifyPropertyChanged
    {
        ObservableCollection<Genre> genres;
        public ObservableCollection<Genre> Genres
        {
            get => genres;
            set
            {
                genres = value;
                Notify();
            }
        }

        private Movie currentMovie;
        public CreateMovieViewModel()
        {
            MyEvent.InitGenre -= InitGenres;
            MyEvent.InitGenre += InitGenres;

            CurrentMovie = new Movie();

            SelectedGenres = new List<Genre>();

            CreateMovieCommand = new RelayCommand(x =>
            {
                Task.Run(() =>
                {
                    if (CurrentMovie.Rating != 0 && TimeSpan.Compare(CurrentMovie.Duration, new TimeSpan()) != 0 &&
                    (CurrentMovie.Duration.Hours!=0|| CurrentMovie.Duration.Minutes!=0))
                    {
                        using (CinemaContext db = new CinemaContext())
                        {
                            List<Genre> newSelectedGenres = new List<Genre>();

                            foreach (var item in SelectedGenres)
                            {
                                newSelectedGenres.Add(db.Gengre.Find(item.GenreId));
                            }

                            CurrentMovie.Genres = newSelectedGenres;
                            db.Movie.Add(CurrentMovie);

                            db.SaveChanges();
                        }
                        CurrentMovie = new Movie();

                 
                        SelectedGenres = new List<Genre>();
                        MyEvent.InitGenreInvoke();

                        MyEvent.InitMovieInvoke();
                    }
                    else
                        MessageBox.Show("Please enter correct data");
                });
            });
            CheckBoxCommand = new RelayCommand(x =>
            {
                Genre genre = x as Genre;

                if (SelectedGenres.Contains(genre))
                    SelectedGenres.Remove(genre);
                else
                    SelectedGenres.Add(genre);

                Notify("SelectedGenres");
            });
        }

        public void InitGenres()
        {
            Task.Run(() =>
            {
                using (CinemaContext db = new CinemaContext())
                {
                    Genres = new ObservableCollection<Genre>(db.Gengre);
                }
            });
        }
        private List<Genre> selectedGenres;

        public List<Genre> SelectedGenres
        {
            get => selectedGenres;
            set
            {
                selectedGenres = value;
                Notify();
            }
        }

        public Movie CurrentMovie
        {
            get => currentMovie;
            set
            {
                currentMovie = value;
                Notify();
            }
        }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public ICommand CreateMovieCommand { get; set; }
        public ICommand CheckBoxCommand { get; set; }
    }
}
