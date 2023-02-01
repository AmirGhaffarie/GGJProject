using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ChangeScaleOverTime : MonoBehaviour
{
    [SerializeField] Vector3 startingScale;
    [SerializeField] Vector3 endingScale;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] bool ScaledTime = true;

    public void Animate(Action doAfter = null)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateRoutine(doAfter));
    }

    private IEnumerator AnimateRoutine(Action doAfter = null)
    {
        var maxTime = animationCurve.keys.Last().time;
        var time = animationCurve.keys.First().time;
        while (time < maxTime)
        {
            time += ScaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(startingScale, endingScale, animationCurve.Evaluate(time));
            yield return null;
        }
        transform.localScale = Vector3.Lerp(startingScale, endingScale, animationCurve.keys.Last().value);
        doAfter?.Invoke();
    }
}
