using System;

namespace Hangman1
{

    internal class Program 
    {

        public static void Main(string[] args)
        {
            // Create instance of RandomWord class and add a word to RandomWordList
            RandomWord randomWord = new RandomWord();
            randomWord.PopulateList("apple");

            string[] hangmanArray = {@$"
    ___________            
    |            
    |            
    |           
    |           
    |              
=========================", @$"
    ___________            
    |         |    
    |            
    |           
    |           
    |              
=========================", @$"
    ___________            
    |         |    
    |         o    
    |           
    |           
    |              
=========================", @$"
    ___________            
    |         |    
    |         o    
    |         |   
    |           
    |              
=========================", @$"
    ___________            
    |         |    
    |         o    
    |        /|   
    |           
    |              
=========================", @$"
    ___________            
    |         |    
    |         o    
    |        /|\   
    |          
    |              
=========================", @$"
    ___________            
    |         |    
    |         o    
    |        /|\   
    |        /     
    |              
=========================", @$"
    ___________            
    |         |    
    |         o    
    |        /|\   
    |        / \   
    |              
========================="};
            string message = hangmanArray[0];

            // Set secretWord to a word from RandomWordList
            //--string secretWord=randomWord.PickWord();
            string secretWord=RandomWord.GetRandomWordFromStaticList();
            
            // Set secretWord to uppercase and trim whitespace
            secretWord=secretWord.ToUpper().Trim();   

            

            //create a char array from secret word
            char[] secretWordCharArray=secretWord.ToCharArray();
           //list for found letters
           // - - - - - - -
           List<char> foundLetters=new List<char>(new string('-',secretWord.Length));
           
            List<char> incorrectLetters=new List<char>();
           // console out foundletters array
           Console.WriteLine(message);
           Console.WriteLine(string.Join(" ",foundLetters));


           int incorrectCounter = 0;

           while (true) {
                Console.WriteLine("Enter a letter");
                char userInput=char.ToUpper(Console.ReadKey().KeyChar); // stops input when user has pressed a key
                
                Console.WriteLine("\nuser input is "+userInput);
                int counter=0;

                /* if (!secretWordCharArray.Contains(userInput) && !incorrectLetters.Contains(userInput) && !foundLetters.Contains(userInput))
                {
                        incorrectLetters.Add(userInput);
                } */
                if (!secretWordCharArray.Contains(userInput))
                {
                    
                    if (!incorrectLetters.Contains(userInput) && !foundLetters.Contains(userInput))
                    {
                        incorrectLetters.Add(userInput);
                        incorrectCounter++;
                        message = hangmanArray[incorrectCounter];
                    }
                    else if (incorrectLetters.Contains(userInput))
                    {
                        message += $"\nTHE LETTER {userInput} HAS ALREADY BEEN GUESSED!!!";
                    }
                }
            
                // to compare my char with secret word char
                // i create a foreach loop
                foreach(char c in secretWordCharArray)
                {
                    if (c==userInput)
                    {
                        foundLetters[counter]=c;
                    }
                    
                    
                    counter++;
                }

                // console out incorrect guesses
                Console.WriteLine($"Incorrect letters: {string.Join(" ",incorrectLetters)}");
                Console.WriteLine(message);
                // console out foundletters array
                Console.WriteLine(string.Join(" ",foundLetters));
                
                if (incorrectCounter == 7) 
                {
                    Console.WriteLine($"Game Over! The word was {secretWord}");
                    break;
                } 
                else if (!foundLetters.Contains('-'))
                {
                    Console.WriteLine("You won!");
                    break;
                }

            }
        }
    }
}


           
           
