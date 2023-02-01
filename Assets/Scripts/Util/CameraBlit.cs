using UnityEngine;

[ExecuteAlways]
public class CameraBlit : MonoBehaviour
{
    [SerializeField] MaterialAnimator blurMat;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (blurMat)
            Graphics.Blit(source, destination, blurMat.Material);
    }
}
