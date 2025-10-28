using Calculator.Common;
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
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// standard
        /// </summary>
        public StandardViewModel StandardViewModel { get; }
        /// <summary>
        /// HistoryViewModel
        /// </summary>
        public HistoryViewModel HistoryViewModel{ get;}

        public MainWindowViewModel()
        {
            StandardViewModel = new StandardViewModel();
            HistoryViewModel = new HistoryViewModel();

            StandardViewModel.resultHistory += (s, e) =>
            {
                HistoryViewModel.AddToHistory(e.Equation, e.Result);
            };

        }
     
    }

}
