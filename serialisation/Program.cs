
// Création d'un membre
using serialisation;

List<string> activites = new List<string> { "Yoga", "Cours de langues", "Tennis" };
Membre membre = new Membre("Jean Dupont", "jean.dupont@email.com", DateTime.Now, activites);

// Sérialisation manuelle
string jsonMembreManuel = MembreSerialisable.SerialiseMembreManuel(membre);
Console.WriteLine("JSON Manuel :");
Console.WriteLine(jsonMembreManuel);

// Désérialisation manuelle
Membre membreDeserialiseManuel = MembreSerialisable.DeserialiseMembreManuel(jsonMembreManuel);
Console.WriteLine($"\nMembre désérialisé manuellement : {membreDeserialiseManuel.Nom}, {membreDeserialiseManuel.Email}");