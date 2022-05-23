// Author:  Joseph Crump
// Date:    05/22/22

using UnityEngine;
using UnityEngine.SceneManagement;

namespace JC.Prototyping
{
    /// <summary>
    /// Asset to help facilitate Scene navigation.
    /// </summary>
    [CreateAssetMenu(fileName = "SceneLoader", menuName = "Utility/Scene Loader")]
    public class SceneLoaderAsset : ScriptableObject
    {
        public Scene ActiveScene => SceneManager.GetActiveScene();

        /// <summary>
        /// Load a scene by its build index.
        /// </summary>
        public void LoadScene(int buildIndex)
        {
            try
            {
                SceneManager.LoadScene(buildIndex);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        /// <summary>
        /// Load a scene by its name.
        /// </summary>
        public void LoadScene(string sceneName)
        {
            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        /// <summary>
        /// Load the next scene in the build index.
        /// </summary>
        public void LoadNextScene()
        {
            LoadScene(ActiveScene.buildIndex + 1);
        }

        /// <summary>
        /// Load the previous scene in the build index.
        /// </summary>
        public void LoadPreviousScene()
        {
            LoadScene(ActiveScene.buildIndex - 1);
        }

        /// <summary>
        /// Load the first scene in the build indices.
        /// </summary>
        public void LoadFirstScene()
        {
            LoadScene(0);
        }

        /// <summary>
        /// Load the last scene in the build indices.
        /// </summary>
        public void LoadFinalScene()
        {
            LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
    }
}
