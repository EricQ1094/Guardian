using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private static SoundPlayer thisInstance;

    public static SoundPlayer ThisSoundPlayer
    {
        get
        {
            return thisInstance;
        }
    }

    private AudioSource thisAudioSource = null;
    [SerializeField] private AudioClip thisCoinSound = null;
    // Start is called before the first frame update
    void Start()
    {
        thisInstance = this;

        thisAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCoinSound()
    {
        thisAudioSource.PlayOneShot(thisCoinSound);
    }
}
