namespace EventMessages
{
    public struct BlockDestroyed
    {
        public int AmountPoints;

        public BlockDestroyed(int amountPoints)
        {
            AmountPoints = amountPoints;
        }
    }
}