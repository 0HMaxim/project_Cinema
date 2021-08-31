using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.StaticClassField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.ViewModels
{
    class SelectDateTimeViewModel : BaseNotifyPropertyChanged
    {
        DateTime dateTime = DateTime.Now;
        
        public DateTime DateTime
        {
            get => dateTime;
            set
            {
                dateTime = value;
                StaticFields.CurrentDateTime = DateTime;
                Notify();
            }
        }
    }
}
