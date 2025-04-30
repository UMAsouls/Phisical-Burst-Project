using System.Collections;
using UnityEngine;

public class SEPlayer : MonoBehaviour, SEPlayable
{
    [SerializeField]
    AudioSource[] sources;

    private int playIdx;

    public void PlaySE(AudioClip clip)
    {
        sources[playIdx].clip = clip;
        sources[playIdx].Play();
        playIdx = (playIdx + 1)%sources.Length;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}