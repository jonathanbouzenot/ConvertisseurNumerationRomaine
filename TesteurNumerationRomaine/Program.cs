using ConvertisseurNumerationRomaine;
using System;

namespace TesteurNumerationRomaine
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Convertisseur de nombre romains.");
            Console.WriteLine("Veuillez saisir Q à tout moment pour quitter le programme.");

            string ligne = string.Empty;
            string nombreRomain = string.Empty;
            int nombreAConvertir = 0;

            do
            {
                Console.WriteLine("Nombre à convertir :");
                ligne = Console.ReadLine();

                if (int.TryParse(ligne, out nombreAConvertir))
                {
                    if (nombreAConvertir < 1 || nombreAConvertir >= 10000)
                    {
                        Console.WriteLine("Le nombre doit être compris entre 1 et 9999.");
                    }
                    else
                    {
                        nombreRomain = ConvertisseurNombreRomain.ConvertirEntierVersRomain(nombreAConvertir);

                        Console.WriteLine($"Nombre romain équivalent : {nombreRomain}");
                    }
                }
                else
                {
                    Console.WriteLine("Format de nombre invalide.");
                }
            }
            while (ligne.ToUpper() != "Q");
        }
    }
}
