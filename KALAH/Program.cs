using System.Security.Cryptography;

namespace Kalah
{
    public class Program
    {
        static public void Main(String[] agrs)
        {
            int[] seedsRange = {1,4};
            int[] houseRange = {6, 10};
            int storeNumber = 1;

            Console.WriteLine("Welcome to Kalah!");
            Console.WriteLine("Please initial the Game first.");

            int initialSeedsNumber = getInitialSeedsNumber(seedsRange);
            int initialHouseNumber = getInitialHouseNumber(houseRange);

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("You set the seeds ( " + initialSeedsNumber + " )");
            Console.WriteLine("You set the house number ( " + initialHouseNumber + " )");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("You can choose game direaction.");
            Direction direction = GetValidDirection();
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"You have entered a valid direction: {direction}");
            Console.WriteLine("--------------------------------------------------------------------");

            Console.WriteLine("You can choose your opponent.");

            string opponent = chooseValidOpponent();
            opponent = opponent.ToLower();
            
            Player player1 = new HumanPlayer(PlayerName.Player1, initialHouseNumber, initialSeedsNumber, storeNumber, direction);
            Player player2 = null;
            if (opponent == "h" || opponent == "human")
                player2 = new HumanPlayer(PlayerName.Player2,initialHouseNumber, initialSeedsNumber, storeNumber, direction); 
            else if (opponent == "c" || opponent == "computer")
                player2 = new ComputerPlayer(initialHouseNumber, initialSeedsNumber, storeNumber, direction);
            else
                Console.WriteLine("Enter neither c or h.");
            

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"You have entered a valid opponent: {player2.getPlayerName()}");
            Console.WriteLine("--------------------------------------------------------------------");


            Console.WriteLine("Players created!");
            Console.WriteLine();
            Console.WriteLine("Now game ready!");
            Console.WriteLine("3, 2, 1...");
            Console.WriteLine("Game Start!");
    
            Player gameStartByPlayer = player1;
            Game game = new Game(player1, player2, direction, gameStartByPlayer, storeNumber);
            game.start();
        }

        static private int getInitialSeedsNumber(int[] seedsRange)
        {
            int initialSeeds = 0;
            string userInput;
            bool isValidInput = false;
            while(!isValidInput)
            {
                Console.WriteLine("Please enter the seeds in each house(" + seedsRange[0] + "-" +seedsRange[1] + " ): ");
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out initialSeeds) && initialSeeds >= seedsRange[0] && initialSeeds <= seedsRange[1])
                    isValidInput = true;
                else 
                    Console.WriteLine("Your input is not valid! Please enter a number between "+ seedsRange[0] + " and " + seedsRange[1] + ".");
            }
            return initialSeeds;
        }

        static private int getInitialHouseNumber(int[] houseRange)
        {
            int initialHouseNumber = 0;
            string userInput;
            bool isValidInput = false;
            while(!isValidInput)
            {
                Console.WriteLine("Please enter house number(" + houseRange[0] + "-" + houseRange[1] + " ): ");
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out initialHouseNumber) && initialHouseNumber >= houseRange[0] && initialHouseNumber <= houseRange[1])
                    isValidInput = true;
                else 
                    Console.WriteLine("Your input is not valid! Please enter a number between "+ houseRange[0] + " and " + houseRange[1] + ".");
            }
            return initialHouseNumber;
        }

        private static Direction GetValidDirection()
        {
            Direction direction =  Direction.Vertical;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Please enter a direction (Vertical/Horizontal or v/h): ");
                string userInput = Console.ReadLine();

                if (isValidDirection(userInput, out direction))
                    isValidInput = true;
                else
                    Console.WriteLine("Your input is not valid! Please enter 'Vertical', 'Horizontal', 'v', or 'h'.");
            }
            return direction;
        }

        static private bool isValidDirection(string input, out Direction direction)
        {
            direction = default(Direction);
            if (string.IsNullOrWhiteSpace(input))
                return false;
            input = input.Trim().ToLower();
            if (input == "v")
            {
                direction = Direction.Vertical;
                return true;
            }
            else if (input == "h")
            {
                direction = Direction.Horizontal;
                return true;
            }
            return Enum.TryParse<Direction>(input, true, out direction);
        }

        static private string chooseValidOpponent()
        {
            Console.WriteLine("You can choose play with computer or with another player.");
            bool isValidInput = false;
            string opponent = null;
            while(!isValidInput)
            {
                Console.WriteLine("Please enter Computer or Human(c/h):");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input can't be null.");
                    continue;
                }
                input = input.Trim().ToLower();
                if (input == "h" || input == "c" || input =="human" || input == "computer")
                    isValidInput = true;
                    opponent = input;
            }
            return opponent;
        }
    }
}