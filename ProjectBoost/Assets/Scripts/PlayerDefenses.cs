using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenses : MonoBehaviour
{
    [SerializeField] float growthRate = 2f;
    [SerializeField] float iRange = 100f;
    [SerializeField] float iIntensity = 25f;

    [SerializeField] float LightDuration = 5f;
    Light light;
    

    void Start(){
        var child = transform.GetChild(0).gameObject;
        light = child.GetComponent<Light>();
        GetComponent<MeshRenderer>().material.color = applyHexColor("#FF0000");
    }

    void Update(){
        if(Input.GetKey(KeyCode.Z)){
            StartCoroutine(Defend());
        }
    }

    public bool BurnBright(float amount){
        int count = 0;
         if(light.range < iRange){
            light.range += amount;
            count++;
        }
        if(light.intensity < iIntensity){
            light.intensity += amount / 4;
            count++;
        }
        if(count >= 2){
            return true;
        }else{
            return false;
        }
    }

    public bool DimDown(float amount){
        int count = 0;
        if(light.range > 0){
            light.range -= amount;
            count++;
        }
        if(light.intensity > 0){
            light.intensity -= amount / 4;
            count++;
        }
        if(count >= 2){
            return true;
        }else{
            return false;
        }
    }

    private IEnumerator Defend(){
        var gettingBrighter = true;
        var factor = 100000;
        while(gettingBrighter){
            var growthTime = growthRate * factor * Time.deltaTime;
            Debug.Log(growthTime);
            gettingBrighter = BurnBright(growthTime);
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(LightDuration);
        var gettingDimmer = true;
        while(gettingDimmer){
            var growthTime = growthRate * factor * Time.deltaTime;
            Debug.Log(growthTime);
            gettingDimmer = DimDown(growthTime);
            yield return new WaitForSeconds(.1f);
        }
    }

    public float GetLightRange(){
        return light.range;
    }

    private Color applyHexColor(string hexcode){
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
