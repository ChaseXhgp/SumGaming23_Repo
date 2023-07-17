using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject playerTarget; // The Player object which the monster chases
    public float moveSpeed = 10;
    public float detectionRoadius = 10;
    public float killingRadius = 5;
    private NavMeshAgent _agent;
    public LayerMask detectableLayers; 

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, detectionRoadius, 1 << 6)) //Check if the player is in range or not
        {
            _agent.SetDestination(playerTarget.transform.position);

            if (Physics.CheckSphere(transform.position, killingRadius, 1 << 6)) //Check if the player is in killing range or not
        {
            print("Kill Player");
        }
        }
        
    }

     void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRoadius);

         Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killingRadius);
    }
}
