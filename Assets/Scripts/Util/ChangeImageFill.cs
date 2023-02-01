using UnityEngine;
using UnityEngine.UI;

public class ChangeImageFill : MonoBehaviour
{
    [SerializeField] FloatVariable percent;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        percent.onChange += OnPercentChange;
        OnPercentChange(percent.Value);
    }

    private void OnPercentChange(float p)
    {
        image.fillAmount = percent.Value;
    }
}
