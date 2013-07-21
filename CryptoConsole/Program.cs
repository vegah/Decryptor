using CryptoLib.Knowledge;
using CryptoLib.SimpleSubstitution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoConsole
{
    class Program
    {
        const Int32 mutations = 2;  // How many mutations per iteration
        const Int32 maxIterationsBeforeNewRandomKey = 1000;

        static void Main(string[] args)
        {
            QuadGramCollection collection = new QuadGramCollection();
            collection.GenerateFromFile(@"C:\development\crypto\tekster\warandpeace.txt");
            // Test random string generation and mutation...
            Decrypt decrypt;
            using (TextReader reader = new StreamReader(@"C:\development\crypto\tekster\ciphertext1.txt"))
            {
                decrypt = new Decrypt(reader.ReadToEnd()); // From wikipedia
            }
            //String isThisCorrect = decrypt.TryKey("ZEBRASCDFGHIJKLMNOPQTUVWXY");
            //Console.WriteLine(isThisCorrect);
            
            String startKey = GenerateRandomKey();
            Random rand = new Random();
            Int32 counter = 0;
            double bestScore = double.MinValue;
            String bestKey = "";
            while (true)
            {
                char[] mutation = startKey.ToArray();
                for (Int32 i = 0; i < mutations; i++)
                {
                    Int32 switch1 = rand.Next(startKey.Length);
                    Int32 switch2 = rand.Next(startKey.Length);
                    char c1 = mutation[switch2];
                    char c2 = mutation[switch1];
                    mutation[switch2] = c2;
                    mutation[switch1] = c1;
                }
                String mutatedKey = new String(mutation);
                String oldValue = decrypt.TryKey(startKey);
                String newValue = decrypt.TryKey(mutatedKey);
                double score1 = collection.ScoreString(oldValue);
                double score2 = collection.ScoreString(newValue);
                if (score2 > score1)
                {
                    startKey = mutatedKey;
                    //Console.WriteLine("{0} - {1} : {2} - {3}", startKey, mutatedKey, oldValue, newValue);
                    counter = 0;
                    if (score2 > bestScore)
                    {
                        bestScore = score2;
                        bestKey = mutatedKey;
                        Console.WriteLine(newValue);
                    }
                }
                counter++;
                if (counter > maxIterationsBeforeNewRandomKey)
                {
                    startKey = GenerateRandomKey();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Current score {0}", bestScore);
                    Console.WriteLine("Current key {0}", bestKey);
                    String decrypted = decrypt.TryKey(bestKey);
                    Console.WriteLine("Current first 200 chars {0}", decrypted.Substring(0,decrypted.Length>200 ? 200 : decrypted.Length));
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.ReadLine();
        }

        private static String GenerateRandomKey()
        {
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
            List<char> alph = new List<char>(alphabet);
            Random rand = new Random();
            for (Int32 i = 0; i < alphabet.Length; i++)
            {
                Int32 nr = rand.Next(alph.Count);
                alphabet[i] = alph[nr];
                alph.RemoveAt(nr);
            }
            return new String(alphabet);
        }
    }
}
