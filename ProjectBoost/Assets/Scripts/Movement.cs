using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioManager audioManager;
    RigidBodyManager rigidBodyManager;
    [SerializeField] float speed = 10f;
    [SerializeField] float rotationalSpeed = 20f;
    
    void Awake()
    {
        rigidBodyManager = GetComponent<RigidBodyManager>();
        audioManager = GetComponent<AudioManager>();
    }

    void Update()
    {
        Thrust();
        Rotation();
    }

    private void Thrust(){
        rigidBodyManager.MoveForwardWhen("y", speed, Input.GetKey(KeyCode.Space));
        audioManager.PlayWhen("thrust", Input.GetKey(KeyCode.Space));
    }

    private void Rotation(){
        rigidBodyManager.MoveClockWiseWhen("z", rotationalSpeed, Input.GetKey(KeyCode.RightArrow));
        rigidBodyManager.MoveCounterClockWiseWhen("z", rotationalSpeed, Input.GetKey(KeyCode.LeftArrow));
    }   
}
