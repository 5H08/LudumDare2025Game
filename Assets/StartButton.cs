using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void StartGame(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int SceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progressValue;

            yield return null;
        }
    }
}
