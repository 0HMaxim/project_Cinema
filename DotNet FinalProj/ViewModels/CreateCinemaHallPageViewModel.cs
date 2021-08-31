using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.StaticClassField;
using DotNet_FinalProj.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class CreateCinemaHallPageViewModel : BaseNotifyPropertyChanged
    {
        CinemaHall currentCinemaHall;
        public CreateCinemaHallPageViewModel()
        {
            CurrentCinemaHall = new CinemaHall();
            CreateCinemaHall = new RelayCommand(x =>
            {
                Task.Run(() =>
                {
                    using (CinemaContext db = new CinemaContext())
                    {
                        db.CinemaHall.Add(CurrentCinemaHall);

                        //if (CinemaHallPageViewModel.CinemaHalls == null)
                        //    CinemaHallPageViewModel.InitCinemaHall();
                        //CinemaHallPageViewModel.CinemaHalls.Add(CurrentCinemaHall);

                        db.SaveChanges();
                    }
                    CurrentCinemaHall = new CinemaHall();
                });
            });
        }
        public CinemaHall CurrentCinemaHall
        {
            get => currentCinemaHall;
            set
            {
                currentCinemaHall = value;
                Notify();
            }
        }

        public ICommand CreateCinemaHall { get; set; }
    }
}
