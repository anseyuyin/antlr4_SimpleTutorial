using System;

namespace calculatorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入算式,按回车(例如 1+2*5)");
            Console.WriteLine("输入:");

            //监听输入
            while (true) { 
                var inputStr = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputStr)) { 
                    var result = tool.calcValue(inputStr);
                    Console.WriteLine(string.Format("计算结果：{0}",result));
                }
            }
        }
    }
}
