using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFootsteps : MonoBehaviour
{
    public AudioSource mASource;
    public AudioClip[] mAClipList;
    void Start()
    {
    }
    void Update()
    {
        
    }
    public void NormalFootsteps() {
        mASource.volume = 0.5f;
        mASource.PlayOneShot(mAClipList[Random.Range(0, 3)]);
    }
    public void HeavyFootsteps() {
        mASource.volume = 1f;
        mASource.PlayOneShot(mAClipList[Random.Range(0, 3)]);
        mASource.volume = 0.5f;
    }
}
