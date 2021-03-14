using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] List<AudioEvent> audioEvents = new List<AudioEvent>();
    [SerializeField] List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] int initialSources = 1;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioMixerGroup mixGroup;
    [SerializeField] [Range(0, 1)] float spatialBlend = 0;
    internal int playIndex = 0;

    [SerializeField] Unit unitInfo;

    void Start()
    {
        if (audioSources.Count < initialSources)
        {
            for (int i = audioSources.Count; i < initialSources; ++i)
            {
                AddAudioSource();
            }
        }

        foreach (AudioSource s in audioSources)
        {
            s.outputAudioMixerGroup = mixGroup;
            s.spatialBlend = spatialBlend;
        }

        if (unitInfo == null)
            unitInfo = GetComponent<Unit>();
    }

    private void AddAudioSource()
    {
        var audioS = gameObject.AddComponent<AudioSource>();
        audioSources.Add(audioS);
    }

    public void PlayAudioEvent(int eventIndex)
    {
        if (eventIndex < audioEvents.Count)
        {
            var clip = audioEvents[eventIndex].GetClip();
            var source = GetNextSource();

            source.clip = clip;
            source.Play();
        }
        else
        {
            Debug.LogError("Event Index out of range.  You called index: " + eventIndex + " from: " + gameObject.name);
        }
    }

    public void PlayAudioEvent(AudioEvent audioEvent)
    {
        if (audioEvent != null)
        {
            var clip = audioEvent.GetClip();
            var source = GetNextSource();
            source.clip = clip;
            source.Play();
        }
        else
            Debug.LogError("Event undefined.  Check: " + gameObject.name);
    }

    private AudioSource GetNextSource()
    {
        playIndex = (playIndex + 1) % audioSources.Count;

        if (!audioSources[playIndex].isPlaying)
        {
            return audioSources[playIndex];
        }
        else
        {
            AddAudioSource();
            GetNextSource();
        }

        return audioSources[playIndex];
    }
}
