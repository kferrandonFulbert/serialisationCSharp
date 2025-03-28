# Projet de Sérialisation

Ce projet illustre l'utilisation de la sérialisation en C#. Il contient trois composants principaux : `Membre`, `MembreSerialisable` et `Program.cs`.

## Classes

### Membre
La classe `Membre` représente un objet de base avec des propriétés non sérialisables. Elle sert de modèle pour les données qui ne sont pas directement sérialisées.

### MembreSerialisable
La classe `MembreSerialisable` est une classe distincte qui ajoute des fonctionnalités pour permettre la sérialisation d'un objet `Membre`. Elle utilise des attributs comme `[Serializable]` ou des mécanismes personnalisés pour rendre ses données sérialisables.

## Program.cs
Le fichier `Program.cs` contient le point d'entrée de l'application. Il démontre :
- La création d'instances de `Membre` et `MembreSerialisable`.
- La sérialisation et la désérialisation des objets.
- L'affichage des résultats pour valider le processus.

## Exécution
Pour exécuter le projet :
1. Compilez le code.
2. Lancez l'application pour observer la sérialisation et la désérialisation en action.

## Objectif
Ce projet est conçu pour apprendre et expérimenter avec la sérialisation en C#.

## Exercice

### Objectif
Faites évoluer le projet en modifiant les classes `Membre` et `MembreSerialisable` pour inclure de nouvelles fonctionnalités, et ajoutez une fonctionnalité pour écrire les données dans un fichier JSON.

### Instructions
1. **Ajoutez des propriétés à la classe `Membre` :**
    - Une propriété `Catégorie` (par exemple : benjamin, minime, senior...).
    - Une propriété `Classement` pour représenter le classement du membre.

2. **Mettez à jour la classe `MembreSerialisable` :**
    - Assurez-vous que les nouvelles propriétés `Catégorie` et `Classement` de `Membre` soient correctement sérialisées et désérialisées.
    - Si nécessaire, adaptez les mécanismes de sérialisation pour inclure ces nouvelles données.

3. **Ajoutez une fonctionnalité d'écriture dans un fichier JSON :**
    - Dans le programme principal (`Program.cs`), ajoutez une méthode pour sérialiser les données des membres dans un fichier `membre.json`.
    - Utilisez une bibliothèque comme `System.Text.Json` ou `Newtonsoft.Json` pour effectuer cette opération.

4. **Testez vos modifications :**
    - Créez des instances de `Membre` avec les nouvelles propriétés.
    - Sérialisez et désérialisez ces instances.
    - Vérifiez que les données des nouvelles propriétés sont correctement conservées.
    - Assurez-vous que le fichier `membre.json` est correctement généré et contient les données attendues.

### Résultat attendu
Une version mise à jour des classes `Membre` et `MembreSerialisable` qui prend en charge les nouvelles propriétés, avec une fonctionnalité supplémentaire pour écrire les données dans un fichier JSON, et des tests démontrant leur bon fonctionnement.
