namespace Hangman1
{
    public class RandomWord
    {
        public List<string> RandomWordList{get;set;} = new List<string>();

        public RandomWord() {}

        // Populate list
        public void PopulateList(string newWord) 
        {
            RandomWordList.Add(newWord);
        }
        
        // Method to pick a random word from RandomWordList
        public string PickWord()
        {
            Random random = new Random();
            int listLength = RandomWordList.Count;
            int randomIndex = random.Next(listLength);
            string secretWord = RandomWordList[randomIndex];
            return secretWord;
        }

        public static string GetRandomWordFromStaticList()
        {
            string[] predefinedWords = {"apple", "banana", "cherry"};
            Random random = new Random();
            int randomIndex = random.Next(predefinedWords.Length);
            Console.WriteLine($"Index Number: {randomIndex}");
            return predefinedWords[randomIndex];
        }
    }
}