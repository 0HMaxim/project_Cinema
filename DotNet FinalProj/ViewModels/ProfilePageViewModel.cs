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
    class ProfilePageViewModel : BaseNotifyPropertyChanged
    {
        UserControl registrationPageView;
        UserControl loginPageView;
        UserControl currentProfileView;

        public UserControl CurrentProfileView
        {
            get => currentProfileView;
            set
            {
                currentProfileView = value;
                currentProfileView.Resources = StaticFields.Temp;
                StaticFields.CurrentProfileView = currentProfileView;
                Notify();
            }
        }
        public ProfilePageViewModel()
        {
            CurrentProfileView = new ProfilePageLoginView();

            MyEvent.ChangeView += ChangeView;

            ShowProfilePageRegistrationCommand = new RelayCommand(x =>
            {
                CurrentProfileView = registrationPageView ?? (registrationPageView = new ProfilePageRegistrationView());
            }, x => StaticFields.CurrentClient == null);

            ShowProfilePageLoginCommand = new RelayCommand(x =>
            {
                CurrentProfileView = loginPageView ?? (loginPageView = new ProfilePageLoginView());
            }, x => StaticFields.CurrentClient == null);


            LogOutCommand = new RelayCommand(x =>
            {
                StaticFields.CurrentClient = null;
                CurrentProfileView = loginPageView ?? (loginPageView = new ProfilePageLoginView());
            });

        }
        public ICommand ShowProfilePageRegistrationCommand { get; set; }
        public ICommand ShowProfilePageLoginCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public void ChangeView()
        {
            CurrentProfileView = StaticFields.CurrentProfileView;
        }
    }
}
