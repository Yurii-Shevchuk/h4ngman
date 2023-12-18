using static System.Console;
public class Game{
    private readonly string _filepath;

    public Game(string filepath)
    {
        _filepath = filepath;
        WordToGuess = PickWord();
        EncodedWord = new string('*', WordToGuess.Length);
    }

    private string EncodedWord {get; set;}
    private string WordToGuess {get; set;}
    private string UsedLetters {get; set;} = "";

    public bool GameLoop()
    {
        int numberOfTries = 0;
        FancyConsole();
        WriteLine($"Word you need to guess: {EncodedWord}");
        WriteLine($"Word you need to guess(deciphered): {WordToGuess}");

        while(numberOfTries < 6)
        {
            if(String.Equals(EncodedWord, WordToGuess))
            {
                return true;
            }
            char guess = ReadGuess();
            if(!CheckGuess(guess))
            {
                numberOfTries++;
                WriteLine($"You've only got {6 - numberOfTries} tries left, try harder!");
            }
            WriteLine(EncodedWord);
            WriteLine($"Letters already used:{UsedLetters}");
        }
        WriteLine($"The word was {WordToGuess}");
        return false;
    }

    private bool CheckGuess(char letter)
    {
        bool isGuessed = false;
        if(WordToGuess.Contains(letter))
        {
            isGuessed = true;
            List<int> indices = GetAllIndices(letter);
            foreach(int index in indices)
            {
                DecipherPartly(index, letter);
            }
        }
        AddUsedLetter(letter);
        return isGuessed;
    }

    private List<int> GetAllIndices(char character)
    {
        return Enumerable.Range(0, WordToGuess.Length)
                         .Where(i => WordToGuess[i] == character)
                         .ToList();
    }
    private void AddUsedLetter(char letter)
    {
        if(!UsedLetters.Contains(letter))
        {
            UsedLetters += $" {letter},";
        }
    }
    private void DecipherPartly(int index, char letter)
    {
        EncodedWord = EncodedWord.Substring(0, index) + letter + EncodedWord.Substring(index + 1);
    }

    private char ReadGuess()
    {
        WriteLine("Please write one letter to guess");
        bool isGoodGuess = false;
        string guess = String.Empty;
        while (!isGoodGuess)
        {
            guess = ReadLine().ToLower();
            if (guess.Length == 1)
                isGoodGuess = true;
        }
        return Convert.ToChar(guess);
    }

    private string PickWord()
    {
        Random random = new Random();
        int length = File.ReadAllLines(_filepath).Length;
        int lineToFind = random.Next(0, length+1);
        using(var reader = new StreamReader(_filepath))
        {
            int currentLine = 0;
            string word;
             while ((word = reader.ReadLine()) != null)
                {
                    currentLine++;
                    if (currentLine == lineToFind)
                    {
                        return word;
                    }
                }
        }
        return string.Empty;
    }

    private void FancyConsole()
    {
        BackgroundColor = ConsoleColor.White;
            Clear();
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine(@"
                            ██╗  ██╗ █████╗ ███╗   ██╗ ██████╗ ███╗   ███╗ █████╗ ███╗   ██╗
                            ██║  ██║██╔══██╗████╗  ██║██╔════╝ ████╗ ████║██╔══██╗████╗  ██║
                            ███████║███████║██╔██╗ ██║██║  ███╗██╔████╔██║███████║██╔██╗ ██║
                            ██╔══██║██╔══██║██║╚██╗██║██║   ██║██║╚██╔╝██║██╔══██║██║╚██╗██║
                            ██║  ██║██║  ██║██║ ╚████║╚██████╔╝██║ ╚═╝ ██║██║  ██║██║ ╚████║
                            ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝
                                                                


");
            Write("Welcome to the game of life and death! You' will be given a word and only ");
            ForegroundColor = ConsoleColor.DarkBlue;
            Write("6 tries ");
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine("to guess it or you will die (but try not to), good luck with guessing!");
    }
}