using Calculator.Model;
using Calculator.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Calculator.Model;


namespace Calculator.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<HistoryItem> HistoryList { get; } = new ObservableCollection<HistoryItem>();

        public void AddToHistory(string equation, string result)
        {
            HistoryList.Insert(0, new HistoryItem { Equation = equation, Result = result });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

