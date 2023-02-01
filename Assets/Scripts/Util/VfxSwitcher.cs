using UnityEngine;
using UnityEngine.VFX;

public class VfxSwitcher : MonoBehaviour
{
    [SerializeField] VisualEffect vfx;
    [SerializeField] bool spawnAtStart = false;
    private void Awake()
    {
        if (!spawnAtStart)
            vfx.Stop();
    }
    public void StopSpawningParticles()
    {
        vfx.Stop();
    }
    public void StartSpawningParticles()
    {
        vfx.Play();
    }
}
