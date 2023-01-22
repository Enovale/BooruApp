using System.Collections.ObjectModel;
using System.Linq;
using Glitonea.Mvvm;

namespace BooruApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Greeting { get; set; } = "Welcome to Avalonia!";

        public ObservableCollection<string> TestItems { get; set; }
            = new(Enumerable.Repeat<string>("https://media.discordapp.net/attachments/1053710913754103808/1064260804016357406/image.png", 9));

        public void SelectImage()
        {
            Greeting = "Button Clicked!!";
        }
    }
}