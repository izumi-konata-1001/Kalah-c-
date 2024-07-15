namespace Kalah
{
    public class GameDashboard
    {
        Player player1;
        Player player2;
        Direction direction;
        public GameDashboard(Player player1, Player player2, Direction direction)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.direction = direction;
        }

        public void printGameBoard()
        {
            if(direction == Direction.Horizontal)
                horizontalLine();
            else if (direction == Direction.Vertical)
                verticalLine();
        }

        private void verticalLine()
        {
            Console.WriteLine("+---------------+");
            Console.Write("|       | P2");
            printVerticalStoreSeeds(player2.getStore());
            Console.WriteLine();
            Console.WriteLine("+---------------+");
            printVerticalHouses(player1, player2);
            Console.WriteLine("+---------------+");
            Console.Write("| P1");
            printVerticalStoreSeeds(player1.getStore());
            Console.WriteLine("       |");
            Console.WriteLine("+---------------+");
        }

        private void horizontalLine()
        {
            Console.WriteLine("+----+-------+-------+-------+-------+-------+-------+----+");
            horizontalPlayerDashboard(player2);
            Console.WriteLine("");
            Console.WriteLine("|    |-------+-------+-------+-------+-------+-------|    |");
            horizontalPlayerDashboard(player1);
            Console.WriteLine("");
            Console.WriteLine("+----+-------+-------+-------+-------+-------+-------+----+");
        }

        private void horizontalPlayerDashboard(Player player)
        {
            PlayerName playerName = player.getPlayerName();
            if (playerName.Equals(PlayerName.Player1))
            {
                Console.Write("|");
                Store store = player.getStore();
                printHorizontalStoreSeeds(store);
                House[] houses = player.getHouses();
                for (int i = 0; i < houses.Length; i++)
                    printHorizontalHouseSeeds(houses[i]);
                Console.Write(" P1 |");
            }
            else if(playerName.Equals(PlayerName.Player2) || playerName.Equals(PlayerName.Computer) )
            {
                Console.Write("| P2 ");
                Console.Write("|");
                House[] houses = player.getHouses();
                for (int i = houses.Length - 1; i >= 0; i--)
                    printHorizontalHouseSeeds(houses[i]);
                Store store = player.getStore();
                printHorizontalStoreSeeds(store);
            }
        }
        private void printHorizontalStoreSeeds(Store store)
        {
            int currentStoreSeeds = store.getSeeds();
            if (currentStoreSeeds < 10)
                Console.Write("  " + currentStoreSeeds + " |");
            else
                Console.Write(" " + currentStoreSeeds + " |");
        }

        private void printHorizontalHouseSeeds(House house)
        {
            int currentHouseSeeds = house.getSeeds();
            if (currentHouseSeeds < 10)
                Console.Write(" " + house.getHouseName() + "[ " + currentHouseSeeds + "] |");
            else
                Console.Write(" " + house.getHouseName() + "[" + currentHouseSeeds + "] |");
        }

        private void printVerticalStoreSeeds(Store store)
        {
            int seeds = store.getSeeds();
            if (seeds >= 10)
                Console.Write(" " + seeds + " |");
            else
                Console.Write("  " + seeds + " |");
        }

        private void printVerticalHouses(Player player1, Player player2)
        {
            House[] player1Houses = player1.getHouses();
            House[] player2Houses = player2.getHouses();
            for (int i = 0; i < player1.getHouses().Length; i++)
            {
                House currentHouse1 = player1Houses[i];
                House currentHouse2 = player2Houses[player2.getHouses().Length - i - 1];
                printVerticalHouseSeeds(currentHouse1, currentHouse2);
            }
        }
        
        private void printVerticalHouseSeeds(House house1, House house2)
        {
            Console.Write("| " + house1.getHouseName());
            if (house1.getSeeds() >= 10)
                Console.Write("[" + house1.getSeeds() + "]");
            else
                Console.Write("[ " + house1.getSeeds() + "]");
            Console.Write(" | " + house2.getHouseName());
            if (house2.getSeeds() >= 10)
                Console.Write("[" + house2.getSeeds() + "]");
            else
                Console.Write("[ " + house2.getSeeds() + "]"); 
            Console.WriteLine(" |");
        }
    }
}