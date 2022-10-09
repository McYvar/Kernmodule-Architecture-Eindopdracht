using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioState : State<int>
{
    public bool Finished = false;
    AudioSource myAudio;
    AudioClip clipreverb = (AudioClip)Resources.Load("Sounds/cowReverbed");
    AudioClip clipSong = (AudioClip)Resources.Load("Sounds/cow");
    AudioClip Clipmoo = (AudioClip)Resources.Load("Sounds/cow-moo");
    public AudioState(AudioSource _source, string _name, int _id) : base(_id, _name)
    {
        this.Name = _name;//"attack";
        myAudio = _source;
    }

    public override void OnEnter()
    {
        Debug.Log("enter");
        myAudio.clip = clipreverb;
        myAudio.Play();
    }
    public override void OnUpdate()
    {
        if(!myAudio.isPlaying)
        {
            Finished = true;
        }
    }
    public override void OnExit()
    {
        Debug.Log("exit audio fase 1");
    }
}
