using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other){
        switch(other.gameObject.tag){
            case "Finish":
                NextLevel();
                break;
            case "Friendly":
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                StartCoroutine(ReloadLevel());
                break;
        }
    }
    //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html
    private IEnumerator ReloadLevel(){
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentIndex);
    }

    void NextLevel(){
        int nextIndex = SceneManager.GetActiveScene().buildIndex;
        if( nextIndex <= SceneManager.sceneCount){
            SceneManager.LoadScene(nextIndex + 1);
        }else{
            SceneManager.LoadScene(0);
        }
    }
}
