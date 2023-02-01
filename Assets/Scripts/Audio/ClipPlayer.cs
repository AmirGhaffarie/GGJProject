using UnityEngine;

public class ClipPlayer : MonoBehaviour
{
    public void PlayClip(ClipSource source)
    {
        source.Play();
    }
}
