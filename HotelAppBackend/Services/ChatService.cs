public interface IChatService
{
    Task AskQuestion(string Question);
    Task<string> GetForbiddenWords();
}

public class ChatService : IChatService
{

    static readonly HttpClient client = new HttpClient();
    public async Task AskQuestion(string Question)
    {
        string Words = await GetForbiddenWords();
        List<string> wordsSeperated2 = new List<string>(){
            "wine",
            "beer",
            "vodka",
            "whiskey",
            "rum"
        };

        foreach(string Word in wordsSeperated2){
            if(Question.Contains(Word)){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                return new HttpStatusCodeResult(HttpStatusCode.Ok);
            }
        }
    }

    public async Task<List<Word>> GetForbiddenWords()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("https://backendapi.mangotree-eedbf301.westeurope.azurecontainerapps.io/forbiddenwords");
            List<Word> words = await response.Content.ReadFromJsonAsync();
            return words;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }

}