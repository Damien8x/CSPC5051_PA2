﻿//Damien Sudol
//EncryptWord.cs
//04/11/2018
//v1.0

//Class Description: EncryptWord class provides the necessary functionality, intuitevely, for class designers to encrypt a string with a 
//length greater than 3 characters, using an undisclosed Caesar cipher shift. A shift value will be a randomly generated integer between 1 
//and 9. Each individual character will be "shifted" according to the shift value, for example, with a shift value of 4 we can expect
//'a' => 'e', 'Y' => 'C'. The string, "Hello", with a shift value of 3 would be encrypted as, "Khoor". The shift value and encryption occurs
//during object construction. The string parameter passed to the constructor will be the string used for encryption. Any white-space or
//special characters will be ignored. Additonal functionality include: ability to query the object by passing an integer to the checkShift(),
//function, which returns a boolean, true, if integer matches shift, false, if integer does not match shift. All statistics are tracked by the
//class and may be viewed through public functions getGuessCount() (returns number of guess(s)*), getAvgGuess() (average guess(s)* value),
//getHighGuessCount() (guess(s)* greater than shift value), getLowGuessCount (guess(s)* less than shift value). Object can also be "reset"
//by calling the objectReset() function and passing a string parameter for encryption. Calling the objectReset() function mimics
//constructin of a new EncryptWord object, all variables are set to values in the same fashion as initialization (state transitions are laid
//out below).
//
//Object is ON if object has been initialized and is in a valid state. An object which is ON has access to the full functionality of the class.
//Object is OFF if object has not been initialized or is in an invalid state, public functions will not be accessible. 
//
// **********************  State transitions for public, non constant functions  ***************************
//
//  EncryptWord(string plainText)       Object OFF => Object ON
// * guessCount =>        0  
// * avgGuess =>          0  
// * totalGuess =>        0  
// * highGuessCount =>    0  
// * lowGuessCount =>     0  
// * plainText =>        plainText (string parameter)
// * shiftValue =>            integer between 1 and 9
// * encryptedText =>        Caesar cipher shift of plainText, with shift value of variable, shiftValue
// **************************************************************************************************************************
// checkShift(int guessValue)       Object ON => Object ON
// * guessCount =>       guessCount++
// * avgGuess =>         totalGuess/guessCount or 0
// * totalGuess =>       totalGuess += guessValue
// * highGuessCount =>   highGuessCount++ (if guessValue > shiftValue)
// * lowGuessCount =>    lowGuessCount++  (if guessValue < shiftValue)
// **************************************************************************************************************************
// objectReset(string newPlainText)  Object ON => Object ON
// * guessCount =>       0  
// * avgGuess =>         0  
// * totalGuess =>       0  
// * highGuessCount =>   0  
// * lowGuessCount =>    0  
// * plainText =>        newPlainText
// * shiftValue =>       integer between 1 and 9
// * encryptedText =>    Caesar cipher shift of plainText, with shift value of variable, shiftValue
//
// Vairables guessCount, avgGuess, totalGuess, highGuessCount and lowguessCount with value 0 prior to call will not be impacted.
// Random generation of shift value allows for the possibility that variable, shiftValue, will retain its value.
// IF parameter newPlainText is equal to plainText the variable will retain its value.
// IF both variable, shiftValue, and variable, plainText, retain their values, variable, encryptedText, will retain its value.
// **************************************************************************************************************************
//
//
// Anticipated Use: Class is intended to aid application programmers in designing a guessing game, targeted at elementary students,
// to support "decoding".  Class is not designed for robust exception handling. It is the responsibility of the application programmer
// to ensure all dependencies and function arguments are in line with documentation. Program is highly modularized and does not support
// formatting of statistics, rather, class provides a returned value for an individual statistic through its respective function call. 
//  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1
{
    class EncryptWord
    {
        //*term "guess(s)" will be used int the context of user input passed to checkShift() function intended to "guess" the
        //Caesar cipher shift 

        //Description: Integer value of Caesar cipher shift used for encryption
        private int shiftValue;
        //Description: Integer value with 1:1 relationship to number of *guess(s) 
        private int guessCount;
        //Description: Integer value of totalGuess/guessCount => avg value of *guess(s)
        private double avgGuess;
        //Description: Sum of all *guess(s) values
        private double totalGuess;
        //Description: Integer value incrememnts by 1 for each guess above "shift" value
        private int highGuessCount;
        //Descritpion: Integer value increments by 1 for each guess below "shift" value
        private int lowGuessCount;
        //Description: Encrypted string produced by applying Caesar cipher shift to plainText
        //by means of logic found in function, setEncryption()
        private string encryptedText;
        //Description: String value equal to EncryptWord constructor argument, string plainText  
        private string plainText;

        //**********GLOBAL CONSTANTS***********

        //Description: Global Integer, value represents minimum length of encrypted words
        private int MIN_CHARACTER_LIMIT = 4;
        //Description: Lowercase, lower boundry, representing 'a' in ASCII
        private int LOWERCASE_LOWER_BOUNDRY = 64;
        //Description: Lowercase, upper bounddry, representing 'z' in ASCII
        private int LOWERCASE_UPPER_BOUNDRY = 90;
        //Description: Uppercase, lower boundry, representing 'A' in ASCII
        private int UPPERCASE_LOWER_BOUNDRY = 96;
        //Description: Uppercase, upper boundry, representing 'Z' in ASCII
        private int UPPERCASE_UPPER_BOUNDRY = 122;
        
        
        //Description: EncryptWord constructor, requires string argument intended to be encrypted. Note, there are no
        //default or overloaded constructors for type EncryptWord. Call to constructor initializes object of type EncryptWord;
        //all class variables are set to default values, cipher shift value is established through call to function setShift().
        //String argument, plainText, is encrypted and value is stored in string, encryptedText, by means of function setEncryption().
        //Condition statement within constructor requires string parameter, plainText, to be a minimum of 4 characters, aborting
        //object initialization if length requirements are not met. Note, successful encryption will not print the encryptedText string.
        //To reference the variable, encryptedText, use the function getEncrypted()
        //
        //Precondition: Object is OFF. Call must have string argument intended for encryption with a minimum character length of 4.
        //
        //Postcondition: Object is ON. All public functions are available. All variables are set to default values, variable, shiftValue,
        //is set to random number between 1 and 9, passed string argument, plainText, is encrypted and stored in string variable,
        //encryptedText. 
        public EncryptWord(string plainText)
        {
            //***set default values for variables***
            this.guessCount = 0;
            this.avgGuess = 0;
            this.totalGuess = 0;
            this.highGuessCount = 0;
            this.lowGuessCount = 0;
            this.totalGuess = 0;
            this.shiftValue = 0;
            this.encryptedText = "";
            this.plainText = plainText;

            //set random shift value
            setShift();

            //encrypt plainText
            setEncryption();

        }

        //Description: Function is a means for user to *guess(s) Caesar cipher shift value used in for encryption. Integer
        //argument, guessValue, is intended to be a user's *guess(s). Every function call will result in subsequent function calls
        //used for tracking *guess(s) and statisitcs; setGuessCount(), setTotalGuess(), setAvgGuess(), and possibly setLowGuessCount() or
        //setHighGuessCount() if conditions are met. If guessValue < shiftValue, setLowGuessCount() is called. If guessValue > shiftValue, 
        //setHighGuessCount() is called. Function will return false if guessValue != shiftValue, or true if guessValue == shiftValue.
        //
        //Precondition: Object is ON.
        //
        //PostCondition: Object is ON. Values for variables guessCount, totalGuess, avgGuess will be modified. lowGuessCount and 
        //highGuessCount MAY be modified. Only if outlined conditions are met will highGuessCount or lowGuessCount be modified. 
        public bool checkShift(int guessValue)
        {
            //Increment guess count
            setGuessCount();
            //Add to sum of guess values
            setTotalGuess(guessValue);
            //Calculate average guess value
            setAvgGuess();
            //Check conditions for respective function calls and return value
            if (guessValue > this.shiftValue)
            {
                setHighGuessCount();
                return false;
            }
            else if (guessValue < this.shiftValue)
            {
                setLowGuessCount();
                return false;
            }
            else
                return true;

        
        }

        //Description: Function will mimic the class constructor IF passed string is > 3 characters. Variables guessCount, 
        //avgGuess, totalGuess, highGuessCount and lowGuessCount will be set to their default values. Variable plainText will be assigned
        //the value of the string argument. A new Caesar cipher shift value will be generated by the setShift() method and assigned to 
        //variable, shiftValue. String, plainText, will be re-encrypted with the respective new shift value and assigned to string variable, 
        //encryptedText. Any prior statistical data will be lost. There is a possibility that prior shift values will be randomly generated 
        //again, resulting in variables shift retaining the same value. If the function parameter is equal to the variable plainText, it 
        //will retain its value. If both the variable, shiftValue and variable, plainText, retain their values, variable, encrypt, will
        //also retain its respective value. If the string parameter is < 4 characters no state change will occur.
        //
        //Precondition: Object ON.
        //
        //Postcondition: Object ON. IF string parameter is < 4 characters no state change will occur. If parameter is > 4 we can expect
        //all variables MAY change, depending on current state and string parameter value.
        //State transitons will be as listed:
        //  guessCount =>       0  *
        //  avgGuess =>         0  *
        //  totalGuess =>       0  *
        //  highGuessCount =>   0  *
        //  lowGuessCount =>    0  *
        //  plainText =>        newPlainText
        //  shiftValue =>       integer between 1 and 9
        //  encryptedText =>        Caesar cipher shift of plainText, with shift value of variable, shiftValue
        //
        // * All variables which hold value of 0 prior to call will not be impacted. 
        // Random generation of shift value allows for the possibility that variable, shiftValue, will retain its value.
        // IF parameter newPlainText is equal to plainText the variable will retain its value.
        // IF both variable, shiftValue, and variable, plainText, retain their values we expect variable, encryptedText, to retain its value.
        public void objectReset(string newPlainText)
        {
            if (newPlainText.Length >= MIN_CHARACTER_LIMIT)
            {
                //set plainText to its new value
                this.plainText = newPlainText;
                this.guessCount = 0;
                this.avgGuess = 0;
                this.totalGuess = 0;
                this.highGuessCount = 0;
                this.lowGuessCount = 0;

                //generate new shift value and assign to, shiftValue, variable. Value MAY be equal to prior value
                setShift();

                //Re-encrypt variable, encryptedText. IF shift value is unchanged, encryptedText, will be unchanged
                setEncryption();
            }
        }

        //Description: Returns a 1:1 integer value for number of user *guess(s) 
        //
        //Precondition: Object ON
        //
        //Postcondition: Object ON. No state changes.
        public int getGuessCount()
        {
            return this.guessCount;
        }

       //Description: Returns a double determined by totalGuess/guessCount - average value  of *guess(s).
       //
       //Precondition: Object is ON.
       //
       //PostCondition: Object is ON. No state changes.
        public double getAvgGuess()
        {
            return this.avgGuess;
        }

        //Description: Returns integer value for all user guess(s)* < shiftValue
        //
        //Precondiditon: Object is ON.
        //
        //Postcondition: Object is ON. No state changes.
        public int getLowGuessCount()
        {
            return this.lowGuessCount;
        }

        //Description: Returns integer value for all user guess(s)* > shiftValue
        //
        //Precondition: Object is ON.
        //
        //Postcondition: Object is ON. No state changes.
        public int getHighGuessCount()
        {
            return this.highGuessCount;
        }
        
        //Description: Returns sum of all *guess(s)
        //
        //Precondition: Object ON
        //
        //Postcondition: Object ON. No state changes.

        private double getTotalGuess()
        {
            return this.totalGuess;
        }

        //Description: Returns encryptedText string.
        //
        //Precondition: Object is ON.
        //
        //Postcondition: Object is ON. No state changes.
        public string getEncrypted()
        {
            return this.encryptedText;
        }

        //Description: Returns plain text string, originally passed to be encrypted.
        //
        //Precondition: Object is ON.
        //
        //Postcondition: Object is ON. No state changes.
        public string getPlainText()
        {
            return this.plainText;
        }

        //Description: Randomly generates an integer between 1 and 9, assigning value to variable, shiftValue.
        //
        //Precondition: Object MAY be ON or OFF. Legal calls require one of two scenarios:
        //1)Object is being initialized (Object OFF), Fucntion is called from within constructor.
        //2)function is called from within objectReset() (Object ON).
        //
        //Postcondition: Object MAY be ON or OFF dependent on the state when function is called:
        //1)Object is being initialized (Object OFF), Fucntion is called from within constructor
        //2)function is called from within objectReset() (Object ON).
        //
        //If scenario 1, variable, shiftValue's, state WILL change. If scenario 2, shiftValue's, state  MAY change. For scenario 2,
        //if the randomly generated number is equal to, shiftValue's, current number, no state changes will occur within the function, 
        //else variable, shiftValue, will be assigned a new value within the function.
        private void setShift()
        {
            //*** generate random number between 1 and 9 and assign value to, shiftValue ***
            Random rnd = new Random();
            int randomNum = rnd.Next(1, 10);
            this.shiftValue = randomNum;
        }

        //Description: Function will apply a Caesar cipher shift to the variable plainText, using the generated shift value assigned to 
        //variable, shiftValue, and assign the newly encrypted string to the variable, encryptedText. IF characters are shifted beyond the A-Z alphabet
        //the shifted chacter will wrap around, starting from a. For example, with a shift of 2, z => b, Y => A. All special
        //characters and white space will not be encrypted. 
        //
        //Precondition: Object MAY be ON or OFF. Legal calls require one of two scenarios:
        //1)Object is being initialized (Object OFF), Fucntion is called from within constructor.
        //2)function is called from within objectReset() (Object ON).
        //
        //Postcondition: Object MAY be ON or OFF dependent on the state when the function is called:
        //1)Object is being initialized (Object OFF), Fucntion is called from within constructor.
        //2)function is called from within objectReset() (Object ON).
        //If scenario 1, variable, encryptedText's, state WILL change. If scenario 2, encryptedText's, state  MAY change. For scenario 2,
        //if the shift value is identical to the prior value no state changes will occur within the function, 
        //else variable, encryptedText, will be assigned a new string value within the function.
        private void setEncryption()
        {
            this.encryptedText = "";
            char currentChar;

            for(int i = 0; i < this.plainText.Length; i++ )
            {
                currentChar = plainText[i];
                int currentCharASCIIValue = (int)currentChar;

                if (currentCharASCIIValue > LOWERCASE_LOWER_BOUNDRY && currentCharASCIIValue <= LOWERCASE_UPPER_BOUNDRY)
                {
                    if (currentCharASCIIValue + this.shiftValue > LOWERCASE_UPPER_BOUNDRY)
                    {
                        currentCharASCIIValue = (((currentCharASCIIValue + this.shiftValue) - LOWERCASE_UPPER_BOUNDRY) + LOWERCASE_LOWER_BOUNDRY);
                        encryptedText += (char)currentCharASCIIValue;
                    }
                    else
                    {
                        currentCharASCIIValue = currentCharASCIIValue + this.shiftValue;
                        encryptedText += (char)currentCharASCIIValue;
                    }
                }
                else if (currentCharASCIIValue > UPPERCASE_LOWER_BOUNDRY && currentCharASCIIValue <= UPPERCASE_UPPER_BOUNDRY)
                {
                    if (currentCharASCIIValue + this.shiftValue > UPPERCASE_UPPER_BOUNDRY)
                    {
                        currentCharASCIIValue = (((currentCharASCIIValue + this.shiftValue) - UPPERCASE_UPPER_BOUNDRY) + UPPERCASE_LOWER_BOUNDRY);
                        encryptedText += (char)currentCharASCIIValue;
                    }
                    else
                    {
                        currentCharASCIIValue = currentCharASCIIValue + this.shiftValue;
                        encryptedText += (char)currentCharASCIIValue;
                    }
                }
                else
                    encryptedText += (char)currentCharASCIIValue;

                
            }
            //*** use ascii values to map and shift characters of variable, plainText. Assign encrypted string to variable, encryptedText ***
        }

        //Description: Increments by one for each *guess. Function is called from method checkShift().
        //
        //Precondition: Object is ON
        //
        //Postcondition: Object is ON. Variable guessCount increases by 1.
        private void setGuessCount()
        {
            this.guessCount++;
        }

        //Description: Sum of all *guess(s) made. Accepts parameter with the *guess value passed to checkShift(). Function
        //is only called within checkShift().
        //
        //Precondition: Object is ON.
        //
        //Postcondition: Object is ON. Variable totalGuess is increased by the value of the guessValue (*guess input 
        //passed to checkShift()). If guessValue == 0 no change to state will occur.
        private void setTotalGuess(int guessValue)
        {
            this.totalGuess += guessValue;
        }

        //Description: Assigns double value of totalGuess/guessCount to variable avgGuess. Ensures totalGuess != 0 to prevent
        //divide by zero error.
        //
        //Precondition: Object is ON.
        //
        //Postcondition: Object is ON. Variable avgGuess will be assigned quotient of totalGuess/guessCount. IF totalGuess == 0
        //avgGuess will be assigned the value 0.
        void setAvgGuess()
        {
            if (totalGuess != 0)
                this.avgGuess = this.totalGuess / this.guessCount;
            else
                this.avgGuess = 0;
        }

        //Function can only be called within function checkShift() when *guess value is > shiftValue. Will increment variable highGuessCount 
        //by 1 each call.
        //
        //Precondition: Object is ON.
        //
        //Postcondition: Object is ON. Variable highGuessCount increments by 1.
        void setHighGuessCount()
        {
            this.highGuessCount++;
        }

        //Function can only be called within function checkShift() when *guess value is < shiftValue. Will increment cariable lowGuessCount
        //by 1 each call.
        //
        //Precondition: Object is ON.
        //
        //Postcondition: Object is ON. Variable lowGuessCount increments by 1.
        void setLowGuessCount()
        {
           this.lowGuessCount++;
        }
    }
}
