using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Camera cam;

    public LayerMask walkable; 

    PlayerMotor motor;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;//sets camer equal to the main cam

        motor = GetComponent<PlayerMotor>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, walkable))
            {
                motor.MoveToPoint(hit.point);//moves to the point that was clicked   
            }
        }


    }
}
 