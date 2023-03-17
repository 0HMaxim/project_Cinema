using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DotNet_FinalProj.Infrastructure
{
    public class MyEvent
    {
        public static event Action ChangeView;
        public static void ChangeViewInvoke()
        {
            ChangeView?.Invoke();
        }


        public static event Action InitGenre;
        public static void InitGenreInvoke()
        {
            InitGenre?.Invoke();
        }


        public static event Action InitMovie;
        public static void InitMovieInvoke()
        {
            InitMovie?.Invoke();
        }

        public static event Action InitClientTickets;
        public static void InitClientTicketsInvoke()
        {
            InitClientTickets?.Invoke();
        }
        






        public static event Action InitSession;
        public static void InitSessionInvoke()
        {
            InitSession?.Invoke();
        }



        public static event Action InitTicket;
        public static void InitTicketInvoke()
        {
            InitTicket?.Invoke();
        }






        public static event Action ChangeViewBookASeat;
        public static void ChangeViewBookASeatInvoke()
        {
            ChangeViewBookASeat?.Invoke();
        }



        public static event Action InitCinemaHall;
        public static void InitCinemaHallInvoke()
        {
            InitCinemaHall?.Invoke();
        }

    }
}
