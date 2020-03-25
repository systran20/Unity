using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume=0.66f;

    //MUZİKLERİ AYRI MUSİC SINIFINDA TOPLAYINCA BUNA GEREK KALMADI
    //public bool loop;

   [Header("Play On Scene")]
    public int pOnScene;

    [HideInInspector]
    public AudioSource source;
    
}
