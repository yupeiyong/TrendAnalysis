using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var biz = new RecordBLL();
            var ls = biz.GetData();
            var q = from r in ls.AsQueryable() orderby r.Times select r.SeventhNum;
            var lSeven = q.ToList();
            var lFind = new List<int>();
            lFind.Add(12);
            lFind.Add(13);
            lFind.Add(14);
            var dict = Get(lSeven, lFind, 4);
            Console.ReadKey();
        }
        static SortedDictionary<int, int> Get(List<byte> lSource, List<int> lFind, int findCount)
        {
            var dict = new SortedDictionary<int, int>();
            for (int i = 0, len = lSource.Count; i < len; i++)
            {
                int j = i;
                int n = 0;
                while (j < len)
                {
                    if (lFind.Contains(lSource[j]))
                    {
                        n++;
                    }
                    else
                    {
                        if (n > 0)
                        {
                            if (dict.ContainsKey(n))
                            {
                                dict[n]++;
                            }
                            else
                            {
                                dict.Add(n, 1);
                                if (dict.Count > findCount)
                                {
                                    dict.Remove(dict.First().Key);
                                }
                            }
                        }
                        break;
                    }
                    j++;
                }
            }
            return dict;
        }
    }
}
