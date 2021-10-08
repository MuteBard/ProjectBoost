using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Awake(){
        SetUpSingleton();
    }

    private void SetUpSingleton(){
       if(FindObjectsOfType<Music>().Length > 1) {
           gameObject.SetActive(false);
           Destroy(gameObject);
       }else{
           DontDestroyOnLoad(gameObject);
       }
    }
}
