//Damien Sudol
//P1Driver.cs
//04/11/2018
//v1.0


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace p1
{
    class P1Driver
    {
        //Driver Constants
        private int LOW_GUESS_VALUE = 1;
        private int HIGH_GUESS_VALUE = 9;
        private string BREAKLINE = "***************************************************************************************************";

        //Class attribute;
        private List<EncryptWord> encryptWordList;
        private int currentObjectPosition;
        private bool reset;

        //Class constructor initializes class attributes and puts them in a safe state
        public P1Driver()
        {
            this.currentObjectPosition = -1;
            this.encryptWordList = new List<EncryptWord>();
            this.reset = false;
        }

        //Description: Method used to create and store new EncryptWord object
        //Function also increments currentObjectPosition and calls printStats function
        public void encryptWord(string plainText)
        {
            this.encryptWordList.Add(new EncryptWord(plainText));
            this.currentObjectPosition++;
            Console.WriteLine("Plain Text:\t" + plainText);
            Console.WriteLine("Encrypted:\t" + encryptWordList[currentObjectPosition]);
            Console.WriteLine(BREAKLINE);
            printStats();
        }
    
        //Welcome message describing driver and what it seeks to accomplish
             public void welcomeMessage()
        {
            Console.WriteLine(BREAKLINE + "\n\t\t\t\tWELCOME TO ENCRYPT WORD\n\n"
                + " This driver is designed to test an extensive list of scenarios,\n" +
                " ensuring that the program operates as intended. Testing will include:\n\n" +
                "\t\t1)Creating a new EncryptWord object\n" +
                "\t\t2)Simulate user input for shift key \"guesses\" (integer values 1-9)\n" +
                "\t\t  followed by all applicalble statistics per \"guess\"\n" +
                "\t\t3)Perform an \"Object Reset\" with new input string\n" +
                "\t\t4)Audit object statistics after object reset\n" +
                "\t\t5)Repeat step 2 for \"reset\" encrypted word\n" +
                "\t\t6)Test that encryption ignores special characters/white-space\n" +
                "\t\t7)Create multiple, distinct objects, testing for persistent, unique values \n\n" +
                " For any questions regarding expected behavior beyond the stated, please refer to\n" +
                " the provided documentation.\n\n" + BREAKLINE);
        }

        //prints a description of function, followed by a formatted break down of the current statistical
        //state of all objects, in order, contained in encryptWordList.
        public void printAllObjCreated()
        {
            if (currentObjectPosition != 1)
            {
                Console.WriteLine(BREAKLINE + "\nWe can expect output to be equal to that of the last\n" +
                    "printed statistic for each resepctive object, thus we can\n" +
                    "expect all objects will share \"Guess Count\" and\n" +
                    "\"Avg Guess\" values. High and Low Counts MAY share values\n" +
                    "if the randomly generated shift values are equal.\n");

                for (int i = 0; i < encryptWordList.Count; i++)
                {
                    double avgGuess = encryptWordList[i].getAvgGuess();
                    var s = string.Format("{0:0.00}", avgGuess);
                    Console.WriteLine("\nObject " + (i + 1) + "\nCurrent Stats:\nGuess Count:\t" +
                        encryptWordList[i].getGuessCount() + "\nAvg Guess:\t" + s +
                        "\nHigh Guess:\t" + encryptWordList[i].getHighGuessCount()
                        + "\nLow Guess:\t" + encryptWordList[i].getLowGuessCount());
                }
            }
            else
            {
                Console.WriteLine("Please create an EncryptWord object through the obj.encryptWord() function");
            }
        }
        //Function formats, prints, and resets EncryptWord object,
        //followed by a call to the printStats() function
        public void printObjReset(string newPlainText)
        {
            if (currentObjectPosition != -1)
            {
                Console.WriteLine(BREAKLINE);
                Console.WriteLine("OBJECT RESET");
                this.encryptWordList[currentObjectPosition].objectReset(newPlainText);
                Console.WriteLine("Unencrypted:\t" + newPlainText);
                Console.WriteLine("Encrypted:\t" + this.encryptWordList[currentObjectPosition].getEncrypted());
                Console.WriteLine("\nTest Statistics Reset\nExpect outputs to be zero\n");
                Console.WriteLine("Guess Count:\t\t" + this.encryptWordList[currentObjectPosition].getGuessCount());
                Console.WriteLine("High Guess Count:\t" + this.encryptWordList[currentObjectPosition].getHighGuessCount());
                Console.WriteLine("Low Guess Count:\t" + this.encryptWordList[currentObjectPosition].getLowGuessCount());
                Console.WriteLine("Avg Guess value:\t" + this.encryptWordList[currentObjectPosition].getAvgGuess());
                reset = true;
                printStats();
            }
            else
            {
                Console.WriteLine("Please create an EncryptWord object through the obj.encryptWord() function");
            }
        }

        //Description: prints all valuable statistics for expectd guesses(1-9). Also
        //prints flag if guess matches key. Expect that all guesses prior to key detection
        //will increment the lowGuessCount, and all guesses above flag to increment highGuessCount.
        //We can also assume that the guessCount will end increment by 1 each call and the avg Guess 
        //count will be identical for all calls to method if the EncryptWord object's stat attributes
        //are zero.
        private void printStats()
        {
            bool shiftGuess = false;
            Console.WriteLine(BREAKLINE);
            if (this.reset != true)
                Console.WriteLine("NOT RESET OBJECT " + currentObjectPosition + "\n");
            else
            {
                Console.WriteLine("RESET OBJECT " + currentObjectPosition + "\n");
                this.reset = false;
            }
            for (int i = LOW_GUESS_VALUE; i <= HIGH_GUESS_VALUE; i++)
            {

                shiftGuess = this.encryptWordList[currentObjectPosition].checkShift(i);
                if (shiftGuess == true)
                    Console.WriteLine("\t\t  \t\t!!!!!!KEY DETECTED!!!!!!\n");
                Console.WriteLine("Guess Count: " + this.encryptWordList[currentObjectPosition].getGuessCount());
                Console.WriteLine("Guess Value: " + i + "\nKey Detected: " + shiftGuess);
                double avgGuess = this.encryptWordList[currentObjectPosition].getAvgGuess();
                var s = string.Format("{0:0.00}", avgGuess);
                Console.WriteLine("Average Guess: " + s);
                Console.WriteLine("High Guess Count: " + this.encryptWordList[currentObjectPosition].getHighGuessCount());
                Console.WriteLine("Low Guess Count: " + this.encryptWordList[currentObjectPosition].getLowGuessCount() + "\n");
            }
        }

    }
}
