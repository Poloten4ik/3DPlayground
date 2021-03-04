using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{

    [SerializeField] private float speed = 10f;

    void Start()
    {
        
    }



    void Update()
    {
        //Vector3 newPosition = transform.position;
        //newPosition.y += speed * Time.deltaTime;
        //transform.position = newPosition;

        if (transform.position.y < 40)
        {
            transform.DOMoveY(40, 2f);
        }
        
    }
}
