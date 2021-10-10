using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject finissimo;
    [SerializeField] GameObject chased;
    [SerializeField] int spawnRate = 1;

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
        int number = Random.Range(0, 1000 / spawnRate);
        int count = GameObject.FindGameObjectsWithTag("Finissimo").Length;
        if(count <= 5 && number == 1){
            SpawnRandomPosition();
        }
    }
}
