using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;


    class DiffieHellman
    {


       public static void Main(string[] args)
        {
           //Write your code here and do not change the class name.
           byte[] iVector = hexToByteArray(args[0]);
           byte[] encryptedMessage = hexToByteArray(args[7]);
           string plainText = args[8];
           BigInteger g_e = BigInteger.Parse(args[1]);
           BigInteger g_c = BigInteger.Parse(args[2]);
           BigInteger n_e = BigInteger.Parse(args[3]);
           BigInteger n_c = BigInteger.Parse(args[4]);
           BigInteger x = BigInteger.Parse(args[5]);
           BigInteger gymodN = BigInteger.Parse(args[6]);

           BigInteger generator = BigInteger.Pow(2, (int)g_e) - g_c;
           BigInteger randomNumber = BigInteger.Pow(2, (int)n_e) - n_c;

           BigInteger secret = BigInteger.ModPow(gymodN, x, randomNumber);
           byte[] secretBytes = secret.ToByteArray();


           byte[] aesEncryptionKey = secretBytes;
           string decryptedMsg = DecryptUsingAES(encryptedMessage, aesEncryptionKey, iVector);

           byte[] encyptionBytes = EncryptUsingAES(plainText, aesEncryptionKey, iVector);

           Console.WriteLine($"{decryptedMsg},{BitConverter.ToString(encyptionBytes).Replace("-", " ")}");

        }

    static byte[] EncryptUsingAES(string plaintext, byte[] key, byte[] iVector)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = key;
            aes.IV = iVector;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plaintext);
                return encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            }
        }
    }

    static string DecryptUsingAES(byte[] encryptedMessage, byte[] key, byte[] iVector)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = key;
            aes.IV = iVector;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedMessage, 0, encryptedMessage.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }

        static byte[] hexToByteArray(string hex) {
            hex = hex.Replace(" ", ""); 
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
}
    }

