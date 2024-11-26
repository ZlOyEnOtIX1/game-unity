using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private Transform character;

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.x = character.position.x;
        temp.y = character.position.y;

        transform.position = temp;
    }
}
