using DotNet_FinalProj.Infrastructure;
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
    class ClientViewModel : BaseNotifyPropertyChanged
    {
        UserControl currentView;
        UserControl movieView;
        UserControl ticketView;
        public UserControl CurrentView
        {
            get => currentView;

            set
            {
                currentView = value;
                Notify();
            }
        }

        public ClientViewModel()
        {
            ShowTicketViewCommand = new RelayCommand(x =>
            {
                CurrentView = ticketView ?? (ticketView = new ClientTicketsView());
                MyEvent.InitClientTicketsInvoke();
            });

            ShowMovieViewCommand = new RelayCommand(x =>
            {
                CurrentView = movieView ?? (movieView = new BookASeatPageView());
            });


        }

        public ICommand ShowTicketViewCommand { get; set; }
        public ICommand ShowMovieViewCommand { get; set; }

    }
}
