using System.ComponentModel;

namespace Kalah
{
    public class Game
    {
        Player player1;
        Player player2;
        Direction direction;
        Player currentPlayer;
        int storeNumber;
        GameDashboard dashboard;

        bool changePlayer = true;
        public Game(Player player1, Player player2, Direction direction, Player gameStartByPlayer, int storeNumber)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.direction = direction;
            currentPlayer = gameStartByPlayer;
            this.storeNumber = storeNumber;
            dashboard = new GameDashboard(player1, player2, direction);
        }
        
        public void start()
        {
            if (player2.getPlayerName().Equals(PlayerName.Player2))
                playWithHuman();
            else
                playerWithComputer();
        }

        private void playWithHuman()
        {
            while(currentPlayerHasRestSeeds(getCurrentPlayer()))
            {
                dashboard.printGameBoard();
                Console.WriteLine(currentPlayer.getPlayerName() + "'s turn - Specify house number or 'q' to quit:");
                string userInput = Console.ReadLine();
                while (!isValidInput(userInput))
                {
                    Console.WriteLine(currentPlayer.getPlayerName() + "'s turn - Specify house number or 'q' to quit:");
                    userInput = Console.ReadLine();
                }
                if (userInput == "q" || userInput == "Q")
                {
                    quiteGame();
                    break;
                }
                else
                {
                    int chosenHouseName;
                    if (int.TryParse(userInput, out chosenHouseName))
                        if(hasSeedsInHouse(chosenHouseName))
                            makeMove(chosenHouseName);
                        else
                        {
                            Console.WriteLine("The house has no seeds.");
                            Console.WriteLine("Please choose another house.");
                            continue;
                        }
                }
                if (!currentPlayerHasRestSeeds(getCurrentPlayer()))
                    gameOver();
                else
                {
                    if(changePlayer)
                        currentPlayer = getNextPlayer(currentPlayer);
                }
            }
        }

        private void playerWithComputer()
        {
            while(currentPlayerHasRestSeeds(getCurrentPlayer()))
            {
                dashboard.printGameBoard();
                Console.WriteLine(currentPlayer.getPlayerName() + "'s turn - Specify house number or 'q' to quit:");
                string userInput;
                if (currentPlayer.Equals(player1))
                {
                    userInput = humanPlayerTurn();
                }
                else
                {
                    userInput = computerPlayerTurn().ToString();
                    Console.WriteLine("Computer choose: " + userInput);
                }
                if (userInput == "q" || userInput == "Q")
                {
                    quiteGame();
                    break;
                }
                else
                {
                    int chosenHouseName;
                    if (int.TryParse(userInput, out chosenHouseName))
                        if(hasSeedsInHouse(chosenHouseName))
                            makeMove(chosenHouseName);
                        else
                        {
                            Console.WriteLine("The house has no seeds.");
                            Console.WriteLine("Please choose another house.");
                            continue;
                        }
                }
                if (!currentPlayerHasRestSeeds(getCurrentPlayer()))
                    gameOver();
                else
                {
                    if(changePlayer)
                        currentPlayer = getNextPlayer(currentPlayer);
                }
            }
        }

        private string humanPlayerTurn()
        {
            string userInput = Console.ReadLine();
            while (!isValidInput(userInput))
            {
                Console.WriteLine(currentPlayer.getPlayerName() + "'s turn - Specify house number or 'q' to quit:");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
        private int computerPlayerTurn()
        {
            if (getAdditionalChance().hasAdditionalChance)
            {
                return getAdditionalChance().chosenHouseName;
            }
            else if (getCaptureChance().hasCaptureChance)
            {
                return getCaptureChance().chosenHouseName;
            }
            else
            {
                return getLowestIndexHouse();
            }
        }

        private (bool hasAdditionalChance, int chosenHouseName) getAdditionalChance()
        {
            int chosenHouseName;
            House[] houses = player2.getHouses();
            for (int i = 0; i < houses.Length; i ++)
            {
                House currentHouse = houses[i];
                int seeds = currentHouse.getSeeds();
                int currentHouseIndex = currentHouse.getIndex();
                int finalIndex = getFinalIndex(player2, currentHouseIndex, seeds);
                int targetHouseIndex = normalizeToRange(finalIndex, 1, currentPlayer.getHouses().Length * 2 + storeNumber * 2);
                if (targetHouseIndex == player2.getStore().getIndex())
                    return (true, currentHouse.getHouseName());
            }
            return (false, 0);
        }

        private (bool hasCaptureChance, int chosenHouseName) getCaptureChance()
        {
            int chosenHouseName;
            House[] houses = player2.getHouses();
            for (int i = 0; i < houses.Length; i ++)
            {
                House currentHouse = houses[i];
                int seeds = currentHouse.getSeeds();
                int currentHouseIndex = currentHouse.getIndex();
                int finalIndex = getFinalIndex(player2, currentHouseIndex, seeds);
                int targetHouseIndex = normalizeToRange(finalIndex, 1, currentPlayer.getHouses().Length * 2 + storeNumber * 2);
                if (player2.isPlayerHouse(targetHouseIndex) && seeds <= houses.Length * 2 + storeNumber)
                {
                    House targetHouse = getHouseByIndex(targetHouseIndex);
                    if (targetHouse.getSeeds() == 0)
                        return (true, currentHouse.getHouseName());
                }
            }
            return (false, 0);
        }

        private int getLowestIndexHouse()
        {
            House[] houses = player2.getHouses();
            for (int i = 0; i < houses.Length; i++)
            {
                House currentHouse = houses[i];
                if (currentHouse.getSeeds() > 0)
                    return currentHouse.getHouseName();
            }
            return 0;
        }


        private int getFinalIndex(Player currentPlayer,int currentHouseIndex, int seeds)
        {
            Store oppositeStore = getNextPlayer(currentPlayer).getStore();
            int finalIndex = currentHouseIndex;
            while (seeds > 0)
            {
                finalIndex = finalIndex + 1;
                if (finalIndex + 1 != oppositeStore.getIndex())
                    seeds = seeds - 1;
            }
            return finalIndex;
        }
        

        private void makeMove(int chosenHouseName)
        {
            House currentHouse = getHouseByName(currentPlayer, chosenHouseName);
            int seeds = currentHouse.getSeeds();
            int currentHouseIndex = currentHouse.getIndex();
            currentHouse.clearSeeds();
            while(seeds > 0)
            {
                int nextIndex = currentHouseIndex + 1;
                int formatNextIndex = normalizeToRange(nextIndex, 1, currentPlayer.getHouses().Length * 2 + storeNumber * 2);
                if (isOppositeStore(formatNextIndex))
                {
                    currentHouseIndex = formatNextIndex;
                    continue;
                }
                else
                {
                    moveSeeds(formatNextIndex, seeds);
                    seeds = seeds - 1;
                    currentHouseIndex = formatNextIndex;
                }
            }
        }

        private void moveSeeds(int targetHouseIndex,int seeds)
        {
            if (!isFinalSeeds(seeds))
            {
                if (isCurrentPlayerStore(targetHouseIndex))
                {
                    Store currentPlayerStore = currentPlayer.getStore();
                    currentPlayerStore.addSeeds(1);
                }
                else
                {
                    House targetHouse = getHouseByIndex(targetHouseIndex);
                    targetHouse.addSeeds(1);
                }
            }
            else
            {
                if (isCurrentPlayerStore(targetHouseIndex))
                {
                    additionalChance();
                    changePlayer = false;
                }
                else if (isCurrentPlayerHouse(targetHouseIndex))
                {
                    House targetHouse = getHouseByIndex(targetHouseIndex);
                    if (!targetHouseHasSeeds(targetHouse))
                    {
                        captureSeeds(targetHouseIndex);
                        changePlayer = true;
                    }
                    else
                    {
                        targetHouse.addSeeds(1);
                        changePlayer = true;
                    }
                }
                else
                {  
                    House targetHouse = getHouseByIndex(targetHouseIndex);
                    targetHouse.addSeeds(1);
                    changePlayer = true;
                }
            }  
        }

        private bool isFinalSeeds(int seeds)
        {
            if(seeds <= 1)
                return true;
            else
                return false;
        }

        private bool isCurrentPlayerStore(int targetHouseIndex)
        {
            Store currentPlayerStore = currentPlayer.getStore();
            int currentPlayerStoreIndex = currentPlayerStore.getIndex();
            if (targetHouseIndex == currentPlayerStoreIndex)
                return true;
            return false;
        }

        private bool isCurrentPlayerHouse(int targetIndexInGame)
        {
            House[] houses = currentPlayer.getHouses();
            for (int i = 0; i < houses.Length; i++)
            {
                House currentHouse = houses[i];
                if (currentHouse.getIndex() == targetIndexInGame)
                    return true;
            }
            return false;
        }

        private Player getCurrentPlayer()
        {
            return currentPlayer;
        }

         private Direction getDirection()
        {
            return direction;
        }

        private Player getNextPlayer(Player currentPlayer)
        {
            if (currentPlayer.Equals(player1))
                return player2;
            else
                return player1;
        }

        private bool isValidInput(string input)
        {
            if (input == null)
            {
                Console.WriteLine("Your input is not valid!");
                Console.WriteLine("Please enter Specify house number or 'q'.");
                return false;
            }
            input = input.Trim();
            if (input.Equals("q",StringComparison.OrdinalIgnoreCase))
                return true;
            else if (int.TryParse(input, out int number))
            {
                if (number >= 1 && number <= 6)
                    return true;
            }
            Console.WriteLine("Your input is not valid!");
            Console.WriteLine("Please enter Specify house number or 'q'.");
            return false;
        }

        private void quiteGame()
        {
            Console.WriteLine("You choose to quit the game.");
            Console.WriteLine("Bye!");
        }

        private bool currentPlayerHasRestSeeds(Player currentPlayer)
        {
            House[] houses = currentPlayer.getHouses();
            for (int i = 0; i < houses.Length; i ++)
            {
                House currentHouse = houses[i];
                if (currentHouse.getSeeds() > 0)
                    return true;
            }
            return false;
        }

        private bool hasSeedsInHouse(int chosenHouseName)
        {
            bool hasSeeds = true;
            Player currentPlayer = getCurrentPlayer();
            House[] houses = currentPlayer.getHouses();
            
            for (int i = 0; i < houses.Length; i++)
            {
                House currentHouse = houses[i];
                if (currentHouse.getHouseName() == chosenHouseName)
                {
                    int seeds = currentHouse.getSeeds();
                    if (seeds <= 0)
                        hasSeeds = false;
                }
            }
            return hasSeeds;
        }
        private House getHouseByName(Player currentPlayer, int houseName)
        {
            House chosenHouse = null;
            House[] houses = currentPlayer.getHouses();
            for (int i = 0; i < houses.Length; i++)
            {
                House currentHouse = houses[i];
                if (currentHouse.getHouseName() == houseName)
                    chosenHouse = currentHouse;
            }
            return chosenHouse;
        }
        private bool isOppositeStore(int nextIndex)
        {
            Player oppositePlayer = getNextPlayer(currentPlayer);
            Store oppositeStore = oppositePlayer.getStore();
            if (nextIndex == oppositeStore.getIndex())
                return true;
            return false;
        }
        private House getHouseByIndex(int houseIndex)
        {
            House targetHouse = null;
            House[] houses1 = currentPlayer.getHouses();
            House[] houses2 = getNextPlayer(currentPlayer).getHouses();
            for (int i = 0; i < houses1.Length; i++)
            {
                House currentHouse = houses1[i];
                if (houseIndex == currentHouse.getIndex())
                    targetHouse = currentHouse;
            }
            for (int i = 0; i < houses2.Length; i++)
            {
                House currentHouse = houses2[i];
                if (houseIndex == currentHouse.getIndex())
                    targetHouse = currentHouse;
            }
            return targetHouse;
        }

        private bool targetHouseHasSeeds(House targetHouse)
        {
            if(targetHouse.getSeeds() <= 0)
                return false;
            return true;
        }

        private int normalizeToRange(int input, int lowerBound, int upperBound)
        {
            int range = upperBound - lowerBound + 1;
            if (input < lowerBound)
                return upperBound - ((lowerBound - input - 1) % range);
            return (input - lowerBound) % range + lowerBound;
        }
        private void gameOver()
        {
            Console.WriteLine("You have no seeds in the house!");
            Player oppositePlayer = getNextPlayer(currentPlayer);
            House[] houses = oppositePlayer.getHouses();
            int seedsInOppositeHouses = 0;
            for (int i = 0; i < houses.Length; i++)
            {
                House currentHouse = houses[i];
                seedsInOppositeHouses = seedsInOppositeHouses + currentHouse.getSeeds();
                currentHouse.clearSeeds();
            }

            Store OppositeStore = oppositePlayer.getStore();
            int oppositePlayerTotalSeeds = OppositeStore.getSeeds() + seedsInOppositeHouses;
            OppositeStore.setSeeds(oppositePlayerTotalSeeds);
            
            int currentPlayerTotalSeeds = currentPlayer.getStore().getSeeds();
            dashboard.printGameBoard();
            Console.WriteLine("You have " + currentPlayerTotalSeeds + " seeds.");
            Console.WriteLine(getNextPlayer(currentPlayer).getPlayerName() + " has " + oppositePlayerTotalSeeds + " seeds.");
            if (currentPlayerTotalSeeds < oppositePlayerTotalSeeds)
            {
                Console.WriteLine("You lose!");
                Console.WriteLine(getNextPlayer(currentPlayer).getPlayerName() + " win!");
            }
            else
            {
                Console.WriteLine("You win!");
                Console.WriteLine(getNextPlayer(currentPlayer).getPlayerName() + " lose!");
            }
        }

        private void additionalChance()
        {
            Store currentPlayerStore = currentPlayer.getStore();
            currentPlayerStore.addSeeds(1);
            Console.WriteLine("You have another chance!");
        }

        private void captureSeeds(int targetHouseIndex)
        {
            House oppositeHouse = getOppositeHouse(targetHouseIndex);
            int oppositeSeeds = oppositeHouse.getSeeds();
            oppositeHouse.clearSeeds();
            House targetHouse = getHouseByIndex(targetHouseIndex);
            targetHouse.addSeeds(1 + oppositeSeeds);
        }

        private House getOppositeHouse(int targetHouseIndex)
        {
            int oppositeHouseIndex = getOppositeHouseIndex(targetHouseIndex);
            House oppositeHouse = getHouseByIndex(oppositeHouseIndex);
            return oppositeHouse;
        }

        private int getOppositeHouseIndex(int targetHouseIndex)
        {
            int totalHouseNumber = currentPlayer.getHouses().Length * 2;
            int totalStoreNumber = storeNumber * 2;
            return totalHouseNumber + totalStoreNumber - targetHouseIndex;
        }

    }
}