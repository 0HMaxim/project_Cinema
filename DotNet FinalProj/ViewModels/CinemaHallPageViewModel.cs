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
    class CinemaHallPageViewModel : BaseNotifyPropertyChanged
    {
        private ObservableCollection<CinemaHall> cinemaHalls;
        public ObservableCollection<CinemaHall> CinemaHalls
        {
            get => cinemaHalls;
            set
            {
                cinemaHalls = value;
                Notify();
            }
        }
        bool isWasInit;

        private CinemaHall selectedCinemaHall;
        public CinemaHall SelectedCinemaHall
        {
            get => selectedCinemaHall;
            set
            {
                selectedCinemaHall = value;
                StaticFields.CurrentCinemaHall = SelectedCinemaHall;
                Notify();
            }
        }
        public CinemaHallPageViewModel()
        {
            MyEvent.InitCinemaHall -= InitCinemaHall;
            MyEvent.InitCinemaHall += InitCinemaHall;
        }

        public async void InitCinemaHall()
        {
            if (!isWasInit)
            {
                MessageBox.Show("CinemaHalls loading . . . ");
                isWasInit = true;
            }

            using (CinemaContext db = new CinemaContext())
            {
                var cinemaHalls = await db.CinemaHall.ToListAsync();
                CinemaHalls = new ObservableCollection<CinemaHall>(cinemaHalls);
            }
        }

    }
}
