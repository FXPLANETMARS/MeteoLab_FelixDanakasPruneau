using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MeteoLab_FelixDanakasPruneau.Presentation;

public sealed partial class AccueilPage : Page
{
    public AccueilPage()
    {
        this.InitializeComponent();
    }

    private void GoToSaisie_Click(object sender, RoutedEventArgs e)
    {
    
        Frame.Navigate(typeof(MainPage));
    }

    private void GoToStats_Click(object sender, RoutedEventArgs e)
    {
        
        Frame.Navigate(typeof(StatsPage));
    }
}
