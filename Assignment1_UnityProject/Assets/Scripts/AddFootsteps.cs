using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFootsteps : MonoBehaviour
{
    AudioSource mASource;
    AudioClip[] mAClipList = new AudioClip[4];
    public AudioClip footstep1;
    public AudioClip footstep2;
    public AudioClip footstep3;
    public AudioClip footstep4;
    void Start()
    {
        mAClipList[0] = footstep1;
        mAClipList[1] = footstep2;
        mAClipList[2] = footstep3;
        mAClipList[3] = footstep4;
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor") {
            mASource.PlayOneShot(mAClipList[Random.Range(0, 3)]);
        }
    }
}
