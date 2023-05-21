using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Scenes
{
    public class SceneManager
    {
        private List<Scene> _currentScenes;
        public string[] _levels;

        public SceneManager(params string[] levels)
        {
            _levels = levels;
            _currentScenes = new List<Scene>();
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                _currentScenes.Add(UnityEngine.SceneManagement.SceneManager.GetSceneAt(i));
            }
        }

        public async void LoadScene(string sceneName, Action sceneLaodedCallback = null)
        {
            var loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!loadOperation.isDone)
            {
                await Task.Yield();
            }
            var newScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            _currentScenes.Add(newScene);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(newScene);
            sceneLaodedCallback?.Invoke();
        }

        public async void UnloadScene(string sceneName, Action sceneUnlaodedCallback = null)
        {
            var sceneNameLow = sceneName.ToLower();
            for (int i = 0; i < _currentScenes.Count; i++)
            {
                if (_currentScenes[i].name.ToLower().Equals(sceneNameLow))
                {
                    var unloadOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_currentScenes[i]);
                    while (!unloadOperation.isDone)
                    {
                        await Task.Yield();
                    }
                    _currentScenes.RemoveAt(i);
                    sceneUnlaodedCallback?.Invoke();

                    break;
                }
            }
        }
    }
}
