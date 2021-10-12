using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : MonoBehaviour
{
    [SerializeField] GameObject chased;
    [SerializeField] float moveSpeed = 50f;
    bool spooked = false;
    ColorManager colorManager;

    void Start(){
        colorManager = gameObject.GetComponent<ColorManager>();
    }
    void Update()
    {
        Chase();
    }
    
    public void SetChased(GameObject chased){ this.chased = chased; }
    private void Chase(){      
        StartCoroutine(LockOn());
    }

    private IEnumerator LockOn(){
        float distance = Vector3.Distance(chased.transform.position, transform.position);
        float lightRange = chased.GetComponent<PlayerDefenses>().GetLightRange();

        if(distance <= lightRange && spooked == true){
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }else if(distance <= lightRange || spooked == true){
            spooked = true;
            Retreat();
            colorManager.SetColor("#005CFF");
            yield return new WaitForSeconds(5f);
            spooked = false;
        }else if(spooked == false){
            colorManager.SetDefaultColor();
            Attack();
        }
    }

    void Attack(){
        float factor = 10;
        float tempMovementSpeed = moveSpeed;
        float MovementThisFrame = tempMovementSpeed * factor * Time.deltaTime;
        var lastPosition = chased.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, chased.transform.position, MovementThisFrame);
    }
    void Retreat(){
        float factor = 10;
        float tempMovementSpeed = moveSpeed * 0;
        float MovementThisFrame = tempMovementSpeed * factor * Time.deltaTime;
        var lastPosition = chased.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,0), MovementThisFrame);
    }
}
