using TMPro;
using UnityEngine;

public class SetTextFrom : MonoBehaviour
{
    [SerializeField] IntegerVariable var;

    private TextMeshProUGUI tmpro;

    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        var.onChange += OnChange;
        OnChange(var.Value);
    }

    private void OnChange(int percent)
    {
        tmpro.text = var.Value.ToString();
    }
}
