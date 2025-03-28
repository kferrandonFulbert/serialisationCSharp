using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace serialisation
{
    internal class MembreSerialisable
    {
        /// <summary>
        /// Sérialise un membre en JSON et l'écrit dans un fichier
        /// </summary>
        /// <param name="membre">Le membre à sérialiser</param>
        /// <param name="cheminFichier">Le chemin du fichier de destination</param>
        public static void SerialiseMembre(Membre membre, string cheminFichier)
        {
            try
            {
                // Options de sérialisation pour un formatage plus lisible
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // Sérialisation du membre en JSON
                string jsonMembre = JsonSerializer.Serialize(membre, options);

                // Écriture dans le fichier
                File.WriteAllText(cheminFichier, jsonMembre);

                Console.WriteLine($"Membre sérialisé avec succès dans {cheminFichier}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sérialisation : {ex.Message}");
            }
        }

        /// <summary>
        /// Désérialise un membre à partir d'un fichier JSON
        /// </summary>
        /// <param name="cheminFichier">Le chemin du fichier JSON</param>
        /// <returns>Un objet Membre désérialisé</returns>
        public static Membre DeserializeMembre(string cheminFichier)
        {
            try
            {
                // Lecture du contenu du fichier
                string jsonMembre = File.ReadAllText(cheminFichier);

                // Désérialisation du JSON en objet Membre
                Membre membre = JsonSerializer.Deserialize<Membre>(jsonMembre);

                return membre;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la désérialisation : {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Sérialise manuellement un membre en JSON
        /// </summary>
        /// <param name="membre">Le membre à sérialiser</param>
        /// <returns>Une chaîne JSON représentant le membre</returns>
        public static string SerialiseMembreManuel(Membre membre)
        {
            // Construire le JSON manuellement avec un StringBuilder
            StringBuilder jsonBuilder = new StringBuilder();

            jsonBuilder.Append("{");

            // Sérialisation du nom
            jsonBuilder.Append($"\"Nom\": \"{EchapperCaracteresSpeciaux(membre.Nom)}\",");

            // Sérialisation de l'email
            jsonBuilder.Append($"\"Email\": \"{EchapperCaracteresSpeciaux(membre.Email)}\",");

            // Sérialisation de la date d'adhésion
            jsonBuilder.Append($"\"DateAdhesion\": \"{membre.DateAdhesion:o}\",");

            // Sérialisation des activités
            jsonBuilder.Append("\"Activites\": [");
            if (membre.Activites != null && membre.Activites.Count > 0)
            {
                for (int i = 0; i < membre.Activites.Count; i++)
                {
                    jsonBuilder.Append($"\"{EchapperCaracteresSpeciaux(membre.Activites[i])}\"");
                    if (i < membre.Activites.Count - 1)
                    {
                        jsonBuilder.Append(",");
                    }
                }
            }
            jsonBuilder.Append("]");

            jsonBuilder.Append("}");

            return jsonBuilder.ToString();
        }

        /// <summary>
        /// Échappe les caractères spéciaux pour une sérialisation JSON sûre
        /// </summary>
        /// <param name="texte">Le texte à échapper</param>
        /// <returns>Le texte avec les caractères spéciaux échappés</returns>
        private static string EchapperCaracteresSpeciaux(string texte)
        {
            if (string.IsNullOrEmpty(texte))
                return texte;

            return texte
                .Replace("\\", "\\\\")   // Échappe les backslashes
                .Replace("\"", "\\\"")   // Échappe les guillemets
                .Replace("\b", "\\b")    // Backspace
                .Replace("\f", "\\f")    // Form feed
                .Replace("\n", "\\n")    // Nouvelle ligne
                .Replace("\r", "\\r")    // Retour chariot
                .Replace("\t", "\\t");   // Tabulation
        }


        /// <summary>
        /// Sérialise un membre à partir d'un string JSON
        /// </summary>
        /// <param name="jsonMembre">Le string JSON représentant un membre</param>
        /// <returns>Un objet Membre désérialisé</returns>
        public static Membre DeserializeMembreDepuisString(string jsonMembre)
        {
            try
            {
                // Désérialisation du JSON en objet Membre
                Membre membre = JsonSerializer.Deserialize<Membre>(jsonMembre);

                return membre;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la désérialisation : {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Désérialise manuellement un membre à partir d'une chaîne JSON
        /// </summary>
        /// <param name="jsonMembre">La chaîne JSON représentant un membre</param>
        /// <returns>Un objet Membre désérialisé</returns>
        public static Membre DeserialiseMembreManuel(string jsonMembre)
        {
            if (string.IsNullOrWhiteSpace(jsonMembre))
                throw new ArgumentException("La chaîne JSON est vide ou null.");

            // Nettoyer les espaces et les sauts de ligne
            jsonMembre = jsonMembre.Trim();

            // Vérifier que c'est un objet JSON valide
            if (!jsonMembre.StartsWith("{") || !jsonMembre.EndsWith("}"))
                throw new FormatException("Format JSON invalide.");

            // Enlever les accolades
            jsonMembre = jsonMembre.Substring(1, jsonMembre.Length - 2).Trim();

            // Initialiser les propriétés
            string nom = null;
            string email = null;
            DateTime dateAdhesion = DateTime.MinValue;
            List<string> activites = new List<string>();

            // Diviser les propriétés
            string[] proprietes = SeparerProprietesJson(jsonMembre);

            foreach (string propriete in proprietes)
            {
                string[] parties = propriete.Split(new[] { ':' }, 2);
                if (parties.Length != 2)
                    continue;

                string cle = parties[0].Trim().Trim('"');
                string valeur = parties[1].Trim();

                switch (cle)
                {
                    case "Nom":
                        nom = ExtraireValeurTexte(valeur);
                        break;
                    case "Email":
                        email = ExtraireValeurTexte(valeur);
                        break;
                    case "DateAdhesion":
                        // Supprimer les guillemets
                        valeur = valeur.Trim('"');
                        if (DateTime.TryParse(valeur, out DateTime date))
                        {
                            dateAdhesion = date;
                        }
                        break;
                    case "Activites":
                        // Enlever les crochets et séparer les activités
                        activites = ExtraireListeActivites(valeur);
                        break;
                }
            }

            // Créer et retourner l'objet Membre
            return new Membre(nom, email, dateAdhesion, activites);
        }
    
       /// <summary>
    /// Sépare les propriétés JSON en tenant compte des tableaux et des imbrications
    /// </summary>
    private static string[] SeparerProprietesJson(string jsonContenu)
        {
            var proprietes = new List<string>();
            int profondeur = 0;
            bool dansChaine = false;
            var constructeur = new StringBuilder();

            for (int i = 0; i < jsonContenu.Length; i++)
            {
                char c = jsonContenu[i];
                if (i > 0)
                {
                    if (c == '"' && jsonContenu[i - 1] != '\\')
                        dansChaine = !dansChaine;
                }

                if (!dansChaine)
                {
                    if (c == '{' || c == '[')
                        profondeur++;
                    else if (c == '}' || c == ']')
                        profondeur--;
                    else if (c == ',' && profondeur == 0)
                    {
                        proprietes.Add(constructeur.ToString().Trim());
                        constructeur.Clear();
                        continue;
                    }
                }

                constructeur.Append(c);
            }

            // Ajouter la dernière propriété
            if (constructeur.Length > 0)
                proprietes.Add(constructeur.ToString().Trim());

            return proprietes.ToArray();
        }

        /// <summary>
        /// Extrait la valeur texte en gérant les échappements
        /// </summary>
        private static string ExtraireValeurTexte(string valeur)
        {
            // Supprimer les guillemets et gérer les échappements
            valeur = valeur.Trim();
            if (valeur.StartsWith("\"") && valeur.EndsWith("\""))
                valeur = valeur.Substring(1, valeur.Length - 2);

            return DesechapperCaracteresSpeciaux(valeur);
        }

        /// <summary>
        /// Extrait la liste des activités à partir du JSON
        /// </summary>
        private static List<string> ExtraireListeActivites(string valeur)
        {
            var activites = new List<string>();

            // Enlever les crochets
            valeur = valeur.Trim();
            if (valeur.StartsWith("[") && valeur.EndsWith("]"))
                valeur = valeur.Substring(1, valeur.Length - 2);

            // Séparer les activités
            var activitesBrutes = valeur.Split(',');
            foreach (var activite in activitesBrutes)
            {
                string activiteNettoyee = ExtraireValeurTexte(activite.Trim());
                if (!string.IsNullOrWhiteSpace(activiteNettoyee))
                    activites.Add(activiteNettoyee);
            }

            return activites;
        }

        /// <summary>
        /// Déséchappe les caractères spéciaux
        /// </summary>
        private static string DesechapperCaracteresSpeciaux(string texte)
        {
            return texte
                .Replace("\\\"", "\"")   // Guillemets
                .Replace("\\\\", "\\")   // Backslash
                .Replace("\\b", "\b")    // Backspace
                .Replace("\\f", "\f")    // Form feed
                .Replace("\\n", "\n")    // Nouvelle ligne
                .Replace("\\r", "\r")    // Retour chariot
                .Replace("\\t", "\t");   // Tabulation
        }
    }
}