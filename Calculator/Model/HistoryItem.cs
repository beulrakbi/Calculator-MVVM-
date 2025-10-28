using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public class HistoryItem
    {
        public string Equation { get; set; }
        public string Result { get; set; }
        public string Display => $"{Equation} \n {Result}";
    }
}
