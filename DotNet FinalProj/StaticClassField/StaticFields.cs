using DotNet_FinalProj.Models;
using DotNet_FinalProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DotNet_FinalProj.StaticClassField
{
    public class StaticFields
    {
        public static Window Window { get; set; }
        public static ResourceDictionary Temp { get; set; } = new ResourceDictionary();
        public static UserControl CurrentProfileView { get; set; }
        public static UserControl CurrentProfileAdminView { get; set; }
        public static UserControl CurrentMainView { get; set; }
        public static Movie CurrentMovie { get; set; }
        public static CinemaHall CurrentCinemaHall { get; set; }
        public static DateTime CurrentDateTime { get; set; }
        public static Client CurrentClient { get; set; }
        public static Session CurrentSession { get; set; }
        public static List<Ticket2> ListTicket { get; set; }
        public static int Row { get; set; }
        public static int Collumn { get; set; }
    }
}
