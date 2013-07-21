using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoLib.Knowledge
{
    public class QuadGramCollection
    {
        Dictionary<String, QuadGram> collection;
        public QuadGramCollection()
        {
            collection = new Dictionary<String, QuadGram>();
        }

        public void GenerateFromFile(String filename)
        {
            using (FileStream file = new FileStream(filename, FileMode.Open))
            {
                GenerateFromStream(file);
            }
        }

        public void GenerateFromStream(Stream stream)
        {
            using (TextReader reader = new StreamReader(stream))
            {
                String complete = reader.ReadToEnd();
                complete = complete.ToUpper();
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                complete = rgx.Replace(complete, "");
                for (Int32 i = 0; i < complete.Length - 3; i++)
                {
                    String quad = complete.Substring(i, 4);
                    if (!collection.ContainsKey(quad))
                        collection.Add(quad, new QuadGram(quad, complete.Length-3));
                    collection[quad].Increase();
                }
            }
        }

        public double ScoreFile(String filename)
        {
            using (FileStream file = new FileStream(filename, FileMode.Open))
            {
                return ScoreStream(file);
            }
        }

        public double ScoreStream(Stream stream)
        {
            using (TextReader reader = new StreamReader(stream))
            {
                String complete = reader.ReadToEnd();
                return ScoreString(complete);
            }
        }

        public double ScoreString(String text)
        {
            double totalScore = 0;
            String complete = text.ToUpper();
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            complete = rgx.Replace(complete, "");

            for (Int32 i = 0; i < complete.Length - 3; i++)
            {
                String quad = complete.Substring(i, 4);
                double curScore;
                if (collection.ContainsKey(quad))
                    curScore = collection[quad].Score;
                else
                    curScore = QuadGram.Nonexisting.Score;
                totalScore += curScore;
            }
            return totalScore;
        }
    
    }
}
