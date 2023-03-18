using DotNet_FinalProj.Context;
using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.Models;
using DotNet_FinalProj.StaticClassField;
using DotNet_FinalProj.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class ProfilePageRegistrationViewModel : BaseNotifyPropertyChanged
    {
        public ICommand RegistrationCommand { get; set; }

        Client client;
        public Client Client
        {
            get => client;
            set
            {
                client = value;
                Notify();
            }
        }
        bool IsExeprion;
        public ProfilePageRegistrationViewModel()
        {
            Client = new Client();
            RegistrationCommand = new RelayCommand(x =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        var passwordBox = x as PasswordBox;
                        var password = passwordBox.Password;
                        Client.Password = password;

                        MailAddress from = new MailAddress("teran5656@gmail.com");
                        MailAddress to = new MailAddress(Client.Email);
                        MailMessage m = new MailMessage(from, to);
                        m.Subject = "Регистрация в кинотеатре D2Top";
                        m.Body = $"Здраствуйте {Client.Name}, Вы успешно создали аккаунт в кинотеатре D2Top\nLogin {Client.Login}\nPassword {Client.Password}";
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.Credentials = new NetworkCredential("teran5656@gmail.com", "jforcjoaskkasyuf");
                        smtp.EnableSsl = true;
                        smtp.Send(m);

                        using (CinemaContext db = new CinemaContext())
                        {
                            Client.Password = Sha256Crypt.Sha256(Client.Password, Client.Login);
                            db.Client.Add(Client);
                            db.SaveChanges();
                            StaticFields.CurrentClient = db.Client.FirstOrDefault(y => y.Login == Client.Login &&
                                                                                  y.Password == Client.Password &&
                                                                                  y.Name == Client.Name &&
                                                                                  y.Email == Client.Email);
                        }
                    }
                    catch (Exception ex)
                    {
                        IsExeprion = true;
                        MessageBox.Show("Incorrect Data");
                    }
                }).Wait();

                if(!IsExeprion)
                {
                    Client = new Client();
                    StaticFields.CurrentProfileView = new ClientView();
                    MyEvent.ChangeViewInvoke();
                }
            });
        }
    }
}
