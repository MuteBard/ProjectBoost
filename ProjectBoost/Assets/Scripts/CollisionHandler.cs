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
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                ReloadLevel();
                break;
        }
    }
    //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html
    void ReloadLevel(){
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
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
