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
