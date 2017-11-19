using System;
using System.Linq;
using System.Text;

namespace ConvertisseurNumerationRomaine
{
    public class ConvertisseurNombreRomain
    {
        /// <summary>
        /// Convertit un nombre entier en nombre romain
        /// </summary>
        /// <param name="nombre">Nombre à convertir</param>
        /// <returns>Equivalent en numération romaine</returns>
        public static string ConvertirEntierVersRomain(int nombre)
        {
            if (nombre <= 0 || nombre >= 10000)
            {
                throw new ArgumentException("Le nombre à convertir doit compris entre 1 et 9999", nameof(nombre));
            }

            // Décompose le nombre sous forme "canonique". N = x * 10^3 + y * 10^2 + z * 10 + i
            // On se limite aux milliers car le chiffre romain le plus important représente 1000
            int milliers = nombre / 1000;
            int reste = nombre % 1000;

            int centaines = reste / 100;
            reste %= 100;

            int dizaines = reste / 10;
            reste %= 10;

            int unites = reste;

            // Calcule le nombre romain
            StringBuilder nombreRomain = new StringBuilder();

            // Milliers : pas de règle (dernier chiffre du système de numération), autant de M que le multiplicateur
            if (milliers > 0)
            {
                nombreRomain.Append(new string('M', milliers));
            }

            // Centaines (100-999) : C (100) , D (500), et M (chiffre de la plage suivante)
            nombreRomain.Append(ConvertirPuissanceDixSoustractif(centaines, 'C', 'D', 'M'));

            // Dizaines (10-99) : X, L, C (chiffre de la plage suivante)
            nombreRomain.Append(ConvertirPuissanceDixSoustractif(dizaines, 'X', 'L', 'C'));

            // Unités (1-9) : I, V et X (chiffre de la plage suivante)
            nombreRomain.Append(ConvertirPuissanceDixSoustractif(unites, 'I', 'V', 'X'));

            return nombreRomain.ToString();
        }

        /// <summary>
        /// Convertit un multiplicateur de puissance de 10 (1, 10, 100 ...) en équivalent romain
        /// Chaque plage (1-10, 10-100, 100-1000 ...) s'écrit avec un caractère représentant les unités (1, 10, 10, ...) et un caractère représenant la moitié de la plage (5, 50, 500 ...)
        /// Pour les coefficients 4 et 9, la notation soustractive est utilisée (4 = moitié moins une unité ; 9 = puissance suivante moins une unité)
        /// </summary>
        /// <param name="multiplicateur">Coefficient multiplicateur à convertir en nombre romain</param>
        /// <param name="caractereUnite">Caractère pour décrire les unités</param>
        /// <param name="caractereMoitie">Caractères pour décrire les moitiés</param>
        /// <param name="caracterePuissanceSuivante">Caractère pour décrire la puissance de 10 ultérieure</param>
        /// <returns>Equivalent romain du nombre</returns>
        private static string ConvertirPuissanceDixSoustractif(int multiplicateur, char caractereUnite, char caractereMoitie, char caracterePuissanceSuivante)
        {
            if (multiplicateur < 0 || multiplicateur > 9)
            {
                throw new ArgumentException("Le multiplicateur de puissance 10 doit être compris entre 0 et 9 (vérifier la décomposition canonique)", nameof(multiplicateur));
            }

            StringBuilder equivalentRomain = new StringBuilder();

            // Notation soustractive précédant la puissance ultérieure (9, 90, 900 ...)
            // Ex : IX, XC, CM
            if (multiplicateur == 9)
            {
                equivalentRomain.Append($"{caractereUnite}{caracterePuissanceSuivante}");
            }

            // Plage entre 5 et 8 : caractère de moitié puis les caractères d'unité restants
            else if (multiplicateur >= 5)
            {
                equivalentRomain.Append(caractereMoitie);
                equivalentRomain.Append(new string(caractereUnite, multiplicateur - 5));
            }

            // Notation soustractive précédant la moitié (4, 40, 400 ...)
            // Ex : IV, CL, CD
            else if (multiplicateur == 4)
            {
                equivalentRomain.Append($"{caractereUnite}{caractereMoitie}");
            }

            // Plage entre 1 et 3 (n'écrit rien pour 0)
            // Ex : I, II, X, XX ...
            else if (multiplicateur > 0)
            {
                equivalentRomain.Append(new string(caractereUnite, multiplicateur));
            }

            return equivalentRomain.ToString();
        }
    }
}
