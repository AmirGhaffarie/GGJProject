using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CanvasGroupFade : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] UnityEvent doAfter;
    private void OnEnable()
    {
        StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        float t = animationCurve.keys.First().time;
        float lastFrameTime = animationCurve.keys.Last().time;
        while (t < lastFrameTime)
        {
            canvasGroup.alpha = animationCurve.Evaluate(t);
            t += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = animationCurve.keys.Last().value;
        doAfter.Invoke();
    }
}
