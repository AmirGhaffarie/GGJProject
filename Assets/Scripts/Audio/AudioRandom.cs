using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Audio/Random Clip")]
public class AudioRandom : ClipSource
{
    public List<ClipWithChance> clips;
    public override void Play(string groupName = "")
    {
        var chance = Random.value;
        float currentChance = 0;
        foreach (var clip in clips)
        {
            currentChance += clip.chance;
            if (chance <= currentChance)
            {
                clip.clipSource.Play(groupName);
                return;
            }
        }
    }

    public override void PlayAtPosition(string groupName = "", Vector3 position = default)
    {
        var chance = Random.value;
        float currentChance = 0;
        foreach (var clip in clips)
        {
            currentChance += clip.chance;
            if (chance <= currentChance)
                clip.clipSource.PlayAtPosition(groupName, position);
        }
    }

    public override void TestCleanUp()
    {
        if (HasInfiniteLoop())
            return;
        foreach (var clip in clips)
        {
            clip.clipSource?.TestCleanUp();
        }
    }

    public override void TestPlay()
    {
        if (HasInfiniteLoop())
            throw new System.Exception("Infinite Loop");
        TestStop();
        var chance = Random.value;
        float currentChance = 0;
        foreach (var clip in clips)
        {
            currentChance += clip.chance;
            if (chance <= currentChance)
            {
                clip.clipSource?.TestPlay();
                return;
            }
        }
    }


    public override bool HasInfiniteLoop()
    {
        List<ClipSource> thisLayer = new List<ClipSource>();
        List<ClipSource> lastLayer = new List<ClipSource>();
        foreach (var clip in clips.Select((c) => c.clipSource))
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
            clip.clipSource?.TestStop();
        }
    }
}
