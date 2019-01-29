using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Model
{
    public class Exercise
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Repetition { get; set; }
        public bool IfSeconds { get; set; }
    }
}
