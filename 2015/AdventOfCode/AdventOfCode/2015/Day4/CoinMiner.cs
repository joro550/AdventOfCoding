using System;
using System.Text;
using System.Security.Cryptography;

namespace AdventOfCode._2015.Day4
{
    public class CoinMiner
    {
        private readonly int _numberOfZeros;
        private readonly string _stringToCompare;

        public CoinMiner(int numberOfZeros = 5)
        {
            _numberOfZeros = numberOfZeros;
            for (var i = 0; i < numberOfZeros; i++) 
                _stringToCompare += "0";
        }

        public int GetValidCoinNumber(string key)
        {
            var hash = string.Empty;
            using var md5Hasher = MD5.Create();
            
            int i = 1;
            for (; !IsValidHash(hash); i++) 
                hash = FromByteArray(md5Hasher.ComputeHash(ToByteArray(key, i)));

            return i - 1;
        }

        private static byte[] ToByteArray(string key, int value)
            => Encoding.ASCII.GetBytes($"{key}{value}"); 

        private static string FromByteArray(byte[] data)
        {
            var sb = new StringBuilder();
            foreach (var t in data) 
                sb.Append(t.ToString("X2"));
            return sb.ToString();
        }

        private bool IsValidHash(string hash) 
            => hash.Length >= _numberOfZeros && _stringToCompare.Equals(hash[.._numberOfZeros], StringComparison.CurrentCultureIgnoreCase);
    }
}