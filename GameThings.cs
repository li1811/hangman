using System.Text.Json;
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

        public string UpdateHangman(int counter)
        {
            char[] SymbolArray = ['|', 'o', '/', '|', '\\', '/', '\\'];
            HangmanArray[(counter)] = SymbolArray[(counter)];
            return @$"
            
    ___________            
    |         {HangmanArray[0]}    
    |         {HangmanArray[1]}    
    |        {HangmanArray[2]}{HangmanArray[3]}{HangmanArray[4]}   
    |        {HangmanArray[5]} {HangmanArray[6]}   
    |              
=========================";
        }

        public async static Task<string> RandomWord(char difficulty)
        {
            Random r = new Random();
            int length = difficulty switch
            {
                '1' => r.Next(3, 7),
                '2' => r.Next(6, 11),
                '3' => r.Next(10, 20),
                '4' => r.Next(20, 45),
                _ => r.Next(45)
            };
            string url =  $"https://random-word-api.herokuapp.com/word?length={length}"; 
            using HttpClient client = new HttpClient();
            try
            {

                HttpResponseMessage response = await client.GetAsync(url);

                //if success
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    //deserialize 
                    string[] words = JsonSerializer.Deserialize<string[]>(data) ?? Array.Empty<string>();
                    string secretWord = words[0];
                    return secretWord;

                }
                else
                {
                    Console.WriteLine($"error {response.StatusCode}");
                    return response.StatusCode.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception", ex.Message);
                return ex.Message;
            }
        }
    }
}