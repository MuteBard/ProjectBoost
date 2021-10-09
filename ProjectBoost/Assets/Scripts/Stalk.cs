using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : MonoBehaviour
{
    [SerializeField] GameObject chased;
    [SerializeField] float moveSpeed = 50f;
    [SerializeField] string hexAggro = "";
    [SerializeField] string hexStun = "";

    float wait = 5f;
    bool spooked = false;

    void Start(){
        if(hexAggro.Equals("")){
            hexAggro = "#FF00D6";
        }
        if(hexStun.Equals("")){
            hexStun = "#005CFF";
        }
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

        if(distance <= (lightRange) && spooked == true){
            yield return new WaitForSeconds(1f);
            SetColors("#00FFFF");
            yield return new WaitForSeconds(1f);
            SetColors("#FFFFFF");
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }else if(distance <= lightRange || spooked == true){
            spooked = true;
            Retreat();
            SetColors("#005CFF");
            yield return new WaitForSeconds(5f);
            spooked = false;
        }else if(spooked == false){
            SetColors("#FF00D6");
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

    private void SetColors(string color){
        var cubes = GetComponentsInChildren<MeshRenderer>();
        var lights = GetComponentsInChildren<Light>();
        var newCubes = cubes.Select(cube => { 
            cube.material.color = ApplyHexColor(color);
            return cube; 
        }).ToList();
        var newLights = lights.Select(light =>{ 
            light.color = ApplyHexColor(color);
            return light; 
        }).ToList();
    }

    private Color ApplyHexColor(string hexcode){
        if(hexcode[0].Equals("#") && hexcode.Length == 7){
            return new Color(0, 0, 0, 0);
        }
        string color1 = hexcode.Substring(1,2);
        string color2 = hexcode.Substring(3,2);
        string color3 = hexcode.Substring(5,2);
        float number1 = (float) System.Convert.ToInt32(color1, 16);
        float number2 = (float) System.Convert.ToInt32(color2, 16);
        float number3 = (float) System.Convert.ToInt32(color3, 16);
        return new Color(number1, number2, number3, 0);
    }
}
