using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other){
        switch(other.gameObject.tag){
            case "Finish":
                StartCoroutine(NextLevel());
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

    private IEnumerator NextLevel(){
        int nextIndex = SceneManager.GetActiveScene().buildIndex;
        if( nextIndex <= SceneManager.sceneCount){
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(nextIndex + 1);
        }else{
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }
    }
}
