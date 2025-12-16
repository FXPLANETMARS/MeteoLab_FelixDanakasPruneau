namespace MeteoLab_FelixDanakasPruneau.Presentation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

public sealed partial class SecondPage : Page
{
    public SecondPage()
    {
        this.InitializeComponent();
    }

    
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is DonneeMeteo laMeteoRecue)
        {
            this.DataContext = laMeteoRecue;

        
            TexteDate.Text = laMeteoRecue.Date.ToString("dd MMMM yyyy");
        }
    }

    private void Retour_Click(object sender, RoutedEventArgs e)
    {
       
        if (this.Frame.CanGoBack)
        {
            this.Frame.GoBack();
        }
    }
}

