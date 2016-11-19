using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            char[] peices = new char[12];
            peices[0] = '\u2654';
            peices[1] = '\u265A';
            peices[2] = '\u2655';
            peices[3] = '\u265B';
            peices[4] = '\u2656';
            peices[5] = '\u265C';
            peices[6] = '\u2657';
            peices[7] = '\u265D';
            peices[8] = '\u2658';
            peices[9] = '\u265E';
            peices[10] = '\u2659';
            peices[11] = '\u265F';

            for (int i = 0; i < 12; i++)
            {
                Console.Write("{0}", peices[i]);
            }
            Console.WriteLine("\n");
            Console.ReadLine();

            //Console.WriteLine(Console.OutputEncoding);
            //Console.WriteLine(Encoding.Unicode);
            //Console.WriteLine(knight);


            Console.Write("\n\nPlease enter your garage capacity: ");
            int n = int.Parse(Console.ReadLine());
            Car[] garage = new Car[n];

            Console.WriteLine("\nNow this is your garage, it currently has {0} empty spot(s), and I'm here to help you manage it;\nSo what do you want me to do??", n);

            while (true)
            {
                while (true)
                {
                    Console.Write("\nYou can type\n  1 : if you want to add a new car to the garage,\n  2 : if you want to make a parked car VANISH from your garage, and\n  3 : if you want the current status of your garage.\n\n=> ");
                    int command;
                    if (int.TryParse(Console.ReadLine(), out command))
                    {
                        if (command == 1)
                        {
                            /************************
                            // ADD a new car
                            *************************/
                            if (garage[n - 1] != null)
                            {
                                Console.WriteLine("\nSorry, but there's no room for more cars.\nRemove a parked car first to enter a new one.");
                                break;
                            }
                            else
                            {
                                Console.Write("\n    What is the name of the car (please enter a unique name): ");
                                string carName = Console.ReadLine();
                                Console.Write("    And ");
                                while (true)
                                {
                                    Console.Write("what is the number of the car (please enter a unique number): ");
                                    int carNumber;
                                    if (int.TryParse(Console.ReadLine(), out carNumber))
                                    {
                                        for (int i = 0; i < n; i++)
                                        {
                                            if (garage[i] == null)
                                            {
                                                garage[i] = new Car();
                                                garage[i].Name = carName.First().ToString().ToUpper() + string.Join("", carName.Skip(1)).ToLower();
                                                //Console.WriteLine(garage[i].Name);
                                                garage[i].Number = carNumber;
                                                Console.WriteLine("\nSUCCESS\nYour car is now parked in spot number {0}.", (i + 1));
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    else
                                        Console.Write("\n    Please try again with a valid number.\n    ");
                                }
                                break;
                            }
                        }
                        //else if (command == 2)
                        //{
                        //    /************************
                        //    // REMOVE an existing car
                        //    *************************/
                        //    break;
                        //}
                        else if (command == 3 || command == 2)
                        {
                            /************************
                            // REPORT status
                                ( REMOVE an existing car in case of  )
                            *************************/
                            Console.WriteLine("\nYour Garage's current status is: ");
                            for (int i = 0; i < n; i++)
                            {
                                if (garage[i] != null)
                                {
                                    Console.WriteLine("\n      - Spot no. {0} : you have a parked car,", i);
                                    Console.WriteLine("            The '{0}' which number is {1}.", garage[i].Name, garage[i].Number);
                                }
                                else
                                    Console.WriteLine("\n      - Spot no. {0} : EMPTY.\n", i);
                            }
                            if (command == 2)
                            {
                                if (garage[0] == null)
                                {
                                    Console.Write("HOORAY\nNO CARS TO REMOVE!!!");
                                    break;
                                }
                                //Console.Write("\n\nYou can only remove cars from slots:\n");
                                int taken = 0;
                                Console.Write("\nWhich car do you want to remove? (please enter a its spot number)\nThere are cars parked in spot(s) => ");
                                for (int i = 0; i < n; i++)
                                {
                                    if (garage[i] != null)
                                    {
                                        if (i == 0)
                                            Console.Write("\n[ {0} ", (i + 1));
                                        else
                                            Console.Write(", {0} ", (i + 1));
                                        taken++;
                                    }
                                }
                                Console.Write("]\n\n    => ");
                                while (true)
                                {
                                    int toRemove;
                                    if (int.TryParse(Console.ReadLine(), out toRemove)) //good
                                    {
                                        if (toRemove - 1 > 0 && toRemove - 1 <= taken)
                                        {
                                            for (int i = toRemove - 1; i < n - 1; i++)
                                                garage[i] = garage[i + 1];
                                            garage[n - 1] = null;
                                            break;
                                        }
                                    }
                                    Console.Write("\n    Please enter a valid number\n\n    => ");
                                }
                            }
                            break;
                        }
                        Console.WriteLine("\nMan, how can I help you if you don't cooperate?\nPlease give me a valid number");
                    }
                    else
                    {
                        Console.WriteLine("\nPlease enter a number");
                    }
                }
            }
        }
    }
    class Car
    {
        public string Name;
        public int Number;
    }
}