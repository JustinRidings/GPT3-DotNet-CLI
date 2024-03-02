// Your OpenAI API Key
GPT3Conversation c = new GPT3Conversation("<YourOaiApiKey>");

Console.WriteLine("Welcome to GPT3! Start typing to have a conversation! When you're done, press enter! \nTo end the conversation say: \"I'm done for now\"");

string input = string.Empty;

while(input != "I'm done for now")
{
    input += Console.ReadLine();
    var response = await c.GenerateResponse(input);
    Console.WriteLine(response);
    input = string.Empty;
}

c.Dispose();

return 1;
