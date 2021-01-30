using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0, 1)]
    public float ambientVolume;

    [Range(0, 1)]
    public float effectVolume;

    private void Awake()
    {
        
    }
}
