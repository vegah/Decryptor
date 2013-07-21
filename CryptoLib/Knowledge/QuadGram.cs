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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib.Knowledge
{
    public class QuadGram
    {
        String name;
        Int32 count;
        Int32 totalWords;
        private static QuadGram nonexisting = new QuadGram(null, Int32.MaxValue);
        public QuadGram(String name, Int32 initialSize)
        {
            this.name = name;
            count = 0;
            totalWords = initialSize;
        }

        public void Increase()
        {
            count++;
        }

        public void IncreaseTotal(Int32 number)
        {
            totalWords += number;
        }

        public double Score
        {
            get
            {
                if (count == 0)
                    return Math.Floor(Math.Log10(0.01/(double)totalWords));
                return Math.Log10((double)count / (double)totalWords);
            }
        }

        public static QuadGram Nonexisting
        {
            get
            {
                return nonexisting;
            }
        }

    }
}
