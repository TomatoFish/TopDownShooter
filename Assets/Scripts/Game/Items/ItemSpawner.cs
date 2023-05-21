using UnityEngine;

namespace Game.Level
{
    public class ItemSpawner
    {
        //private readonly ItemViewFactory itemFactory;
        private readonly ItemFactory itemFactory;

        //public ItemSpawner(ItemViewFactory itemFactory)
        //{
        //    this.itemFactory = itemFactory;
        //}

        public ItemSpawner(ItemFactory itemFactory)
        {
            this.itemFactory = itemFactory;
        }

        //public virtual Item SpawnItem(int id, Transform spawnTransform)
        //{
        //    var itemView = itemFactory.Create(id, spawnTransform);
        //    var item = new Item(itemView, null);
        //    return item;
        //}

        public virtual Item SpawnItem(int id, Transform spawnTransform)
        {
            var item = itemFactory.Create(id, spawnTransform);
            return item;
        }
    }
}