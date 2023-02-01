using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPFontSwitcher : MonoBehaviour
{
    TextMeshProUGUI tmp;
    [SerializeField] TMP_FontAsset[] fonts;
    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeFont(int index)
    {
        tmp.font = fonts[index];
        tmp.UpdateFontAsset();
    }
}
