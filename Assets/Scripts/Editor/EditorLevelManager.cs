using UnityEditor;
using UnityEngine.SceneManagement;

namespace Game.Editor
{
    [InitializeOnLoad]
    public class EditorLevelManager
    {
        static EditorLevelManager()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange mode)
        {
            if (mode == PlayModeStateChange.EnteredPlayMode)
            {
                if ( !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(0)) )
                    SceneManager.LoadScene(0);
            }
        }
    }
}
