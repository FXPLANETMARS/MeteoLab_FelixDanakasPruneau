using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoLab_FelixDanakasPruneau.Presentation;
public static class GestionMeteo
{
   
    public static List<DonneeMeteo> Historique { get; set; } = new List<DonneeMeteo>();

   
    public static void Ajouter(DonneeMeteo donnee)
    {
        Historique.Add(donnee);
    }
}
