using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Level";
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text progressText; // optional

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // progress is 0–0.9 while loading
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            if (progressText)
                progressText.text = (progress * 100f).ToString("F0") + "%";

            // When load reaches 90%, Unity waits for allowSceneActivation = true
            if (operation.progress >= 0.9f)
            {
                // Small delay or animation if you want
                yield return new WaitForSeconds(0.5f);

                // Now allow activation
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
