using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]//creates a nav mesh agent when this component is added to an object
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent; 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        
    }

    public void MoveToPoint(Vector3 point)
    {

        agent.SetDestination(point);


    }

}
