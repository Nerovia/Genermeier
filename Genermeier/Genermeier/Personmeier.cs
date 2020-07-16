using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genermeier
{
    public class Personmeier
    {
        static GetMeier GetMeier = new GetMeier();
        static Random Random = new Random();

        public Personmeier(Personmeier[] parents = null)
        {
            this.parents = parents;
            this.firstname = GetMeier.getFirstName();
            this.lastname = GetMeier.getLastName();
        }


        public void Sprout(int levels)
        {
            if (levels <= 0)
                return;

            CreatePartners(Random.Next(0, 3));
            CreateChildren(Random.Next(0, 10));
            if (children != null)
                foreach(var child in children)
                {
                    levels--;
                    child.Sprout(levels);
                }
        }


        public void CreateChildren(int amount)
        {
            //if (amount <= 0)
            //    return;

            //if (partners == null)
            //    return; 

            var parents = new Personmeier[partners.Length + 1];
            for (int n = 0; n < parents.Length - 1; n++)
                parents[n] = partners[n];
            parents[parents.Length - 1] = this;

            children = new Personmeier[amount];
            for (int n = 0; n < children.Length; n++)
                children[n] = new Personmeier(parents);
        }


        public void CreatePartners(int amount)
        {
            //if (amount <= 0)
            //    return;

            partners = new Personmeier[amount];
            for (int n = 0; n < partners.Length; n++)
                partners[n] = new Personmeier();
        }


        public void PrintTree(List<bool> level = null)
        {


            Console.Write(this.Name);
            if (this.partners != null)
            {
                foreach (var partner in this.partners)
                {
                    Console.Write(" & [");
                    Console.Write(partner.Name);
                    Console.Write("]");
                }
            }


            Console.WriteLine();
            if (this.children != null)
            {
                if (level is null)
                    level = new List<bool> { };
                else
                    level.Add(true);
                foreach (var child in this.children)
                {
                    foreach (var l in level)
                        if (l)
                            Console.Write(" │ ");
                        else
                            Console.Write("   ");

                    if (this.children.Last() == child)
                    {
                        Console.Write(" └ ");
                        if (level.Count > 0)
                            level[level.Count - 1] = false;
                    }
                    else
                        Console.Write(" ├ ");
                    child.PrintTree(level);
                }
                if (level.Count > 0)
                    level.RemoveAt(level.Count - 1);
                //Console.WriteLine();
            }
        }

        public string Name { get => firstname + " " + lastname;  }

        public string firstname = "mei";
        public string lastname = "er";

        public Personmeier[] children;
        public Personmeier[] partners;
        public Personmeier[] parents;
    }
}
