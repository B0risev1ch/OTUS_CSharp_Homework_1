//RegEx? :)
using System.Reflection;

var commandDict = new Dictionary<string, string>()
{
    {   @"\start", "to introduce yourself" },
    {   @"\help",  "to show this message" },
    {   @"\info",  "to get the information about this app and assemblies" },
    {   @"\echo",  "to echo" },
    {   @"\exit",  "to exit" },
    {   @"\Y", "to say yes"},
    {   @"\N", "to say no"}
};

(string? command, string? attribute) userInput;
string? userName = null;

string commands = $"{(string.Join("\n", commandDict.Select(kvp => kvp.Key + "\t-\t" + kvp.Value))) }";
Console.WriteLine($"Welcome! Here is the list of commands you might want to use:\n{commands}");

do
{
    Console.WriteLine($"{(userName ?? "Stranger")}, enter yor command:");
    userInput = ParseCommandAndAttribute(Console.ReadLine());

    switch (userInput.command)
    {
        case "\\start":
            {
                if (userName != null)
                {
                    Console.WriteLine($"Your name is {userName}, rename?\n\\Y, \\N");

                    var (command, _) = ParseCommandAndAttribute(Console.ReadLine());
                        switch ((string?)command?.ToUpperInvariant())
                        {
                        case @"\Y":
                        {
                            Console.WriteLine($"Who are you, stranger?");
                            userName = Console.ReadLine();
                            Console.WriteLine($"\'{userName}\', so it be!");
                            break;
                        }
                        case @"\N":
                        {
                            Console.WriteLine($"\'{userName}\', so it be!");
                            break;
                        }
                        default:
                            Console.WriteLine($"Command unknown, returning back");
                            break;
                        }
                }
                else
                {
                    Console.WriteLine($"Who are you, stranger?");
                    userName = Console.ReadLine();
                    Console.WriteLine($"\'{userName}\', so it be!");
                }
                break;
            }
        case "\\info":
            {
                Console.WriteLine($"{userName}, info is here:");
                Console.WriteLine($"Program version: {Assembly.GetExecutingAssembly().GetName().Version}");
                Console.WriteLine($"Assemblies:\n{(string.Join("\n", AppDomain.CurrentDomain.GetAssemblies().ToList()))}");
                break;
            }
        case "\\exit":
            {
                Console.WriteLine($"See you, {userName}!");
                break;
            }
        case "\\echo":
            {
                Console.WriteLine($"{userInput.attribute}");
                break;
            }
        case "\\help":
            {
                Console.WriteLine($"{userName}, here is the list of commands you might want to use:\n{commands}");
                break;
            }
        default:
            {
                Console.WriteLine($"Command unknown, {userName}, try again");
                break;
            }
    }

} while (userInput.command != @"\exit");
(string? command, string? attribute) ParseCommandAndAttribute(string? inputLine)
{
    if (inputLine != null)
    {
        var commandLastIndex = inputLine.IndexOf(' ') > 0 ? inputLine.IndexOf(' ') : inputLine.Length;

        var command = inputLine[..commandLastIndex];

        var attribute = commandLastIndex + 1 < inputLine.Length ?
            inputLine.Substring(commandLastIndex + 1, inputLine.Length - commandLastIndex - 1)
            : null;
        var commandList = commandDict.Keys;
        return commandList.Contains(command) ? (command, attribute) : (null, null);
    }
    else
        return (null, null);
}