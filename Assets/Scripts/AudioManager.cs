using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool splatSwitch;
    private List<AudioClip> activeHitScale; // one array per instrument
    private List<AudioClip> activeLevelCompleteChords; // one per instr
    private List<AudioClip> backgroundMusic;
    // lists
    void Awake()
    {
        backgroundMusic = new List<AudioClip>();
        // load all sfx
    }
    

    void LoadSounds(string path, List<AudioClip> sounds)
    {
        // loads of load
    }

    void Update()
    {

    }

    public void PlaySplat()
    {
        if(splatSwitch)
        {
            // play 0
        }
        else
        {
            // play 1
        }

        splatSwitch = !splatSwitch;
    }
}
