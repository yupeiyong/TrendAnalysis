using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Service
{
    public class Combination
    {
    }
    public class Node<T>
    {
        public List<T> Left { get; set; }
        public List<T> Right { get; set; }
    }
}
