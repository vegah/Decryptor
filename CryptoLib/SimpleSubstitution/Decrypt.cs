using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib.SimpleSubstitution
{
    public class Decrypt
    {
        String encryptedText;
        String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public Decrypt(String encryptedText)
        {
            this.encryptedText = encryptedText.ToUpper();
        }

        public String TryKey(String key)
        {
            if (key.Length != alphabet.Length)
                throw new System.Exception("Key must have same length as alphabet");
            String backup = encryptedText;
            for (Int32 i = 0; i < alphabet.Length; i++)
                backup = backup.Replace(key[i], char.ToLower(alphabet[i]));
            return backup.ToUpper();
        }
    }
}
