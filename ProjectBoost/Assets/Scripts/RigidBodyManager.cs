using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyManager : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float speedMuliplier = 5000f;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // https://homeweb.csulb.edu/~pnguyen/cecs475/pdf/cecs475lec5.pdf
    public void ClockWiseAlongX(bool moveRight, float speed){
        Vector3 rotation = moveRight ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        ClockWise(rotation * speed * speedMuliplier * Time.deltaTime);
    }

    public void ClockWiseAlongY(bool moveRight, float speed){
        Vector3 rotation = moveRight ? new Vector3(0, 1, 0)  : new Vector3(0, -1, 0);
        ClockWise(rotation * speed * speedMuliplier * Time.deltaTime);
    }

    public void ClockWiseAlongZ(bool moveRight, float speed){
        Vector3 rotation = moveRight ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1);
        ClockWise(rotation * speed * speedMuliplier * Time.deltaTime);
    }

    public void ForwardAlongX(bool moveForward, float speed){
        Vector3 position = moveForward ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        ForwardAlong(position * speed * speedMuliplier *  Time.deltaTime);
    }

    public void ForwardAlongY(bool moveForward, float speed){
        Debug.Log("HIII");
        Vector3 position = moveForward ? new Vector3(0, 1, 0) : new Vector3(0, -1, 0);
        ForwardAlong(position * speed * speedMuliplier *  Time.deltaTime);
    }

    public void ForwardAlongZ(bool moveForward, float speed){
        Vector3 position = moveForward ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1);
        ForwardAlong(position * speed * speedMuliplier *  Time.deltaTime);
    }

    private void ClockWise(Vector3 rotation){
        rigidBody.freezeRotation = true;
        transform.Rotate(rotation);
        rigidBody.freezeRotation = false;
    }

    private void ForwardAlong(Vector3 position){
        rigidBody.freezeRotation = true;
        rigidBody.AddRelativeForce(position);
        rigidBody.freezeRotation = false;
    }

    public void MoveClockWiseWhen(string axis, float speed, bool condition){
        if(condition){
            switch(axis.ToLower()){
                case "x":
                    ClockWiseAlongX(true, speed);
                    break;
                case "y":
                    ClockWiseAlongY(true, speed);
                    break;
                case "z":
                    ClockWiseAlongZ(true, speed);
                    Debug.Log("Right");
                    break;
                default:
                    Debug.Log("INVALID AXIS PROVIDED");
                    break;
            }
        }
    }

    public void MoveCounterClockWiseWhen(string axis, float speed, bool condition){
        if(condition){
            switch(axis.ToLower()){
                case "x":
                    ClockWiseAlongX(false, speed);
                    break;
                case "y":
                    ClockWiseAlongY(false, speed);
                    break;
                case "z":
                    ClockWiseAlongZ(false, speed);
                     Debug.Log("Left");
                    break;
                default:
                    Debug.Log("INVALID AXIS PROVIDED");
                    break;
            }
        }
    }

    public void MoveForwardWhen(string axis, float speed, bool condition){
        if(condition){
            switch(axis.ToLower()){
                case "x":
                    ForwardAlongX(true, speed);
                    break;
                case "y":
                    ForwardAlongY(true, speed);
                    break;
                case "z":
                    ForwardAlongZ(true, speed);
                    break;
                default:
                    Debug.Log("INVALID AXIS PROVIDED");
                    break;
            }
        }
    }

    public void MoveBackwardWhen(string axis, float speed, bool condition){
        if(condition){
            switch(axis.ToLower()){
                case "x":
                    ForwardAlongX(false, speed);
                    break;
                case "y":
                    ForwardAlongY(false, speed);
                    break;
                case "z":
                    ForwardAlongZ(false, speed);
                    break;
                default:
                    Debug.Log("INVALID AXIS PROVIDED");
                    break;
            }
        }
    }
    
    // public void Chase(float speed, GameObject chased){
    //     float MovementThisFrame = speed * speedMuliplier * Time.deltaTime;
    //     var lastPosition = chased.transform.position;
    //     Vector3.MoveTowards(transform.position, chased.transform.position, MovementThisFrame);
    // }
    // public void Flee(float speed){
    //     float MovementThisFrame = speed * speedMuliplier * Time.deltaTime;
    //     var lastPosition = chased.transform.position;
    //     rigidBody.AddRelativeForce(position);
    // }

    public void TurnAroundX(float degrees, float speed){
        Vector3 to = new Vector3(Math.Abs(degrees), 0, 0);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
    }

    public void TurnAroundY(float degrees, float speed){
        Vector3 to = new Vector3(0, Math.Abs(degrees), 0);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
    }

    public void TurnAroundZ(float degrees, float speed){
        Vector3 to = new Vector3(0, 0, Math.Abs(degrees));
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
    }
}
