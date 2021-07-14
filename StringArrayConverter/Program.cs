using System;
using System.Collections.Generic;
using System.Linq;

namespace StringArrayConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = new string[] { "ток", "рост", "кот", "торс", "Кто", "фывап", "рок" };
            var res=Converter(example);
        }
        static string[][] Converter(string[] input)
        {
            var linput=input.ToList();
            List<string[]> result = new List<string[]>();
            
            var d=input
                .Select((val,idx)=> new { val, idx })
                .ToDictionary(pair => pair.idx, pair => pair.val)
                .Select((s,index)=>new string ( s.Value.OrderBy(c => c).ToArray()));
            var temp = input
                .Select((x, idx) =>
                {
                    var val = new string(x.OrderBy(c => c).ToArray()).ToLower();
                    return new { val, idx };
                })
                .OrderBy(v => v.val)
                .ToDictionary(pair => pair.idx, pair => pair.val);
            var tempList = new List<string>();
            var tempResList = new List<string>();
            foreach (var s in temp)
            {
                if (tempList.Count > 0)
                {
                    if (s.Value != tempList.Last())
                    {
                        result.Add(tempResList.ToArray());
                        tempList.Clear();
                        tempResList.Clear();
                    }
                }
                tempList.Add(s.Value);
                tempResList.Add(input[s.Key]);
            }
            result.Add(tempResList.ToArray());
            return result.OrderByDescending(a=>a.Length).ToArray();
        }
    }
}
