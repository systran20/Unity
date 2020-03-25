using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Music
{
    public string name;
        
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume=0.66f;

    public bool loop=true;

    [Header("Play On Scene")]
    public int pOnScene;

    [HideInInspector]
    public AudioSource source;

}
