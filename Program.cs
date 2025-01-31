using System;

namespace Hangman1
{

    internal class Program 
    {

        public static void Main(string[] args)
        {
            GameThings gameThings = new GameThings();
            Start:
            string message = gameThings.Hangman;

            // Set secretWord to a word from RandomWordList
            //--string secretWord=randomWord.PickWord();
            string secretWord=GameThings.GetRandomWordFromStaticList();
            
            // Set secretWord to uppercase and trim whitespace
            secretWord=secretWord.ToUpper().Trim();   

            

            //create a char array from secret word
            char[] secretWordCharArray=secretWord.ToCharArray();
            //list for found letters
            // - - - - - - -
            List<char> foundLetters=new List<char>(new string('-',secretWord.Length));
            
                List<char> incorrectLetters=new List<char>();
            // console out foundletters array
            
            while(true)
            {
                Console.Clear();
                Console.WriteLine(@"Welcome to Hangman!
Select one of the options below:
-P - Play game with preset words

-M - Multiplayer, choose your own word and play with friends");

                char menuInput=char.ToUpper(Console.ReadKey().KeyChar);
                switch(menuInput) 
                {
                case 'P':
                    goto Game;
                    
                case 'M':
                    Console.Clear();
                    Console.WriteLine("\nEnter your secret word(make sure the person/people guessing can't see)");
                    var input = Console.ReadLine();
                    secretWord = input?.ToUpper() ?? string.Empty;
                    secretWordCharArray=secretWord.ToCharArray();
                    foundLetters=new List<char>(new string('-',secretWord.Length));
                    goto Game;
                
                }                
            }
            Game:
            int incorrectCounter = 0;
            
            while (true) 
            {
                
                Console.Clear();
                Console.WriteLine(message);
                Console.WriteLine($"Incorrect Letters: {string.Join("", incorrectLetters)}");
                Console.WriteLine(string.Join(" ",foundLetters));

                if (incorrectCounter == 7) 
                {
                    Console.WriteLine($"\nGame Over! The word was {secretWord}");
                    Console.WriteLine("Press R to restart or any other key to close program");
                    char gameEndIndput=char.ToUpper(Console.ReadKey().KeyChar);
                    if(gameEndIndput=='R') goto Start;

                    break;
                } 
                else if (!foundLetters.Contains('-'))
                {
                    Console.WriteLine("\nYou won!");
                    Console.WriteLine("Press R to restart or any other key to close program");
                    char gameEndIndput=char.ToUpper(Console.ReadKey().KeyChar);
                    if(gameEndIndput=='R') goto Start;

                    break;
                }
                Console.WriteLine("Enter a letter");
                char userInput=char.ToUpper(Console.ReadKey().KeyChar); // stops input when user has pressed a key
                

                
                if (!secretWordCharArray.Contains(userInput))
                {
                    
                    if (!incorrectLetters.Contains(userInput) && !foundLetters.Contains(userInput))
                    {
                        incorrectLetters.Add(userInput);
                        gameThings.UpdateHangman(incorrectCounter);
                        message = gameThings.Hangman;
                        incorrectCounter++;
                    }
                    else if (incorrectLetters.Contains(userInput))
                    {
                        Console.WriteLine($"\nTHE LETTER {userInput} HAS ALREADY BEEN GUESSED!!!");
                    }
                }
            
                // to compare my char with secret word char
                // i create a foreach loop
                int counter=0;
                foreach(char c in secretWordCharArray)
                {
                    if (c==userInput)
                    {
                        foundLetters[counter]=c;
                    }
                    
                    
                    counter++;
                }

                
                

                
            }
        }
    }
}


           
           
