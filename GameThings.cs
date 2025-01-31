namespace Hangman1
{
    public class GameThings
    {
        public string Hangman{get;set;} = @$"
    ___________            
    |            
    |            
    |           
    |           
    |              
=========================";

        public static char[] HangmanArray{get;set;} = new char[7];

        public static string GetRandomWordFromStaticList()
        {
            string[] predefinedWords = {"apple", "banana", "cherry", "elderberry", "pineapple",};
            Random random = new Random();
            int randomIndex = random.Next(predefinedWords.Length);
            return predefinedWords[randomIndex];
        }

        public void UpdateHangman(int counter)
        {
            char[] SymbolArray = ['|', 'o', '/', '|', '\\', '/', '\\'];
            HangmanArray[(counter)] = SymbolArray[(counter)];
            Hangman = @$"
            
    ___________            
    |         {HangmanArray[0]}    
    |         {HangmanArray[1]}    
    |        {HangmanArray[2]}{HangmanArray[3]}{HangmanArray[4]}   
    |        {HangmanArray[5]} {HangmanArray[6]}   
    |              
=========================";
        }
    }
}