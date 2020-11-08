using System.Collections;
using UnityEngine;
namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOutIn(3f));
        }
        IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha<1f) // alpa is not 1
            {
                canvasGroup.alpha += Time.deltaTime / time;
                //moving alpha toward 1
                yield return null;
            }
        }
        IEnumerator FadeOutIn(float time)
        {
            yield return FadeOut(time);
            Debug.Log("Fade out");
            yield return FadeIn(time);
            Debug.Log("Fade in");
        }
        IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0f) // alpa is not 1
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                //moving alpha toward 1
                yield return null;
            }
        }
    }
}