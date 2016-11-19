using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyApp
{
    class MyClass
    {
        static void Main(string[] args)
        {
            //  setup players array
            Player[] players = new Player[2];
            players[0] = new Player('X');
            players[1] = new Player('O');

            //  assign players' names
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("CONNECT4 APP");
                Console.Write("\nPlayer {0}, please enter your name:\n  > ", i + 1);
                players[i].Name = Console.ReadLine();
                Console.Write("\n\n Hi {0}, you will be playing with the '{1}' coin.\n   press enter to continue...", players[i].Name, players[i].Coin);
                Console.ReadLine();
                Console.Clear();
            }

            //  The Connect4 Array constructor
            char[,] connect4 = new char[7, 6];

            //  assign a space char ' ' to each cell for Console.Write preview purposes
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 6; j++)
                    connect4[i, j] = ' ';

            //  connect4 game main endless loop
            byte index = 1;
            for (int x = 0; x < 42; x++)
            {
                //  connect4 array drawer
                Console.WriteLine("CONNECT4 APP\n");
                for (int j = 5; j >= 0; j--)
                {
                    Console.Write("|     |     |     |     |     |     |     |\n|");
                    for (int i = 0; i < 7; i++)
                        Console.Write("  {0}  |", connect4[i, j]);
                    Console.WriteLine("\n|_____|_____|_____|_____|_____|_____|_____|");
                }
                Console.WriteLine("\n   1     2     3     4     5     6     7   \n");

                //  current player
                Player currentPlayer;
                if (index % 2 > 0)
                    currentPlayer = players[0];
                else
                    currentPlayer = players[1];
                index++;

                while (true)
                {
                    // user column input
                    Console.Write("\n{0}, Please type a column number to drop your coin\n  > ", currentPlayer.Name);
                    int column = int.Parse(Console.ReadLine());
                    if (connect4[column - 1, 5] != ' ')
                        Console.WriteLine("\nThis column is Full, please choose another");
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (connect4[column - 1, i] == ' ')
                            {
                                connect4[column - 1, i] = currentPlayer.Coin;
                                break;
                            }
                        }
                        break;
                    }
                }
                Console.Clear();
            }
        }
    }
    class Player
    {
        public Player(char coin)
        {
            this.Coin = coin;
        }
        public string Name { get; set; }
        public char Coin { get; set; }
    }
}