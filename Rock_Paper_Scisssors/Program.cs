using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Rock_Paper_Scissors
{
    public class Gameresults
    {
        public int Player_score { get; set; }
        public int CPU_score { get; set; }
    }

    class Data
    {
        /*
        public int Player_score {  get; set; }
        public int CPU_score {  get; set; }*/

        public void Player(int input_Player)
        {
            if (input_Player == 1)
            {
                Console.WriteLine($"You choose option {input_Player}: Rock\n");
            }
            else if (input_Player == 2)
            {
                Console.WriteLine($"You choose {input_Player}: Paper\n");
            }
            else
            {
                Console.WriteLine($"You choose {input_Player}: Scissors\n");
            }
        }

        public void CPU(int input_CPU) 
        {
            if (input_CPU == 1) 
            {
                Console.WriteLine($"Computer choose {input_CPU}: Rock\n"); 
            }
            else if (input_CPU == 2)
            {
                Console.WriteLine($"Computer choose {input_CPU}: Paper\n");
            }
            else
            {
                Console.WriteLine($"Computer choose {input_CPU}: Scissors\n");
            }
        }

        public Gameresults Result(int input_Player, int input_CPU)
        {
            Gameresults result = new Gameresults();

            if (input_Player == 1 && input_CPU == 1)
            {
                Console.WriteLine(":::::::::::::::::::::::::: DRAW ::::::::::::::::::::::::::::::\n");
            }
            else if (input_Player == 1 && input_CPU == 2)
            {
                Console.WriteLine(":::::::::::::::::::::::::: Computer WON ::::::::::::::::::::::\n");
                result.CPU_score += 1;
            }
            else if (input_Player == 1 && input_CPU == 3)
            {
                Console.WriteLine(":::::::::::::::::::::::::: Player WON ::::::::::::::::::::::::\n");
                result.Player_score += 1;
            }


            else if (input_Player == 2 && input_CPU == 1)
            {
                Console.WriteLine(":::::::::::::::::::::::::: Player WON ::::::::::::::::::::::\n");
                result.Player_score += 1;
            }
            else if (input_Player == 2 && input_CPU == 2)
            {
                Console.WriteLine(":::::::::::::::::::::::::: DRAW ::::::::::::::::::::::\n");
            }
            else if (input_Player == 2 && input_CPU == 3)
            {
                Console.WriteLine(":::::::::::::::::::::::::: Computer WON ::::::::::::::::::::::\n");
                result.CPU_score += 1;
            }


            else if (input_Player == 3 && input_CPU == 1)
            {
                Console.WriteLine(":::::::::::::::::::::::::: Computer WON ::::::::::::::::::::::\n");
                result.CPU_score += 1;
            }
            else if (input_Player == 3 && input_CPU == 2)
            {
                Console.WriteLine(":::::::::::::::::::::::::: Player WON ::::::::::::::::::::::\n");
                result.Player_score += 1;
            }
            else if (input_Player == 3 && input_CPU == 3)
            {
                Console.WriteLine(":::::::::::::::::::::::::: DRAW ::::::::::::::::::::::\n");
            }


            else
            {
                Console.WriteLine("Oops You have enter Wrong input...");
            }

            return result;
        }
    }

    class Programme
    {
        static void Main(string[] args)
        {
            char ch = 'N';

            do
            {
                Data obj = new Data();
                //Gameresults gameresults = new Gameresults();
    
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*  @Anmol Agarwal Production  *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine("\n##################__Welcome to the ROCK_PAPER_SCISSORS__####################\n");
                Console.WriteLine("This is one player game, as second player will be computer who will make decision randomly..\n");
    
                Console.WriteLine("As you will be familiar, that there will be three options, to choose, as Rock or Paper or Scissors\n");
    
                Console.WriteLine(" 1: Rock\n 2: Paper\n 3: Scissor\nYou have to type number 1 or 2 or 3 to choose your action and in the meanwhile will choose one");

                int input_Player;
                int Random_Number;
    
                int player_score = 0;
                int cpu_score = 0;
    
                int i = 0;
                do
                {
                    Console.WriteLine($"\n+++++++++++++++++ ROUND : {i+1}");
    
                    Console.WriteLine("\nPlease Enter your choice in form of Number: ");
                    input_Player = int.Parse(Console.ReadLine());
                    obj.Player(input_Player);
    
                    Random random = new Random();
                    Random_Number = random.Next(1, 4);
                    //Console.WriteLine($"\nComputer chooses option: {Random_Number}");
                    obj.CPU(Random_Number);
    
                    //Console.WriteLine($"Result is : {obj.Result(input_Player, Random_Number)}\n");
                    //obj.Result(input_Player, Random_Number);
    
                    Gameresults gameresults = obj.Result(input_Player, Random_Number); 
    
                    player_score += gameresults.Player_score;
                    cpu_score += gameresults.CPU_score;
    
                    //Console.WriteLine($"Player score : {gameresults.Player_score}, Computer score : {gameresults.CPU_score}");
                    Console.WriteLine($"Player score : {player_score}, Computer score : {cpu_score}");
    
    
                    i++;
                } while (i < 3);
                Console.WriteLine($"\nTotal Player score : {player_score}, Total Computer score : {cpu_score}\n");
    
                if (player_score > cpu_score)
                {
                    Console.WriteLine("= = = = = = = = = = = = = = = = =  Congrates PLAYER you WON  = = = = = = = = = = = = = = = = =");
                }
                else if (player_score < cpu_score)
                {
                    Console.WriteLine("= = = = = = = = = = = = = = = = = You LOST !!! COMPUTER WON  = = = = = = = = = = = = = = = = =");
                }
                else
                {
                    Console.WriteLine("= = = = = = = = = = = = = = = = =  Game DRAWS  = = = = = = = = = = = = = = = = =");
                }
    
                Console.WriteLine("\n\n\n ^^^^^^^^^^^^^^^^ Do you want to continue this WonderfulGame ^^^^^^^^^^^^^^^ \n\nIf yes then please type Y or if exit press N");
                ch = Convert.ToChar(Console.ReadLine());
                Console.WriteLine($"You choose to {ch}");

            } while(ch == 'Y'); 
        }
    }
}
