using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "Dragon Shard/AudioEvent", fileName = "New AudioEvent.asset")]
public class AudioEvent : ScriptableObject
{
    [SerializeField] string eventName;
    [SerializeField] AudioClip[] audioClips;

    public AudioClip GetClip()
    {
        return audioClips[Random.Range(0, audioClips.Length - 1)];
    }
}
