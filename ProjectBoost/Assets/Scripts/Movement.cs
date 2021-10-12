using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioManager audioManager;
    RigidBodyManager rigidBodyManager;
    ParticleManager particleManager;
    [SerializeField] float speed = 10f;
    [SerializeField] float rotationalSpeed = 20f;
    
    void Awake()
    {
        rigidBodyManager = GetComponent<RigidBodyManager>();
        audioManager = GetComponent<AudioManager>();
        particleManager = GetComponent<ParticleManager>();
    }

    void Update()
    {
        Thrust();
        Rotation();
    }

    private void Thrust(){
        string mainjet = "mainjet";
        if(Input.GetKey(KeyCode.Space)){
            rigidBodyManager.ForwardAlongY(true, speed);
            audioManager.PlayWhen("thrust", Input.GetKey(KeyCode.Space));
            particleManager.Emit(mainjet);
        }else{
            particleManager.StopEmit(mainjet);
        }  
    }

    private void Rotation(){
        string leftjet = "leftjet";
        string rightjet = "rightjet";
        if(Input.GetKey(KeyCode.RightArrow)){
            rigidBodyManager.ClockWiseAlongZ(true, rotationalSpeed);
            particleManager.Emit(leftjet);
        }else if(Input.GetKey(KeyCode.LeftArrow)){
            rigidBodyManager.ClockWiseAlongZ(false, rotationalSpeed);
            particleManager.Emit(rightjet);
        }else{
            particleManager.StopEmit(rightjet);
            particleManager.StopEmit(leftjet);
        }
    }   
}
