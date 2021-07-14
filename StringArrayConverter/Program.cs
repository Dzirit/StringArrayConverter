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
            Console.WriteLine("[");
            foreach (var sa in res)
            {
                Console.Write($" [");
                foreach (var s in sa)
                {
                    Console.Write($"{s}");
                    if (s != sa[sa.Length - 1])
                        Console.Write(",");
                }
                Console.WriteLine("],");
            }
            Console.WriteLine("]");
        }
        static string[][] Converter(string[] input)
        {
            List<string[]> result = new List<string[]>();
           
            var temp = input
                .Select((x, idx) =>
                {
                    var val = new string(x.OrderBy(c => c).ToArray()).ToLower();//сортируем все символы в строках
                    return new { val, idx };
                })
                .OrderBy(v => v.val)//сортируем строки
                .ToDictionary(pair => pair.idx, pair => pair.val);//используем словарь для сохранения начальных индексов

            var tempList = new List<string>();//временный список для отсортированных значений
            var tempResList = new List<string>();//результирующий вложенный список

            foreach (var s in temp)
            {//Заполняем вложенные списки и добавляем их в результирующий
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
