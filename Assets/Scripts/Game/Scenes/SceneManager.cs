using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Scenes
{
    public class SceneManager
    {
        private readonly List<Scene> _availableScenes;
        public string[] _levels;

        public SceneManager(params string[] levels)
        {
            _levels = levels;
            _availableScenes = new List<Scene>();
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                _availableScenes.Add(UnityEngine.SceneManagement.SceneManager.GetSceneAt(i));
            }
        }

        public async void LoadScene(string sceneName, Action sceneLoadedCallback = null)
        {
            var loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!loadOperation.isDone)
            {
                await Task.Yield();
            }
            var newScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            _availableScenes.Add(newScene);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(newScene);
            sceneLoadedCallback?.Invoke();
        }

        public async void UnloadScene(string sceneName, Action sceneUnloadedCallback = null)
        {
            var sceneNameLow = sceneName.ToLower();
            for (int i = 0; i < _availableScenes.Count; i++)
            {
                if (_availableScenes[i].name.ToLower().Equals(sceneNameLow))
                {
                    var unloadOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_availableScenes[i]);
                    while (!unloadOperation.isDone)
                    {
                        await Task.Yield();
                    }
                    _availableScenes.RemoveAt(i);
                    sceneUnloadedCallback?.Invoke();

                    break;
                }
            }
        }
    }
}
