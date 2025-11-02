using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image image;

    public void FadeToBlack()
    {
        StartCoroutine(RunFade());
    }

    IEnumerator RunFade()
    {
        float current = 0.0f;

        // Cache the color to modify it
        Color color = image.color;
        SoundManager.instance.StopWitchSound();

        while (current < 4.0f)
        {
            current += Time.deltaTime;
            color.a += Time.deltaTime / 4.0f; // Adjust alpha over time
            image.color = color; // Reassign the modified color
            yield return null;
        }
        SceneManager.LoadScene(0);
    }
}
