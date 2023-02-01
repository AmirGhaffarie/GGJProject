using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClipSource), true)]
[CanEditMultipleObjects]
public class ClipSourceEditor : Editor
{
    private ClipSource clip;
    private bool showError;
    private void OnEnable()
    {
        clip = (ClipSource)target;
        showError = clip.HasInfiniteLoop();
    }
    private void OnDisable()
    {
        clip.TestCleanUp();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed)
            showError = clip.HasInfiniteLoop();
        if (clip is AudioFile)
        {
            var audioFile = (AudioFile)clip;
            if (audioFile.loop)
                audioFile.length = EditorGUILayout.FloatField("Length", audioFile.length);
        }
        if (showError)
            EditorGUILayout.HelpBox("Infinite Loop Error", MessageType.Error);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Play"))
            Play();
        if (GUILayout.Button("Stop"))
            Stop();
        GUILayout.EndHorizontal();
    }

    private void Stop()
    {
        clip.TestStop();
    }
    private void Play()
    {
        clip.TestPlay();
    }
}
