/*
     Copyright 2013 Vegard Berget

       Licensed under the Apache License, Version 2.0 (the "License");
       you may not use this file except in compliance with the License.
       You may obtain a copy of the License at

           http://www.apache.org/licenses/LICENSE-2.0

       Unless required by applicable law or agreed to in writing, software
       distributed under the License is distributed on an "AS IS" BASIS,
       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
       See the License for the specific language governing permissions and
       limitations under the License.
*/
using CryptoLib.Knowledge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib.SimpleSubstitution
{
    public class Cracker
    {
        ITextScorer scorer;
        String startKey;
        Int32 mutations;
        Int32 maxIterationsBeforeNewRandomKey;
        Decrypt decrypt;
        Int32 counter;
        double bestScore;
        String bestKey;
        String bestValue;

        public Cracker(String encryptedText, ITextScorer scorer, Int32 numberOfMutations, Int32 numberOfIterations)
        {
            decrypt = new Decrypt(encryptedText);
            this.scorer = scorer;
            startKey = GenerateRandomKey();
            maxIterationsBeforeNewRandomKey = numberOfIterations;
            mutations = numberOfMutations;
            bestScore = double.MinValue;
        }

        public void Try()
        {
            char[] mutation = startKey.ToArray();
            Random rand = new Random();
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
            double score1 = scorer.ScoreString(oldValue);
            double score2 = scorer.ScoreString(newValue);
            if (score2 > score1)
            {
                startKey = mutatedKey;
                //Console.WriteLine("{0} - {1} : {2} - {3}", startKey, mutatedKey, oldValue, newValue);
                counter = 0;
                if (score2 > bestScore)
                {
                    bestScore = score2;
                    bestKey = mutatedKey;
                    bestValue = newValue;
                }
            }
            counter++;
            if (counter > maxIterationsBeforeNewRandomKey)
            {
                startKey = GenerateRandomKey();
            }
        }

        private String GenerateRandomKey()
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

        public double Bestscore
        {
            get
            {
                return bestScore;
            }
        }

        public String BestKey
        {
            get
            {
                return bestKey;
            }
        }

        public String BestTry
        {
            get
            {
                return bestValue;
            }
        }

    }
}
