using MonkeysMVVM.Models;
using MonkeysMVVM.Services;
using MonkeysMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonkeysMVVM.ViewModels
{
    public class MonkeyPageViewModel : ViewModel
    {
        private string country;
        private int count;
        public string Country { get { return country; } set { country = value; OnPropertyChanged(); ((Command)MonkeyPagecommand).ChangeCanExecute(); } }
        public int Count { get { return count; } set { count = value; OnPropertyChanged(); } }
        public ICommand MonkeyPagecommand { get; set; }
        private Monkey monkey;
        public string Name { get { return monkey.Name; } }
        public string ImageUrl { get { return monkey.ImageUrl; } }
        public MonkeyPageViewModel()
        {
            monkey = new Monkey() { Name = "אין קופים כרגע" };
            MonkeyPagecommand = new Command(FindMonkeys, () => Country != null&&!String.IsNullOrEmpty(Country));
        }

        private void FindMonkeys()
        {
            MonkeysService service=new MonkeysService();
            List<Monkey> lst = service.FindMonkeysByLocation(Country);
            if (lst.Count > 0)
            {
                monkey = lst[0];
            }
            else
                monkey = new Monkey() { Name = "אין קופים להצגה" };
            Count=lst.Count;
            RefreshData();
            Country = null;

        }
        private void RefreshData()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged(nameof(ImageUrl));
        }
    }
}
