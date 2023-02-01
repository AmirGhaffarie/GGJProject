using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeWithRandom))]
public class RangeWithRandomPropertyDrawer : PropertyDrawer
{
    SerializedProperty activeProperty;
    void Change()
    {
        var random = activeProperty.FindPropertyRelative("random");
        random.boolValue = !random.boolValue;
        random.serializedObject.ApplyModifiedProperties();
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var maximum = property.FindPropertyRelative("Maximum");
        var minimum = property.FindPropertyRelative("Minimum");
        var min = property.FindPropertyRelative("min");
        var max = property.FindPropertyRelative("max");
        var random = property.FindPropertyRelative("random");
        if (maximum.hasMultipleDifferentValues || minimum.hasMultipleDifferentValues || min.hasMultipleDifferentValues || max.hasMultipleDifferentValues || random.hasMultipleDifferentValues)
            return;
        Event e = Event.current;
        var rect = position;
        rect.width = EditorGUIUtility.labelWidth;
        if (e.type == EventType.MouseDown && e.button == 1 && rect.Contains(e.mousePosition))
        {
            activeProperty = property;
            GenericMenu context = new GenericMenu();
            context.AddItem(new GUIContent(random.boolValue ? "To Single Value" : "To Range"), false, Change);
            context.ShowAsContext();
        }
        if (random.boolValue)
        {
            GUIContent content = new GUIContent();
            float mi = min.floatValue;
            float ma = max.floatValue;
            content.text = label.text + "(" + mi.ToString("0.00") + "-" + ma.ToString("0.00") + ")";
            EditorGUI.MinMaxSlider(position, content, ref mi, ref ma, minimum.floatValue, maximum.floatValue);
            min.floatValue = Mathf.Clamp(mi, minimum.floatValue, max.floatValue);
            max.floatValue = Mathf.Clamp(ma, min.floatValue, maximum.floatValue);
        }
        else
        {
            GUIContent content = new GUIContent();
            content.text = label.text + "(" + min.floatValue.ToString("0.00") + ")";
            min.floatValue = EditorGUI.Slider(position, content, min.floatValue, minimum.floatValue, maximum.floatValue);
            min.floatValue = Mathf.Clamp(min.floatValue, minimum.floatValue, maximum.floatValue);
            max.floatValue = min.floatValue;
        }
        property.serializedObject.ApplyModifiedProperties();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
