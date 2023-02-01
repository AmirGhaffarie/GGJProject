using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Audio/Mix Clip")]
public class AudioMix : ClipSource
{
    public List<ClipSource> clips;
    public override void Play(string groupName = "")
    {
        foreach (var clip in clips)
        {
            clip.Play(groupName);
        }
    }

    public override void PlayAtPosition(string groupName = "", Vector3 position = default)
    {
        foreach (var clip in clips)
        {
            clip.PlayAtPosition(groupName, position);
        }
    }

    public override void TestCleanUp()
    {
        if (HasInfiniteLoop())
            return;
        foreach (var clip in clips)
        {
            clip?.TestCleanUp();
        }
    }

    public override void TestPlay()
    {
        if (HasInfiniteLoop())
            throw new System.Exception("Infinite Loop");
        foreach (var clip in clips)
        {
            clip?.TestPlay();
        }
    }

    public override bool HasInfiniteLoop()
    {
        List<ClipSource> thisLayer = new List<ClipSource>();
        List<ClipSource> lastLayer = new List<ClipSource>();
        foreach (var clip in clips)
        {
            if (!(clip is AudioFile))
            {
                thisLayer.Add(clip);
            }
        }
        while (thisLayer.Count > 0)
        {
            if (thisLayer.Contains(this))
                return true;
            lastLayer.Clear();
            lastLayer.AddRange(thisLayer);
            thisLayer.Clear();
            foreach (var item in lastLayer)
            {
                if (item is AudioMix)
                    thisLayer.AddRange((item as AudioMix).clips.Where((c) => !(c is AudioFile)));
                if (item is AudioRandom)
                    thisLayer.AddRange((item as AudioRandom).clips.Select((cl) => cl.clipSource).Where((c) => !(c is AudioFile)));
            }

        }
        return false;
    }

    public override void TestStop()
    {
        if (HasInfiniteLoop())
            throw new System.Exception("Infinite Loop");
        foreach (var clip in clips)
        {
            clip?.TestStop();
        }
    }
}
