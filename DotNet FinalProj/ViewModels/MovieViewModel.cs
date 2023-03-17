using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.Repositories;
using DotNet_FinalProj.StaticClassField;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels 
{
    class MovieViewModel : BaseNotifyPropertyChanged
    {
        private ObservableCollection<Movie> movie;
        public ObservableCollection<Movie> Movie
        {
            get => movie;
            set
            {
                movie = value;
                Notify();
            }
        }
        private bool isWasInit;

        private Movie selectedMovie;
        public Movie SelectedMovie
        {
            get => selectedMovie;
            set
            {
                selectedMovie = value;
                StaticFields.CurrentMovie = SelectedMovie;
                Notify();
            }
        }
        public MovieViewModel()
        {
            MyEvent.InitMovie -= InitMovie;
            MyEvent.InitMovie += InitMovie;
        }

        public async void InitMovie()
        {
            if (!isWasInit)
            {
                MessageBox.Show("Movies loading . . . ");
                isWasInit = true;
            }
            using (CinemaContext db = new CinemaContext())
            {
                await db.Session.ToListAsync();
                await db.CinemaHall.ToListAsync();
                await db.Gengre.ToListAsync();
                var movies = await db.Movie.ToListAsync();
                Movie = new ObservableCollection<Movie>(movies);
            }

            if (Movie.Count == 0)
            {
                MessageBox.Show("No movies");
            }
        }
    }
}
