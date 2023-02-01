using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MaterialAnimator : MonoBehaviour
{
    [SerializeField] private Material baseMaterial;
    [Space]
    [SerializeField] private MaterialAnimation[] animations;
    [Header("Callbacks")]
    [SerializeField] private UnityEvent doBefore;
    [SerializeField] private UnityEvent doAfter;

    private Material material;
    public Material Material
    {
        get
        {
            if (Application.isPlaying)
                return material;
            return baseMaterial;
        }
    }

    private void Awake()
    {
        material = Instantiate(baseMaterial);
    }
    public void StartAnimatingMaterial(int index, bool unscaled = false, Action onFinish = null)
    {
        StartCoroutine(Animate(index, unscaled, onFinish));
    }
    public void StartAnimatingMaterial(int index = 0)
    {
        StartCoroutine(Animate(index));
    }

    public IEnumerator Animate(int index, bool unscaled = false, Action onFinish = null)
    {
        doBefore.Invoke();

        var properties = animations[index].properties;

        var t = properties.Select(p => p.Animation.keys.First().time).Min();
        var max = properties.Select(p => p.Animation.keys.Last().time).Max();
        while (t < max)
        {
            properties.ForEach(p => Material.SetFloat(p.Name, p.Animation.Evaluate(t)));
            t += unscaled ? Time.unscaledDeltaTime : Time.deltaTime;
            yield return null;
        }
        properties.ForEach(p => Material.SetFloat(p.Name, p.Animation.keys.Last().value));
        doAfter.Invoke();
        onFinish?.Invoke();
    }

    [Serializable]
    private struct MaterialProperty
    {
        public string Name;
        public AnimationCurve Animation;
    }
    [Serializable]
    private struct MaterialAnimation
    {
        public List<MaterialProperty> properties;
    }
}
