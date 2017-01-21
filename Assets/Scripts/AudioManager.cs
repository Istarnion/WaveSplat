using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool splatSwitch;
    private AudioClip splatOne, splatTwo;
    private int hitIndex;
    private List<AudioClip> activeHitScale; // one array per instrument
    private List<AudioClip> activeLevelCompleteChords; // one per instr
    private List<AudioClip> backgroundMusic;

    // lists
    void Awake()
    {
        splatOne = (AudioClip) Resources.Load("Audio/splat_01");
        splatTwo = (AudioClip) Resources.Load("Audio/splat_02");
        backgroundMusic = new List<AudioClip>();
        hitIndex = 0;

        activeHitScale = new List<AudioClip>();
        activeLevelCompleteChords = new List<AudioClip>();

        LoadSounds("Audio/HitScale/Piano", activeHitScale);
        LoadSounds("Audio/LevelComplete/Piano", activeLevelCompleteChords);
    }
    

    void LoadSounds(string path, List<AudioClip> clips)
    {
        foreach (Object clip in Resources.LoadAll(path)) clips.Add((AudioClip)clip);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioSource.PlayClipAtPoint(splatOne, Vector3.zero);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioSource.PlayClipAtPoint(splatTwo, Vector3.zero);
        }
    }

    public void PlaySplat()
    {
        if(splatSwitch)
        {
            AudioSource.PlayClipAtPoint(splatOne, Vector3.zero);
        }
        else
        {
            AudioSource.PlayClipAtPoint(splatTwo, Vector3.zero);
        }

        splatSwitch = !splatSwitch;
    }
}
