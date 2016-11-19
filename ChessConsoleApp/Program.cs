using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // instantiating a new chess board game object
            ChessBoard game = new ChessBoard();

            // taking player 1's name && RANDOMIZING their color
            Console.WriteLine("\nCHESS APP - coded by \'lenaggar\'");
            Console.Write("\n  please enter the first player's name:  > ");
            string name1 = Console.ReadLine();
            int randomColor = game.colorSelector();
            char color1 = (randomColor % 2 != 0) ? 'w' : 'b';
            Console.Write("\n  good day {0}, you're taking the \'{1}\' color", name1, (color1 == 'w') ? "White" : "Black");
            if (color1 == 'w')
                Console.Write("\n  YOU'RE PLAYING FIRST!");
            Console.Write("\n\nPress any key to take the second player's name...");
            Console.ReadLine();
            Console.Clear();

            // taking player 2's name && taking the OTHER color
            Console.WriteLine("\nCHESS APP - coded by \'lenaggar\'");
            Console.Write("\n  please enter the second player's name:  > ");
            string name2 = Console.ReadLine();
            char color2 = (color1 == 'w') ? 'b' : 'w';
            Console.Write("\n  good day to you too {0}, you're taking the \'{1}\' color", name2, (color2 == 'w') ? "White" : "Black");
            if (color2 == 'w')
                Console.Write("\n  YOU'RE PLAYING FIRST!");
            Console.Write("\n\nPress any key to start playing...");
            Console.ReadLine();
            Console.Clear();

            // instantiating players
            Player player1 = new Player(name1, color1);
            Player player2 = new Player(name2, color2);

            // creating the gameboard array with all the peices in the right places
            Piece[,] gameBoard = game.init(color1, color2);

            // start GAME LOOP with the board, the players as parameters
            game.playGame(gameBoard, player1, player2);
        }
    }
    class ChessBoard
    {
        public Piece[,] init(char p1Color, char p2Color)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Piece[,] board = new Piece[8, 8];
            board[0, 0] = new Rook(p2Color);
            board[1, 0] = new Knight(p2Color);
            board[2, 0] = new Bishop(p2Color);
            board[5, 0] = new Bishop(p2Color);
            board[6, 0] = new Knight(p2Color);
            board[7, 0] = new Rook(p2Color);
            if (p2Color == 'w')
            {
                board[3, 0] = new King(p2Color);
                board[4, 0] = new Queen(p2Color);
            }
            else
            {
                board[4, 0] = new King(p2Color);
                board[3, 0] = new Queen(p2Color);
            }
            for (int i = 0; i < 8; i++)
                board[i, 1] = new Pawn(p2Color);


            board[0, 7] = new Rook(p1Color);
            board[1, 7] = new Knight(p1Color);
            board[2, 7] = new Bishop(p1Color);
            board[5, 7] = new Bishop(p1Color);
            board[6, 7] = new Knight(p1Color);
            board[7, 7] = new Rook(p1Color);
            if (p1Color == 'w')
            {
                board[4, 7] = new King(p1Color);
                board[3, 7] = new Queen(p1Color);
            }
            else
            {
                board[3, 7] = new King(p1Color);
                board[4, 7] = new Queen(p1Color);
            }
            for (int i = 0; i < 8; i++)
                board[i, 6] = new Pawn(p1Color);
            return board;
        }
        public void boardDraw(Piece[,] arr)
        {
            Console.Clear();
            Console.WriteLine("\nCHESS APP - coded by \'lenaggar\'");
            for (int i = 7; i >= 0; i--)
            {
                string fullBlank = "\u2588";
                string space = "\u0020";
                string y = (i % 2 == 0) ? fullBlank : space;
                string x = (i % 2 == 0) ? space : fullBlank;
                string place;
                string placeColor;
                
                // first blank 1/3 of current row
                Console.Write("\n{0}{0}{0}|", space);
                for (int j = 0; j < 8; j++)
                {
                    placeColor = (j % 2 == 0) ? x : y;
                    Console.Write("{0}{0}{0}{0}{0}{0}|", placeColor);
                }

                // second main 1/3 of current row
                Console.Write("\n{0}{1}{0}|", space, (i + 1));
                for (int j = 0; j < 8; j++)
                {
                    if (arr[j,i] != null)
                    {
                        place = arr[j, i].symbol.ToString();
                        if (j % 2 == 0)
                        {
                            placeColor = x;
                            Console.Write("{0}{0}{1}{0}{0}|", placeColor, place);
                        }
                        else
                        {
                            placeColor = y;
                            Console.Write("{0}{0}{1}{0}{0}|", placeColor, place);
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            place = x + x;
                            placeColor = x;
                            Console.Write("{0}{0}{1}{0}{0}|", placeColor, place);
                        }
                        else
                        {
                            place = y + y;
                            placeColor = y;
                            Console.Write("{0}{0}{1}{0}{0}|", placeColor, place);
                        }
                    }
                }

                // third blank 1/3 of current row
                Console.Write("\n{0}{0}{0}|", space);
                for (int j = 0; j < 8; j++)
                {
                    placeColor = (j % 2 == 0) ? x : y;
                    Console.Write("{0}{0}{0}{0}{0}{0}|", placeColor);
                }
            }

            // board index at the bottom
            Console.Write("\n      a      b      c      d      e      f      g      h\n");
        }
        public void playGame(Piece[,] arr, Player p1, Player p2)
        {
            boardDraw(arr);

            Player currentPlayer = (p1.color == 'w') ? p1 : p2;
            Console.Write("\nWanna move a piece {0}?\n type its position on board (eg. \'d4\'):  > ", currentPlayer.name);
            string moveFrom = Console.ReadLine().ToLower();
            while (!validSquare(moveFrom))
            {
                boardDraw(arr);
                Console.Write("\nPlease, {0}, enter a valid position (eg. \'d4\'):  > ", currentPlayer.name);
                moveFrom = Console.ReadLine();
            }

            convertToProperIndex (moveFrom);





            Console.Write("OK {0}, and where do you wanna move?\n type the position:  > ", currentPlayer.name);
            string moveTo = Console.ReadLine().ToLower();
            validSquare(moveTo);





        }
        public bool validSquare(string p)
        {
            char col = Convert.ToChar(p.Substring(0, 1));
            int row = int.Parse(p.Substring(1, 2));
            bool check = (col >= 'a' && col <= 'h' && row >= 1 && row <= 8);
            return check;
        }
        public int convertToProperIndex (string p)
        {

            return 15;
        }
        public int colorSelector()
        {
            var rnd = new Random();
            return rnd.Next(1, 3);
        }
    }
    class Player
    {
        public string name { get; set; }
        public char color { get; set; }
        public Player(string name, char color)
        {
            this.name = name;
            this.color = color;
        }
    }
    class Piece
    {
        public char symbol { get; set; }
        public void symbolSetter(char c, char objChar)
        {
            if (c == 'w')
            {
                switch (objChar)
                {
                    case 'k':
                        symbol = '\u2654';
                        break;
                    case 'q':
                        symbol = '\u2655';
                        break;
                    case 'r':
                        symbol = '\u2656';
                        break;
                    case 'b':
                        symbol = '\u2657';
                        break;
                    case 'n':
                        symbol = '\u2658';
                        break;
                    case 'p':
                        symbol = '\u2659';
                        break;
                }
            }
            else
            {
                switch (objChar)
                {
                    case 'k':
                        symbol = '\u265A';
                        break;
                    case 'q':
                        symbol = '\u265B';
                        break;
                    case 'r':
                        symbol = '\u265C';
                        break;
                    case 'b':
                        symbol = '\u265D';
                        break;
                    case 'n':
                        symbol = '\u265E';
                        break;
                    case 'p':
                        symbol = '\u265F';
                        break;
                }
            }

        }
    }
    class King : Piece
    {
        public King(char c)
        {
            symbolSetter(c, 'k');
        }
    }
    class Queen : Piece
    {
        public Queen(char c)
        {
            symbolSetter(c, 'q');
        }
    }
    class Rook : Piece
    {
        public Rook(char c)
        {
            symbolSetter(c, 'r');
        }
    }
    class Bishop : Piece
    {
        public Bishop(char c)
        {
            symbolSetter(c, 'b');
        }
    }
    class Knight : Piece
    {
        public Knight(char c)
        {
            symbolSetter(c, 'n');
        }
    }
    class Pawn : Piece
    {
        public Pawn(char c)
        {
            symbolSetter(c, 'p');
        }
        public bool isLegelMove()
        {
            return true;
        }
        public void move()
        {

        }
    }
}
