// https://crackstation.net/
// https://www.mscs.dal.ca/~selinger/md5collision/

using System;
using System.Text;
using BCrypt.Net;
using System.IO;
using System.Security.Cryptography;
namespace consoleApp
{
    class DonneesSecurite
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234561234567890123456"); // 32 bytes AES-256
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes

        public static string Encrypter(string input)
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor();

            byte[] plainBytes = Encoding.UTF8.GetBytes(input);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypter(string input)
        {
            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] cipherBytes = Convert.FromBase64String(input);
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
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
