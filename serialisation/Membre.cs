using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serialisation
{
    internal class Membre
    {
        public string Nom { get; set; }
        public string Email { get; set; }
        public DateTime DateAdhesion { get; set; }
        public List<string> Activites { get; set; }

        // Autre syntaxe possible
        /*
         * private string _categorie;
         * 
         *     public string Categorie
                {
                    get { return _categorie; }
                    set { _categorie = value; }
                }
         * 
         */

        public Membre(string nom, string email, DateTime dateAdhesion, List<string> activites)
        {
            Nom = nom;
            Email = email;
            DateAdhesion = dateAdhesion;
            Activites = activites;
        }
    }
}
