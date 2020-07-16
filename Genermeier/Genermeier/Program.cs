using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genermeier
{
    class Program
    {

        static void Main(string[] args)
        {
            var person = new Personmeier();
            person.Sprout(10);

            person.PrintTree();

            while (true);
        }
    }
}
