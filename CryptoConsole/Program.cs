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
            Console.WriteLine("usage CryptoConsole.exe <txtfiletolearnfrom> <texttodecrypt>");
            if (args.Length != 2)
            {
                Console.WriteLine("Less or more than 2 arguments supplied");
            }
            else
            {
                Console.WriteLine("Running......");
                QuadGramCollection collection = new QuadGramCollection();
                collection.GenerateFromFile(args[0]);
                // Test random string generation and mutation...
                String fileContent;
                using (TextReader reader = new StreamReader(args[1]))
                {
                    fileContent = reader.ReadToEnd(); // From wikipedia
                }
                Cracker cracker = new Cracker(fileContent, collection, 2, 1000);

                DateTime oldTime = DateTime.Now;
                DateTime started = oldTime;
                while (true)
                {
                    cracker.Try();
                    DateTime newTime = DateTime.Now;
                    if ((newTime - oldTime).TotalMilliseconds > 1000)
                    {
                        Console.SetCursorPosition(0, 4);
                        Console.WriteLine("Running for {0} ms     ", (newTime - started).TotalMilliseconds);
                        Console.WriteLine("Best score is {0}", cracker.Bestscore);
                        Console.WriteLine("Best key is {0}", cracker.BestKey);
                        Console.WriteLine("First 200 characters of solution is:\n {0}", cracker.BestTry.Substring(0, cracker.BestTry.Length > 200 ? 200 : cracker.BestTry.Length));
                        oldTime = newTime;
                    }
                }
            }
        }

    }
}
