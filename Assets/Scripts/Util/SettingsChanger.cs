using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Settings Changer")]
public class SettingsChanger : ScriptableObject
{
    public void ChangeTimeScale(float newScale)
    {
        Time.timeScale = newScale;
    }
}
