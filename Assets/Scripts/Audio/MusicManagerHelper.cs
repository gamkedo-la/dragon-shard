using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManagerHelper : MonoBehaviour
{
    [SerializeField] AudioMixerSnapshot defaultSnapshot;
    [SerializeField] AudioMixerSnapshot musicOffSnapshot;
    [SerializeField] KeyCode defaultHotKey;
    [SerializeField] KeyCode musicOffHotKey;

    void Update()
    {
        if (Input.GetKeyDown(defaultHotKey))
            defaultSnapshot.TransitionTo(0.1f);

        if (Input.GetKeyDown(musicOffHotKey))
            musicOffSnapshot.TransitionTo(0.1f);
    }
}
