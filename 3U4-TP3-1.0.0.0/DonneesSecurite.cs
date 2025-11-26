// https://crackstation.net/
// https://www.mscs.dal.ca/~selinger/md5collision/

using System;
using BCrypt.Net;
namespace consoleApp
{
    class DonneesSecurite
    {
        public static string Encrypter(string input)
        {
            string result = "";
            foreach (char c in input)
            {
                char t = (char)(c * 2);
                result += t;
            }
            return result;
        }

        public static string Decrypter(string input)
        {
            string result = "";
            foreach (char c in input)
            {
                char t = (char)(c / 2);
                result += t;
            }
            return result;
        }

        // fonctions permettant le hachage des mots de passe
        public static string HacherLeMotDePasse(string input)
        {
            // Utilise la chaîne d'entrée pour calculer le hachage MD5
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
        
        public static bool VerifierLeMotDePasse(string motDePasse, string hache)
        {
            // Calcule le hachage MD5 du mot de passe fourni et le compare avec le haché stocké
            return BCrypt.Net.BCrypt.Verify(motDePasse, hache);
        }
        
    }
}
