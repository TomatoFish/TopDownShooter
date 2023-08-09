using System.Linq;
using Tools;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Settings/VFXLibrary")]
    public class VFXLibrary : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, GameObject> _library;

        public SerializableDictionary<string, GameObject> Library => _library;

        public GameObject Get(string key)
        {
            if (_library.All(pair => pair.Key != key))
            {
                Debug.LogError($"[VFXLibrary] Key {key} not found in vfx library.");
            }

            return _library.First(pair => pair.Key == key).Value;
        }
    }
}