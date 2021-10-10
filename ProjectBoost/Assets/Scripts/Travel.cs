using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour
{
    
    [Header("Translation")]
    [SerializeField] float moveSpeed = 50f;
    [SerializeField] GameObject path;
    int currentVector = 0;
    List<Vector3> vectors;
    void Start(){
        vectors = GetAllWayPointVectors();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move(){
        if(currentVector < vectors.Count){    
            Vector3 targetPosition = vectors[currentVector];
            float MovementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MovementThisFrame);
            if(transform.position == targetPosition){
                currentVector++;
            }
        }else{
            currentVector = 0;
        }
    }

    private List<Vector3> GetAllWayPointVectors(){
        List<Vector3> vectors = new List<Vector3>();
        var wayPoints = path.GetComponentsInChildren<WayPoint>();
        return wayPoints.Select(wayPoint => wayPoint.GetPosition()).ToList();
    }
}
