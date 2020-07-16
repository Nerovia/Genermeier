using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genermeier
{
    public class Personmeier
    {
        static Random Random = new Random();

        public Personmeier(Personmeier[] parents = null)
        {
            this.parents = parents;
        }


        public void Sprout(int levels)
        {
            if (levels <= 0)
                return;

            CreatePartners(Random.Next(0, 6));
            CreateChildren(Random.Next(0, 5));
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

        public string name;
        public string surname;

        public Personmeier[] children;
        public Personmeier[] partners;
        public Personmeier[] parents;
    }
}
