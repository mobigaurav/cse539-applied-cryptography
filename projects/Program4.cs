using System;
using System.Numerics;


    class RSA
    {


       public static void Main(string[] args)
        {
           //Write your code here and do not change the class name.
           BigInteger p_e = BigInteger.Parse(args[0]);
           BigInteger p_c = BigInteger.Parse(args[1]);
           BigInteger q_e = BigInteger.Parse(args[2]);
           BigInteger q_c = BigInteger.Parse(args[3]);
           BigInteger cipherText = BigInteger.Parse(args[4]);
           BigInteger plainText = BigInteger.Parse(args[5]);

           BigInteger p = BigInteger.Pow(2, (int)p_e) - p_c;
           BigInteger q = BigInteger.Pow(2, (int)q_e) - q_c;

           BigInteger n = p*q;
           BigInteger phi_n = (p-1)*(q-1);

           BigInteger e = 65537;

           BigInteger d = extendedEuclidean(e, phi_n);

           BigInteger decryptedValue = modularExponent(cipherText, d, n);
           BigInteger encryptedValue = modularExponent(plainText, e, n);

           Console.WriteLine($"{decryptedValue},{encryptedValue}");

        }

        public static BigInteger extendedEuclidean(BigInteger e, BigInteger phi_n) {
            BigInteger number1 = e;
            BigInteger number2 = phi_n;
            BigInteger x1 = 1;
            BigInteger x2 = 0;
            BigInteger y1 = 0;
            BigInteger y2 = 1;

            while (number2 != 0) {
                BigInteger quotient = number1 / number2;
                (number1, number2) = (number2, number1 - quotient * number2);
                (x1, x2) = (x2, x1 - quotient * x2);
                (y1, y2) = (y2, y1 - quotient * y2);
            }

            if(x1 < 0) {
                x1 += x1 + phi_n;
            }

            return x1;
        }

        public static BigInteger modularExponent(BigInteger baseNumber, BigInteger exponent, BigInteger modulus) {
            BigInteger result = 1;
            baseNumber = baseNumber % modulus;
            while(exponent > 0) {
                if(exponent % 2 == 1) {
                    result = (result * baseNumber) % modulus;
                }
                exponent = exponent / 2;
                baseNumber = (baseNumber * baseNumber) % modulus;
            }
            return result;
        }
    }

