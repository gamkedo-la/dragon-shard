using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// You need an AudioPlayer in order to play music.
[RequireComponent(typeof(AudioPlayer))]
public class MusicManager : MonoBehaviour
{
    [HideInInspector]
    public static MusicManager instance;

    [SerializeField]
    public List<AudioEvent> musicTracks = new List<AudioEvent>();
    [SerializeField] AudioPlayer audioPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        audioPlayer.PlayAudioEvent(musicTracks[0]);
    }


}
