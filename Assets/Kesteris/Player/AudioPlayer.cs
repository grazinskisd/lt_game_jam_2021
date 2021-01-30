using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource AudioSource;
    [SerializeField]
    List<AudioClip> AudioClips = new List<AudioClip>();
    void Start()
    {
        AudioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        AudioSource.clip = AudioClips.Find(x => x.name.Equals(name));
        AudioSource.Play();
    }
}
