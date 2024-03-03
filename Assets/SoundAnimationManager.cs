using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundAnimationManager : MonoBehaviour
{
    public AudioSource m_AudioSource;
    public AudioClip[] audioClips;
    public int selectedClip;
    public bool PlaySound;

    public void FireClip() 
    {
        m_AudioSource.PlayOneShot(audioClips[selectedClip]);
    }


    public void FireClip(int select)
    {
        m_AudioSource.PlayOneShot(audioClips[select]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlaySound && !m_AudioSource.isPlaying) { FireClip(); }
    }
}
