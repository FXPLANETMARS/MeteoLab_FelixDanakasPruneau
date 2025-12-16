using System;
using System.Linq; 
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace MeteoLab_FelixDanakasPruneau.Presentation;

public sealed partial class StatsPage : Page
{
    public StatsPage()
    {
        this.InitializeComponent();

       
        DateDebut.Date = DateTimeOffset.Now.AddDays(-30); 
        DateFin.Date = DateTimeOffset.Now; 
    }

    private async void Calculer_Click(object sender, RoutedEventArgs e)
    {
        MessageErreur.Text = "";

        // 1. Validation dates
        if (DateFin.Date < DateDebut.Date)
        {
            MessageErreur.Text = "Erreur : La date de fin doit être après la date de début.";
            ResetAffichage();
            return;
        }

        // On désactive le bouton pour éviter de cliquer 2 fois pendant le calcul
        var bouton = sender as Button;
        if (bouton != null) bouton.IsEnabled = false;

        try
        {
            var debut = DateDebut.Date;
            var fin = DateFin.Date;

            // 2. On lance le calcul sur un autre thread (ASYNCHRONE = 10 points !)
            var resultats = await Task.Run(() =>
            {
                // --- TOUT CECI SE PASSE EN ARRIÈRE-PLAN ---

                // a. Filtrage
                var listeFiltree = GestionMeteo.Historique
                    .Where(d => d.Date.Date >= debut && d.Date.Date <= fin)
                    .Select(d =>
                    {
                        // On convertit en double ici pour être tranquille
                        double.TryParse(d.Temperature, out double val);
                        return val;
                    })
                    .ToList();

                if (listeFiltree.Count == 0) return null; // Pas de données

                // b. Calculs de base
                double moy = listeFiltree.Average();
                double min = listeFiltree.Min();
                double max = listeFiltree.Max();

                // c. Écart-Type
                double sommeCarres = listeFiltree.Sum(val => Math.Pow(val - moy, 2));
                double ecartType = Math.Sqrt(sommeCarres / listeFiltree.Count);

                // d. Médiane (NOUVEAU !)
                var listeTriee = listeFiltree.OrderBy(x => x).ToList();
                double mediane = 0;
                int count = listeTriee.Count;
                if (count % 2 == 0)
                    mediane = (listeTriee[count / 2 - 1] + listeTriee[count / 2]) / 2.0;
                else
                    mediane = listeTriee[count / 2];

                // On retourne un petit paquet avec tous les résultats
                return new { Moy = moy, Min = min, Max = max, Ecart = ecartType, Med = mediane };
            });

            // 3. On revient sur l'interface principale pour afficher
            if (resultats == null)
            {
                MessageErreur.Text = "Aucune donnée trouvée pour cette période.";
                ResetAffichage();
            }
            else
            {
                ValMoyenne.Text = $"{resultats.Moy:F1} °C";
                ValMin.Text = $"{resultats.Min} °C";
                ValMax.Text = $"{resultats.Max} °C";
                ValEcartType.Text = $"{resultats.Ecart:F2}";
                ValMediane.Text = $"{resultats.Med} °C"; // Et voilà la médiane !
            }
        }
        catch (Exception ex)
        {
            MessageErreur.Text = "Erreur de calcul.";
        }
        finally
        {
            // On réactive le bouton quoi qu'il arrive
            if (bouton != null) bouton.IsEnabled = true;
        }
    }

    private void ResetAffichage()
    {
        ValMoyenne.Text = "-";
        ValMin.Text = "-";
        ValMax.Text = "-";
        ValEcartType.Text = "-";
    }

    private void Retour_Click(object sender, RoutedEventArgs e)
    {
        if (Frame.CanGoBack) Frame.GoBack();
    }
}
