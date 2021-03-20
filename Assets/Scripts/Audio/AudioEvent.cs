using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "Dragon Shard/AudioEvent", fileName = "New AudioEvent.asset")]
public class AudioEvent : ScriptableObject
{
    [SerializeField] string eventName;
    [SerializeField] AudioClip[] audioClips;

    [Header("Audio Event Config")]
    [SerializeField] bool Loop = false;

    [Range(-80, 0.0001f)]
    [SerializeField] float volume;

    public AudioClip GetClip()
    {
        return audioClips[Random.Range(0, audioClips.Length - 1)];
    }

    public bool isLooping() { return Loop; }
    public float getVolume() { return AudioFunctions.DbToLinear(volume); }
}
