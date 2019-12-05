using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChecker : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource emitter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySound()
    {
        emitter = FindObjectOfType<AudioSource>();
        emitter.clip = clips[SceneController.sceneID];
        emitter.Play();
    }
}
