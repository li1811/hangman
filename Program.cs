using System;

namespace Hangman1
{

    internal class Program 
    {

        public async static Task Main(string[] args)
        {
            GameThings gameThings = new GameThings();
            Start:
            string secretWord = "";

            // While loop for main menu
            while(true)
            {
                Console.Clear();
                Console.WriteLine(@"Welcome to Hangman!
Select one of the options below:
-P - Play with random word(pulls a random word from internet)

-M - Multiplayer, choose your own word and play with friends");

                char menuInput=char.ToUpper(Console.ReadKey().KeyChar);
                switch(menuInput) 
                {
                case 'P':
                    
                    bool difficultySelected = false;
                    while(difficultySelected == false)
                    {
                        Console.Clear();
                        Console.WriteLine(@$"Select a difficulty option:
1- Easy(3-6 letters)
2- Medium(6-10 letters)
3- Hard(10+ letters)
0- Random");

                        char difficulty = char.ToUpper(Console.ReadKey().KeyChar);
                        
                        Console.WriteLine("Loading...");
                        secretWord = await GameThings.RandomWord(difficulty);
                        difficultySelected = true;
                    }
                    goto Game;
                    
                case 'M':
                    // If multiplayer is selected we overwrite the 
                    Console.Clear();
                    Console.WriteLine("\nEnter your secret word(make sure the person/people guessing can't see)");
                    var input = Console.ReadLine();
                    secretWord = input?.ToUpper() ?? string.Empty;
                    goto Game;
                
                }                
            }
            Game:
            int incorrectCounter = 0;
            
            // Set starting state of hangman drawing and picks a random work from static preset list.
            string message = gameThings.Hangman;
            GameThings.HangmanArray = new char[7];
            
            // Set secretWord to uppercase and trim whitespace, then conver to character array
            secretWord=secretWord.ToUpper().Trim();   
            char[] secretWordCharArray=secretWord.ToCharArray();
            
            // Lists used to display the first letters and incorrent letters
            List<char> foundLetters=new List<char>(new string('-',secretWord.Length));
            List<char> incorrectLetters=new List<char>();
            
            
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
                        message = gameThings.UpdateHangman(incorrectCounter);
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


           
           
