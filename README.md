Decryptor
=========

Very simple substitution encryption cracker using quadgram's to analyze the text.

Right now the cracking logic is all in program.cs, which of course is messy etc.  But it works as long as the text is long.

QuadGramCollection.GenerateFromFile stores QuadGrams for a given text.  If the text you are cracking is in english, you will need to have a large english text to load here.

The cipher to crack is loaded on line 23 of program.cs. Just change that to the file you want to decrypt.

