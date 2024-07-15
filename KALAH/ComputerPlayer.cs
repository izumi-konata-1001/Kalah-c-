namespace Kalah
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(int initialHouseNumber, int initialSeeds,int storeNumber,Direction direction) : base(PlayerName.Computer, initialHouseNumber, initialSeeds, storeNumber, direction)
        {
            initialHouses();
            initialStore();
        }
        private void initialHouses()
        {
            for (int i = 0; i < initialHouseNumber; i++)
                houses[i] = new House(initialSeedsNumber, i + 1 + initialHouseNumber + storeNumber, i + 1, playerName);
        }

        private void initialStore()
        {
            if (direction.Equals(Direction.Horizontal))
                store = new Store(initialHouseNumber + storeNumber, playerName);
            else if (direction.Equals(Direction.Vertical))
                store = new Store((initialHouseNumber + storeNumber) * 2, playerName);
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