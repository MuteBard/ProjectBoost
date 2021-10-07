using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource au;
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float rotationalThrust = 2f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();
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
            rb.freezeRotation = true;
            rb.AddRelativeForce(rocketPosition * mainThrust * Time.deltaTime);
            rb.freezeRotation = false;
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

    private IEnumerator volumeManagement(bool turnOn){
        float direction;
        float volChangeDelay = .05f;
        float volChangeInterval = .05f;
        if(turnOn){
            direction = 1f;
            au.Play();
            while(au.volume < 1){
                float volChange = au.volume + direction * volChangeInterval;
                yield return new WaitForSeconds(volChangeDelay);
                au.volume = volChange; 
            }
        }else{
            direction = -1f;
            while(au.volume > 0){
                float volChange = au.volume + direction * volChangeInterval;
                yield return new WaitForSeconds(volChangeDelay);
                au.volume = volChange; 
            }
            au.Stop();
        }
    }


// https://docs.unity3d.com/ScriptReference/AudioSource.html
    private void ApplyThrustNoise(bool keyPressed){
        if(keyPressed){
            if(!au.isPlaying){
                StartCoroutine(volumeManagement(true));
            }
        }else{
            StartCoroutine(volumeManagement(false));
        }
    }

    private void ApplyRotation(float direction){
        Vector3 rocketRotation = new Vector3(0, 0, 1) * direction;
        rb.freezeRotation = true;
        transform.Rotate(rocketRotation * rotationalThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
