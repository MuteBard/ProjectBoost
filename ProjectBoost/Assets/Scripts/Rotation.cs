using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] [Range(1f, 2000f)] float RotationalSpeed = 1000f;
    [SerializeField] bool x = false;
    [SerializeField] bool y = false;
    [SerializeField] bool z = true;

    void Update()
    {
        Move();
    }

    private void Move(){
        int ix = x ? 1 : 0;
        int iy = y ? 1 : 0;
        int iz = z ? 1 : 0;
        Vector3 objRotation = new Vector3(1 * ix, 1 * iy, 1 * iz);
        transform.Rotate(objRotation * RotationalSpeed * Time.deltaTime);
    }
}
