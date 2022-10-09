using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioState : State<int>
{
    public bool Finished = false;
    string filname;
    AudioSource myAudio;
    AudioClip current;
    AudioClip clipreverb = (AudioClip)Resources.Load("Sounds/cowReverbed");
    AudioClip clipSong = (AudioClip)Resources.Load("Sounds/cow");
    AudioClip Clipfinal = (AudioClip)Resources.Load("Sounds/cowFinal");
    public AudioState(AudioSource _source, string _name, int _id) : base(_id, _name)
    {
        this.Name = _name;
        filname = _name;
        myAudio = _source;
        //myAudio.clip = current;
    }

    public override void OnEnter()
    {
        Finished = false;
        setclip(filname);
        Debug.Log("enter"+ this.Name);
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
        Debug.Log("exit audio" + this.Name);
        Finished = false;
    }
    public void setclip(string _name)
    {
      
        switch (_name)
        {
            case "cowReverbed":
                current = clipreverb;
                break;
            case "cow":
                current = clipSong;
                break;
            case "cow-moo":
                current = Clipfinal;
                break;
            default:
                Debug.Log("No audio clip");
                break;
        }
        myAudio.clip = current;
        
    }
}
