using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class Audio_handler : MonoBehaviour
{

    private AudioSource audiosource1;
    private AudioSource audiosource2;
    private AudioSource[] audiosources;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip hit;
    public AudioClip hurt;
    public AudioClip powerup;
    public AudioClip step;
    public int CurrentTrack;
    public AudioClip[] musicTracks;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentTrack = 0;

        audiosources = GetComponents<AudioSource>();
        audiosource1 = audiosources[0];
        audiosource2 = audiosources[1];
        audiosource2.loop = true;
        audiosource2.clip = musicTracks[CurrentTrack];
        audiosource2.Play();
        
        
    }
    
    public void Playclip(AudioClip clip)
    {
        audiosource1.PlayOneShot(clip);
        print(clip.name);
    }

    public void changeTrack(int TrackNum)
    {
        audiosource2.clip = musicTracks[TrackNum];
        CurrentTrack = TrackNum;
        audiosource2.Play();
        
    }

    public void nextTrack()
    {
        
        if(CurrentTrack < (musicTracks.Length-1))
        {
            changeTrack(CurrentTrack + 1);   
        }
        else
        {
            changeTrack(0);
        }
        print(CurrentTrack.ToString());
    }

    public void lastTrack()
    {
        if (CurrentTrack > 0)
        {
            changeTrack(CurrentTrack - 1);
        }
        else
        {
            changeTrack((musicTracks.Length - 1));
        }
        print(CurrentTrack.ToString());
    }

    public void stepp()
    {
        if(!audiosource1.isPlaying)
        {
            Playclip(step);
        }
        
    }

}




