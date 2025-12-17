[ReadMe.md](https://github.com/user-attachments/files/24201851/ReadMe.md)
# MeteoLab 2025 - Application d'Analyse Météorologique

## Description
MeteoLab est une application Uno Platform permettant la saisie, l'importation et l'analyse statistique de données météorologiques (Température, Humidité, Précipitations).

## Guide d'Utilisation

### 1. Démarrage
L'application s'ouvre sur un menu principal proposant deux options :
- **Saisir des nouvelles données** : Pour entrer des relevés manuels ou importer un fichier.
- **Voir les statistiques** : Pour analyser les données enregistrées.

### 2. Saisie de données
- **Mode Manuel** : Remplissez les champs (Date, Température, Humidité, Précipitations) et cliquez sur "Enregistrer".
- **Importation CSV** : Cliquez sur le bouton bleu "📂 Importer CSV (Demo)" pour charger automatiquement le jeu de données de test inclus (Période Déc 2025 - Jan 2026).
- **Validation** : En cas d'erreur (ex: humidité > 100%), un message rouge s'affiche et l'enregistrement est bloqué.

### 3. Analyse Statistique
- Sélectionnez une **Date de début** et une **Date de fin**.
- Cliquez sur **Calculer**.
- L'application affiche de manière asynchrone (sans geler l'interface) :
  - La Moyenne
  - La Médiane
  - Le Minimum et Maximum
  - L'Écart-Type
