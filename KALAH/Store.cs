namespace Kalah
{
    public class Store
    {
        int index;
        int seeds;
        PlayerName owner;

        public Store(int index, PlayerName owner)
        {
            this.index = index;
            this.owner = owner;
            this.seeds = 0;
        }
        
        public void addSeeds(int seedsNumberToAdd)
        {
            seeds = seeds + seedsNumberToAdd;
        }

        public void clearSeeds()
        {
            seeds = 0;
        }

        public int getSeeds()
        {
            return seeds;
        }
        public PlayerName getPlayerName()
        {
            return owner;
        }

        public int getIndex()
        {
            return index;
        }

        public void setSeeds(int seeds)
        {
            this.seeds = seeds;
        }
    }
}