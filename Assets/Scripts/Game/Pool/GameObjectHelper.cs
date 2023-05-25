using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game
{
    public static class GameObjectHelper
    {
        public static async Task<object> LoadObject(AssetReference reference)
        {
            var loadTask = reference.LoadAssetAsync<object>().Task;

            return await loadTask;
        }
    }
}