using Microsoft.Win32.SafeHandles;

namespace Kalah
{
    public class Player
    {
        protected PlayerName playerName;
        protected int initialHouseNumber;
        protected int initialSeedsNumber;
        protected House[] houses;
        protected Store store;
        protected int storeNumber;
        protected Direction direction;
        public Player(PlayerName playerName, int initialHouseNumber, int initialSeedsNumber, int storeNumber, Direction direction)
        {
            this.playerName = playerName;
            this.initialHouseNumber = initialHouseNumber;
            houses = new House[initialHouseNumber];
            this.storeNumber = storeNumber;
            this.initialSeedsNumber = initialSeedsNumber;
            this. direction = direction;
        }

        public PlayerName getPlayerName()
        {
            return playerName;
        }

        public House[] getHouses()
        {
            return houses;
        }

        public Store getStore()
        {
            return store;
        }

        public bool isPlayerHouse(int houseIndex)
        {
            return false;
        }
    }
}