using System.Collections.Generic;
using UnityEngine;

public abstract class ClipSource : ScriptableObject
{
    protected static Dictionary<string, List<AudioSource>> playingSfxes = new Dictionary<string, List<AudioSource>>();

    public abstract void Play(string groupName = "");
    public abstract void PlayAtPosition(string groupName = "", Vector3 position = default);

    public void Stop(string groupName)
    {
        if (playingSfxes.ContainsKey(groupName))
        {
            playingSfxes[groupName].ForEach(s =>
            {
                if (s != null && s.gameObject.activeSelf)
                {
                    s.Stop();
                    AudioFile.Pool.Release(s);
                }
            });
            playingSfxes[groupName].Clear();
        }
    }

    public abstract void TestPlay();
    public abstract void TestStop();

    public abstract void TestCleanUp();
    public virtual bool HasInfiniteLoop() { return false; }
}
