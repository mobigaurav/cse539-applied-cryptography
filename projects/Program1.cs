

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Cryptanalysis
{
    public static void Main(string[] args)
        {
          //Write your code here and do not change the class name.
          DateTime startDate = new DateTime(2020, 7, 3, 11, 0, 0);
          DateTime endDate = new DateTime(2020, 7, 4, 11, 0, 0);
          
          String plainText = args[0];
          String cipherText = args[1];
          for (DateTime dt=startDate; dt<=endDate; dt=dt.AddMinutes(1)){
            TimeSpan ts = dt.Subtract(new DateTime(1970, 1, 1));
            Random rng = new Random((int)ts.TotalMinutes);
            byte[] key = BitConverter.GetBytes(rng.NextDouble());
            try {

            String decryptedString = Decrypt(key, cipherText);
            
            if(decryptedString == plainText) {
                Console.WriteLine((int)ts.TotalMinutes);
                break;
            }
            }
            catch {
              continue;
            }
            
          }

        }
  
     private static string Decrypt(byte[]key, string encryptedString) {
        DESCryptoServiceProvider csp = new DESCryptoServiceProvider();
        byte[] cipherText = Convert.FromBase64String(encryptedString);
        MemoryStream ms = new MemoryStream(cipherText);
        CryptoStream cs = new CryptoStream(ms,csp.CreateDecryptor(key, key), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cs);
        return sr.ReadToEnd();
     }
  
}