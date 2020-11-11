using System.Collections;
using UnityEngine;
namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha<1f) // alpa is not 1
            {
                canvasGroup.alpha += Time.deltaTime / time;
                //moving alpha toward 1
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
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