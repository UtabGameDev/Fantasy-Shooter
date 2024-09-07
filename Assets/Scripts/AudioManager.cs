using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource[] sfx;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StopBgMusic()
    {
        bgMusic.Stop();
    }

    public void PlaySFX(int sfxNumber)
    {
        sfx[sfxNumber].Stop();
        sfx[sfxNumber].Play();
    }
 
    public void StopSFX(int sfxNumber)
    {
        sfx[sfxNumber].Stop(); 
    }
}
