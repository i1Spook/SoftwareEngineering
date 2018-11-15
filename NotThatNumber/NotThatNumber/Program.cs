using System;
using System.IO;

namespace NotThatNumber
{
  class Program
  {
    static int currentScore = 0;

    static int highScore = 0;

    static void Main(string[] args)
    {
      Initialize();

      PlayGame();
    }

    private static void Initialize()
    {
      //  Read and print old Highscore from cache (textfile):
      StreamReader reader = new StreamReader(@"E:\\highscore.txt");

      var highScoreTxt = reader.ReadLine();

      reader.Close();

      Console.WriteLine("Current Highscore:");
      Console.WriteLine(highScoreTxt);

      //  Get old Highscore from cache (textfile):
      var splitText = highScoreTxt.Split(':');

      var oldHighScore = Convert.ToInt32(splitText[1]);

      highScore = oldHighScore;
    }

    static void PlayGame()
    {
      var playAgain = false;

      do
      {
        Console.WriteLine("Type in a number between 1 and 4!");

        var yourNumber = 0;

        ProtectedNumberInput(out yourNumber);

        GetGameResult(yourNumber);

        Console.WriteLine("Again? Yes(y) or No(n).");

        ProtectedReplayInput(out playAgain);

        if (!playAgain)
        {
          UpdateHighscore();

          Environment.Exit(0);
        }
      }
      while (playAgain);
    }

    static void UpdateHighscore()
    {      
      if (currentScore > highScore)
      {
        SaveNewHighscore();

        Console.WriteLine("Your new Highscore has been saved!");
        Console.WriteLine("New Round? Yes(y) or No(n).");

        ProtectedReplayInput(out bool playAgain);

        if (!playAgain)
        {
          Environment.Exit(0);
        }
      }
      else
      {
        Console.WriteLine("Sorry, you didn't beat the Highscore!");
      }
    }

    static void GetGameResult(int yourNumber)
    {
      var randomGenerator = new Random();

      var randomNumber = randomGenerator.Next(1, 5);

      Console.WriteLine($"Not that Number! ({randomNumber})");

      if (yourNumber == randomNumber)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("OOF, YOU LOST! Your Score has been reset.");
        Console.ForegroundColor = ConsoleColor.White;

        currentScore = 0;
      }
      else
      {
        currentScore++;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"YOU WON! (Score:{currentScore})");
        Console.ForegroundColor = ConsoleColor.White;
      }
    }

    static void ProtectedNumberInput(out int yourNumber)
    {
      var yourInput = Console.ReadLine();

      var correctInput = Int32.TryParse(yourInput, out yourNumber);

      while (!correctInput || yourNumber < 1 || yourNumber > 4)
      {
        Console.WriteLine("Please enter a numerical value between 1 and 4!");

        yourInput = Console.ReadLine();

        correctInput = Int32.TryParse(yourInput, out yourNumber);
      }
    }

    static void ProtectedReplayInput(out bool playAgain)
    {
      var answer = Console.ReadLine();

      while (!(answer == "y" || answer == "n"))
      {
        Console.WriteLine("Wrong input. Please answer with y or n!");

        answer = Console.ReadLine();
      }
      if (answer == "y")
      {
        playAgain = true;
      }
      else
      {
        playAgain = false;
      }
    }
    static void SaveNewHighscore()
    {
      var newHighScore = currentScore;

      Console.WriteLine($"Type in your name to save your new Highscore of {newHighScore}!");

      var name = Console.ReadLine();

      while (name.Contains(":"))
      {
        Console.WriteLine("Invalid characters used. Please enter your name again!");

        name = Console.ReadLine();
      }

      StreamWriter writer = new StreamWriter(@"E:\\highscore.txt");

      writer.WriteLine($"{name}:{newHighScore}");

      writer.Close();

      highScore = newHighScore;
    }
  }
}