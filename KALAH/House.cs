namespace Kalah
{
    public class House
    {
        int index;
        int houseName;
        int seeds;
        PlayerName owner;
        public House(int seeds, int index, int houseName, PlayerName owner)
        {
            this.seeds = seeds;
            this.index = index;
            this.houseName = houseName;
            this.owner = owner;
        }

        public void clearSeeds()
        {
            seeds = 0;
        }

        public int getSeeds()
        {
            return seeds;
        }

        public void addSeeds(int addSeedsNumber)
        {
            seeds = seeds + addSeedsNumber;
        }

        public PlayerName getPlayerName()
        {
            return owner;
        }

        public int getIndex()
        {
            return index;
        }
        public int getHouseName()
        {
            return houseName;
        }
    }
}