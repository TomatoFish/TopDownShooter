using System;
using System.Threading.Tasks;
using Logic.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Settings
{
    [Serializable]
    public class UnitObject : IUnitObject
    {
        [SerializeField] private AssetReference _reference;

        private Task<object> _loadTask;

        public async Task<object> LoadObject()
        {
            _loadTask ??= _reference.LoadAssetAsync<object>().Task;

            return await _loadTask;
        }

        public async Task<GameObject> LoadGameObject()
        {
            await LoadObject();
            return (GameObject)_loadTask.Result;
        }
    }
}