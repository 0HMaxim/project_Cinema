using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.StaticClassField;
using DotNet_FinalProj.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class ProfilePageLoginViewModel : BaseNotifyPropertyChanged
    {
        public ICommand SetCurrentClient { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public PasswordBox PasswordBox { get; set; }
        public ProfilePageLoginViewModel()
        {
       

            SetCurrentClient = new RelayCommand(x =>
            {

                using (CinemaContext db = new CinemaContext())
                {
                    var pass = x as PasswordBox;

                    Password = Sha256Crypt.Sha256(pass.Password, Login);

                    StaticFields.CurrentClient = db.Client.FirstOrDefault(y => y.Password == Password &&
                                                                               y.Login == Login);

                    if (StaticFields.CurrentClient == null)
                    {
                        MessageBox.Show("Incorrect user");
                    }


                    else
                    {
                        if (StaticFields.CurrentClient.Login == "admin" && StaticFields.CurrentClient.Password == Password)
                        {
                            StaticFields.CurrentProfileView = new ProfileAdminPageView();

                            MyEvent.ChangeViewInvoke();
                        }

                        else
                        {
                            StaticFields.CurrentProfileView = new ClientView();
                           
                            MyEvent.ChangeViewInvoke();
                        }
                    }
                }

            });
        }
    }
}
