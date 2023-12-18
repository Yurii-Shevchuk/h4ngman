// See https://aka.ms/new-console-template for more information
using System.Threading.Channels;

string filename = "words.txt";
string filepath = Path.Combine(Directory.GetCurrentDirectory(), filename);


Game game = new Game(filepath);
bool gameResult = game.GameLoop();
if(gameResult)
{
    Console.WriteLine("You won!");
}
else
{
    Console.WriteLine("You're dead x_x");
}


