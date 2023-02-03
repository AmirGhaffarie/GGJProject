using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateCode : EditorWindow
{

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Create Code")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CreateCode window = (CreateCode)GetWindow(typeof(CreateCode));
        window.Show();
    }
    string stateName;
    string conditionName;
    void OnGUI()
    {
        stateName = EditorGUILayout.TextField(new GUIContent("State Name"), stateName);

        if (GUILayout.Button("Create State"))
        {
            CreateState();
        }

        conditionName = EditorGUILayout.TextField(new GUIContent("Condition Name"), conditionName);

        if (GUILayout.Button("Create Condition"))
        {
            CreateCondition();
        }
    }

    private void CreateCondition()
    {
        var path = Path.Combine("Assets/Scripts/State Machines/Conditions", $"{conditionName}.cs");

        var Text = $"using UnityEngine;\r\n\r\n[CreateAssetMenu(menuName = \"State Machines/Condition/{conditionName}\")]\r\npublic class {conditionName} : TransitionCondition\r\n{{\r\n    public override bool Check(CharacterControl characterControl)\r\n    {{\r\n        return false;\r\n    }}\r\n}}";

        File.WriteAllText(path, Text);
        AssetDatabase.ImportAsset(path);
        AssetDatabase.Refresh();
    }

    private void CreateState()
    {
        var path = Path.Combine("Assets/Scripts/State Machines/States", $"{stateName}.cs");

        var Text = $"using UnityEngine;\r\n\r\n[CreateAssetMenu(menuName = \"State Machines/State/{stateName}\")]\r\npublic class {stateName} : State\r\n{{\r\n}}";

        File.WriteAllText(path, Text);
        AssetDatabase.ImportAsset(path);
        AssetDatabase.Refresh();
    }
}
