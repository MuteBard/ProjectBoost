using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject finissimo;
    [SerializeField] GameObject chased;

    void Update()
    {
        SpawnAtRandomTime();
    }


    void SpawnRandomPosition(){
        Vector3 position = new Vector3(Random.Range(-5000f, 5000f), Random.Range(-5000f, 5000f), -12000f);
        GameObject newFinissimo = Instantiate(finissimo, position, Quaternion.identity);
        newFinissimo.GetComponent<Stalk>().SetChased(chased);
    }

    void SpawnAtRandomTime(){
        int number = Random.Range(0, 500);
        int count = GameObject.FindGameObjectsWithTag("Finissimo").Length;
        if(count <= 2 && number == 250){
            SpawnRandomPosition();
        }
    }
}
