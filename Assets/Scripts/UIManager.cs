using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField] private Slider loading;

    /// <summary>
    /// Exit the game
    /// </summary>
    public void QuitTheGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Load the MainMenu Scene
    /// </summary>
    public void BackToMainMenu()
    {
        StartCoroutine(LoadAsynchronously(0));
    }

    /// <summary>
    /// Restart the current Scene
    /// </summary>
    public void RestartLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    /// <summary>
    /// Moving from scene to another
    /// </summary>
    /// <param name="scencIndex">index of the scene you will go to</param>
    public void LoadLevel(int scencIndex)
    {
        StartCoroutine(LoadAsynchronously(scencIndex));
    }

    private IEnumerator LoadAsynchronously(int sceneNum)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNum);
        while (!operation.isDone)
        {
            loading.value = operation.progress;

            yield return null;
        }
    }
}
