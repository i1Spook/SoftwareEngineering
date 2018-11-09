using System;

namespace NotThatNumber
{
  class Program
  {
    static int roundCounter = 0;
    static void Main(string[] args)
    {
      ShowHighscore();
      PlayGame();
    }

    public static void ShowHighscore()
    {
      if (!(roundCounter == 0))
      {
        Console.WriteLine("Type in your name to save your Highscore!");
        string name = Console.ReadLine();
        int highScore = 0;
        if (roundCounter > highScore)
        {
          highScore = roundCounter;
          Console.WriteLine($"{name} holds the new record with {highScore} rounds!");
        }
        else
        {
          Console.WriteLine($"{name} holds the record with {highScore} rounds!");
        }        
      }
    }

    public static void PlayGame()
    {
      var replay = 0;
      do
      {
        roundCounter = 1;

        //Number input
        Console.WriteLine("Geben Sie eine Zahl zwischen 1 und 4 ein.");
        int number;
        number = Convert.ToInt32(Console.ReadLine());

        //Random number generation
        var randomGenerator = new Random();
        var randomNumber = randomGenerator.Next(1, 5);
        Console.WriteLine($"Zufällige Zahl = {randomNumber}");

        //Decide if player won
        if (number == randomNumber)
        {
          Console.WriteLine("OOF, YOU LOST!");
          roundCounter = 0;
        }
        else
        {
          Console.WriteLine("YOU WON!");
        }

        //Replay?
        Console.WriteLine("Nochmal? 1 für Ja, 0 für Nein.");
        var answer = Convert.ToInt32(Console.ReadLine());
        if (answer == 1)
        {
          replay = 1;
          roundCounter++;
        }
        else
        {
          ShowHighscore();
        }
      }
      while (replay == 1);
    }
  }
}
