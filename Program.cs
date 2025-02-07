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
                        Console.WriteLine(@$"Select a length option:
1- Short(3-6 letters)
2- Medium(6-10)
3- Long(10-20)
4- Very Long(20+)
0- Random");

                        char difficulty = Console.ReadKey().KeyChar;
                        char[] options = ['1', '2', '3', '4', '0'];
                        if (!options.Contains(difficulty))
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid input, try again");
                            Console.ReadKey();
                            continue;
                        }
                        Console.WriteLine("\nLoading...");
                        secretWord = await GameThings.RandomWord(difficulty);
                        difficultySelected = true;
                    }
                    goto Game;
                    
                case 'M':
                    Console.Clear();
                    Console.WriteLine("\nEnter your secret word(make sure the person/people guessing can't see)");
                    var input = Console.ReadLine();
                    secretWord = input?.ToUpper() ?? string.Empty;
                    goto Game;
                
                }                
            }
            Game:
            int incorrectCounter = 0;
            
            // Set starting state of hangman drawing
            string message = gameThings.Hangman;
            GameThings.HangmanArray = new char[7];
            
            // Set secretWord to uppercase and trim whitespace, then convert to character array
            secretWord=secretWord.ToUpper().Trim();   
            char[] secretWordCharArray=secretWord.ToCharArray();
            
            // Lists used to display the correctly guessed letters and incorrent letters
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
                char userInput=char.ToUpper(Console.ReadKey().KeyChar); 
                

                
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


           
           
