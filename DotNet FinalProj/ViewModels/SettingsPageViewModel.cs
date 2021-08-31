using DotNet_FinalProj.Infrastructure;
using DotNet_FinalProj.StaticClassField;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNet_FinalProj.ViewModels
{
    class SettingsPageViewModel : BaseNotifyPropertyChanged
    {
        public List<string> Themes { get; set; }
        public string selectedTheme;
        public string SelectedTheme
        {
            get => selectedTheme;
            set
            {
                selectedTheme = value;
                Notify();
            }
        }

        public SettingsPageViewModel()
        {
            Themes = new List<string>();
            Themes.Add("Light Theme");
            Themes.Add("Dark Theme");
            ChangeTheme = new RelayCommand(x =>
            {
                string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.xaml");
                SelectedTheme = SelectedTheme.Replace(" ", "");
                foreach (string file in files)
                    if (file.Contains(SelectedTheme))
                    {
                        try
                        {
                            StaticFields.Temp.Source = new Uri(file);
                            StaticFields.CurrentMainView.Resources = StaticFields.Temp;
                            StaticFields.CurrentProfileView.Resources = StaticFields.Temp;
                            StaticFields.Window.Resources = StaticFields.Temp;
                        }
                        catch (Exception ex) { }
                    }
            });
        }

        public ICommand ChangeTheme { get; set; }
    }
}
