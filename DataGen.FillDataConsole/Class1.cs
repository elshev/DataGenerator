using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGen.FillDataConsole
{
    public class Class1 : object
    {
        public void A()
        {
            object o = new object();
            Type t = o.GetType();
            Console.WriteLine(t.Name);
            Console.WriteLine(t);
            Console.WriteLine(t.ToString());
            Console.WriteLine(t.IsClass);
            o = "alkjsdflk";
            t = o.GetType();
            Console.WriteLine(t.Name);
            Console.WriteLine(t);
            Console.WriteLine(t.ToString());
            Console.WriteLine(t.IsClass);
            o = new FirstName();
            t = o.GetType();
            Console.WriteLine(t.Name);
            Console.WriteLine(t);
            Console.WriteLine(t.Assembly);
            Console.WriteLine(t.FullName);
            Console.WriteLine(t.IsClass);
            o = 10;
            t = o.GetType();
            Console.WriteLine(t.Name);
            Console.WriteLine(t);
            Console.WriteLine(t.ToString());
            Console.WriteLine(t.Assembly);
            Console.WriteLine(t.FullName);
            Console.WriteLine(t.IsClass);

        }
    }
}
