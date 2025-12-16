namespace MeteoLab_FelixDanakasPruneau.Presentation;
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using global::MeteoLab_FelixDanakasPruneau.Models; 

public sealed partial class MainPage : Page
{
    private DonneeMeteo _meteoActuelle;

    public MainPage()
    {
        this.InitializeComponent();

        
        _meteoActuelle = new DonneeMeteo();

        
        FormulaireMeteo.DataContext = _meteoActuelle;
    }

    private void RetourMenu_Click(object sender, RoutedEventArgs e)
    {
        
        this.Frame.Navigate(typeof(AccueilPage));
    }


    private async void ImporterCSV_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            
            var uri = new Uri("ms-appx:///Assets/Donnéescsv.csv");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);

            var lignes = await FileIO.ReadLinesAsync(file);

            int compteur = 0;

          
            foreach (string ligne in lignes)
            {
                
                if (ligne.StartsWith("Date") || string.IsNullOrWhiteSpace(ligne)) continue;

                string[] colonnes = ligne.Split(',');

                if (colonnes.Length >= 3)
                {
                    DonneeMeteo m = new DonneeMeteo();

                    
                    if (DateTimeOffset.TryParse(colonnes[0], out DateTimeOffset date))
                        m.Date = date;

                    
                    m.Temperature = colonnes[1];

                    
                    if (double.TryParse(colonnes[2], out double hum))
                    {
                        m.Humidite = hum;
                    }

                    
                    m.Precipitations = 0;

                    GestionMeteo.Ajouter(m);
                    compteur++;
                }
            }

            MessageErreur.Text = $"Succès ! {compteur} données importées.";
            MessageErreur.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Blue);
        }
        catch (Exception ex)
        {
            MessageErreur.Text = $"Erreur Import : {ex.Message}";
        }
    }

    private void Enregistrer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            
            GestionMeteo.Ajouter(_meteoActuelle);
            

            MessageErreur.Text = "Données sauvegardées !";
            MessageErreur.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Green);

            
            this.Frame.Navigate(typeof(SecondPage), _meteoActuelle);

            
            _meteoActuelle = new DonneeMeteo();
            FormulaireMeteo.DataContext = _meteoActuelle;
            
        }
        catch (Exception ex)
        {
            MessageErreur.Text = $"Erreur : {ex.Message}";
            MessageErreur.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Red);
        }
    }
}
