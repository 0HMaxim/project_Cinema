using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.StaticClassField;
using DotNet_FinalProj.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class ProfileAdminPageViewModel : BaseNotifyPropertyChanged
    {
        UserControl createMoviePageView;
        UserControl createGenrePageView;
        UserControl reliseMoviePageView;
        UserControl createCinemaHallPageView;
        UserControl currentProfileAdminView;
        
        public UserControl CurrentProfileAdminView
        {
            get => currentProfileAdminView;
            set
            {
                currentProfileAdminView = value;
                currentProfileAdminView.Resources = StaticFields.Temp;
                StaticFields.CurrentProfileAdminView = currentProfileAdminView;
                Notify();
            }
        }

        public ProfileAdminPageViewModel()
        {
            CreateMovieCommand = new RelayCommand(x =>
            {
                CurrentProfileAdminView = createMoviePageView ?? (createMoviePageView = new CreateMoviePageView());
                MyEvent.InitGenreInvoke();
            });

            CreateGenreCommand = new RelayCommand(x =>
            {
                CurrentProfileAdminView = createGenrePageView ?? (createGenrePageView = new CreateGenrePageView());
            });

            CreateCinemaHallCommand = new RelayCommand(x =>
            {
                CurrentProfileAdminView = createCinemaHallPageView ?? (createCinemaHallPageView = new CreateCinemaHallPageView());
            });

            ReliseMoviePageCommand = new RelayCommand(x =>
            {
                CurrentProfileAdminView = reliseMoviePageView ?? (reliseMoviePageView = new ReliseMoviePageView());
            });

        }
        
        public ICommand ReliseMoviePageCommand { get; set; }
        public ICommand CreateMovieCommand { get; set; }
        public ICommand CreateGenreCommand { get; set; }
        public ICommand CreateCinemaHallCommand { get; set; }
    }
}
