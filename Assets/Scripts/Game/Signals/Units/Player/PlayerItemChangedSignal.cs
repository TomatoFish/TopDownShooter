namespace Game.Level
{
    public class PlayerItemChangedSignal
    {
        public readonly Item PrevItem;
        public readonly Item NewItem;

        public PlayerItemChangedSignal(Item prevItem, Item newItem)
        {
            PrevItem = prevItem;
            NewItem = newItem;
        }
    }
}