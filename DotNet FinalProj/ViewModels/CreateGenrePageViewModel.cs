using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class CreateGenrePageViewModel : BaseNotifyPropertyChanged
    {
        public string GenreName { get; set; }
        Genre genre;
        public Genre Genre
        {
            get => genre;
            set
            {
                genre = value;
                Notify();
            }
        }
        
        public CreateGenrePageViewModel()
        {
            MyEvent.InitGenreInvoke();

            Genre = new Genre();
            CreateGenre = new RelayCommand(x =>
            {
                Task.Run(() =>
                {
                    using (CinemaContext db = new CinemaContext())
                    {
                        db.Gengre.Add(Genre);

                        //if (CreateMovieViewModel.Genres == null)
                        //    CreateMovieViewModel.LoadGenres();
                        //
                        //CreateMovieViewModel.Genres.Add(Genre);

                        db.SaveChanges();
                    }
                    Genre = new Genre();
                });
            });
        }

        public ICommand CreateGenre { get; set; }
    }
}
