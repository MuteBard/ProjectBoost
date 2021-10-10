using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] AudioClip audioClip;
    AudioManager audioManager;
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float rotationalThrust = 2f;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioManager = GetComponent<AudioManager>();
    }

    
    void Update()
    {
        Thrust();
        Rotation();
    }

    private void Thrust(){
        var keyPressed = Input.GetKey(KeyCode.Space);
        if(keyPressed){
            Vector3 rocketPosition = new Vector3(0, 1, 0);
            rigidBody.freezeRotation = true;
            rigidBody.AddRelativeForce(rocketPosition * mainThrust * Time.deltaTime);
            rigidBody.freezeRotation = false;
        }
        
        ApplyThrustNoise(keyPressed);
    }

// https://docs.unity3d.com/ScriptReference/KeyCode.html
    private void Rotation(){
        if(Input.GetKey(KeyCode.RightArrow)){
            ApplyRotation(1);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            ApplyRotation(-1);
        }
    }


// https://docs.unity3d.com/ScriptReference/AudioSource.html
    private void ApplyThrustNoise(bool keyPressed){
        bool turnOn = false;
        if(keyPressed){
            if(audioManager.IsNotPlaying()){
                audioManager.SetVolume(1);
                audioManager.Play("thrust");
            }
        }else{
            audioManager.Stop();
        }
        
    }

    private void ApplyRotation(float direction){
        Vector3 rocketRotation = new Vector3(0, 0, 1) * direction;
        rigidBody.freezeRotation = true;
        transform.Rotate(rocketRotation * rotationalThrust * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }
}
