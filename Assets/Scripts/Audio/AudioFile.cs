
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Custom/Audio/Clip File")]
public class AudioFile : ClipSource
{
    public AudioClip audioClip;
    public AudioMixerGroup mixer;
    public bool loop = false;
    public RangeWithRandom volume = new(0, 1, 1);
    public RangeWithRandom pitch = new(-3, 3, 1);
    [HideInInspector] public float length;


    public static ObjectPool<AudioSource> Pool => sfxPool;

    private static ObjectPool<AudioSource> sfxPool;
    private static bool isPoolInitialized = false;


    public static void ResetPool()
    {
        if (isPoolInitialized)
            sfxPool.Clear();
    }

    private static void InitializePool()
    {
        SceneManager.sceneUnloaded += (scene) => ResetPool();
        sfxPool = new ObjectPool<AudioSource>(
            () =>
            {
                var go = new GameObject("Sfx");
                return go.AddComponent<AudioSource>();
            },
            (source) =>
            {
                source.gameObject.SetActive(true);
            },
            (source) =>
            {
                source.Stop();
                source.gameObject.SetActive(false);
            },
            (source) =>
            {
                if (source != null)
                {
                    source.Stop();
                    Destroy(source.gameObject);
                }
            }
            ,false,30,100
            );
        isPoolInitialized = true;
    }

    private ObjectPool<AudioSource> SfxPool
    {
        get
        {
            if(!isPoolInitialized)
            {
                InitializePool();
            }
            return sfxPool;
        }
    }

    private AudioSource audioSource;
    private GameObject player;

    public override void Play(string groupName = "")
    {
        if (audioClip == null)
            return;

        audioSource = SfxPool.Get();
        player = audioSource.gameObject;

        audioSource.clip = audioClip;
        audioSource.volume = volume.Value;
        audioSource.pitch = pitch.Value;
        audioSource.loop = loop;
        if (mixer != null)
            audioSource.outputAudioMixerGroup = mixer;
        audioSource.Play();

        if (playingSfxes.ContainsKey(groupName))
            playingSfxes[groupName].Add(audioSource);
        else
            playingSfxes.Add(groupName, new List<AudioSource> { audioSource });

        if (loop && Mathf.Approximately(length, 0))
            return;
        DestroySourceAsync(groupName, audioSource, loop ? length : audioClip.length / audioSource.pitch);
    }
    public override void PlayAtPosition(string groupName = "", Vector3 position = default)
    {
        if (audioClip == null)
            return;


        audioSource = SfxPool.Get();
        player = audioSource.gameObject;

        audioSource.transform.position = position;
        audioSource.clip = audioClip;
        audioSource.volume = volume.Value;
        audioSource.pitch = pitch.Value;
        audioSource.loop = loop;

        if (mixer != null)
            audioSource.outputAudioMixerGroup = mixer;
        audioSource.Play();

        if (playingSfxes.ContainsKey(groupName))
            playingSfxes[groupName].Add(audioSource);
        else
            playingSfxes.Add(groupName, new List<AudioSource> { audioSource });

        if (loop && Mathf.Approximately(length, 0))
            return;
        DestroySourceAsync(groupName, audioSource, loop ? length : audioClip.length / audioSource.pitch);
    }


    private async void DestroySourceAsync(string groupName, AudioSource audioSource, float time)
    {
        await Task.Delay((int)(time * 1000));
        if (audioSource != null && audioSource.gameObject.activeSelf)
        {
            if (playingSfxes.ContainsKey(groupName))
            {
                if (playingSfxes[groupName].Contains(audioSource))
                    playingSfxes[groupName].Remove(audioSource);
            }
            SfxPool.Release(audioSource);
        }
    }
    public override void TestCleanUp()
    {
        if (player != null)
            DestroyImmediate(player);
    }

    public override void TestPlay()
    {
        if (audioClip == null)
            return;
        if (player == null)

        {
            player = new GameObject("Sfx")
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            audioSource = player.AddComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        audioSource.volume = volume.Value;
        audioSource.pitch = pitch.Value;
        audioSource.loop = loop;
        if (mixer != null)
            audioSource.outputAudioMixerGroup = mixer;
        audioSource.Play();
    }

    public override void TestStop()
    {
        if (audioSource != null)
            audioSource.Stop();
    }


}