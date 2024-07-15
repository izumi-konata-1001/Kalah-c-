namespace Kalah
{
    public class HumanPlayer : Player
    {

        public HumanPlayer(PlayerName playerName,int initialHouseNumber, int initialSeedsNumber, int storeNumber, Direction direction) : base(playerName,initialHouseNumber,initialSeedsNumber, storeNumber, direction)
        {
            initialHouses();
            initialStore();
        }
        private void initialHouses()
        {
            if (playerName.Equals(PlayerName.Player1))
            {
                for (int i = 0; i < initialHouseNumber; i++)
                    houses[i] = new House(initialSeedsNumber, i + 1, i + 1,  playerName);
            }
            else
            {
                for (int i = 0; i < initialHouseNumber; i++)
                    houses[i] = new House(initialSeedsNumber, i + 1 + initialHouseNumber + storeNumber, i + 1, playerName);
            }
        }
        private void initialStore()
        {
            if (playerName.Equals(PlayerName.Player1))
            {
                if (direction.Equals(Direction.Horizontal))
                    store = new Store((initialHouseNumber + storeNumber) * 2, playerName);
                else if (direction.Equals(Direction.Vertical))
                    store = new Store(initialHouseNumber + storeNumber, playerName);
            }
            else
            {
                if (direction.Equals(Direction.Horizontal))
                    store = new Store(initialHouseNumber + storeNumber, playerName);
                else if (direction.Equals(Direction.Vertical))
                    store = new Store((initialHouseNumber + storeNumber) * 2, playerName);
            }
        }

        public bool isPlayerHouse(int houseIndex)
        {
            for (int i = 0; i < houses.Length; i ++)
            {
                House currentHouse = houses[i];
                if (currentHouse.getIndex() == houseIndex)
                    return true;
            }
            return false;
        }
    }
}