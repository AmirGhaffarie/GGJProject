using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class PostProcessAnimate : MonoBehaviour
{
    Volume volume;
    [SerializeField] AnimationCurve[] curves;
    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    public void AnimateVolume(int index)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateVolumeRoutine(index));
    }

    public IEnumerator AnimateVolumeRoutine(int index)
    {
        var curve = curves[index];
        float t = curve.keys.First().time;
        float maxTime = curve.keys.Last().time;
        while (t < maxTime)
        {
            volume.weight = curve.Evaluate(t);
            t += Time.deltaTime;
            yield return null;
        }
        volume.weight = curve.keys.Last().value;
    }
    public void AnimateVolumeUnscaled(int index)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateVolumeRoutineUnscaled(index));
    }

    public IEnumerator AnimateVolumeRoutineUnscaled(int index)
    {
        var curve = curves[index];
        float t = curve.keys.First().time;
        float maxTime = curve.keys.Last().time;
        while (t < maxTime)
        {
            volume.weight = curve.Evaluate(t);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        volume.weight = curve.keys.Last().value;
    }

}
