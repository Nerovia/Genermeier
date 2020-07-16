using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genermeier
{
    class Program
    {
        static void DrawFamily(Personmeier person)
        {
            Console.Write("O");
            // If family is even 
            for (int n = 0; n < person.partners.Length; n++)
            {
                if (person.partners.Length % 2 != 0 && person.partners.Length / 2 == n)
                    Console.Write("─┬─");
                else
                    Console.Write("───");
                Console.Write("O");
            }
        }

        static void Main(string[] args)
        {
            var person = new Personmeier();
            person.Sprout(5);

            DrawFamily(person);

        }
    }
}
