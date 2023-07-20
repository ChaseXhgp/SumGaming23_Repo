using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace MimicSpace
{

    /// <summary>
    /// This is a very basic movement script, if you want to replace it
    /// Just don't forget to update the Mimic's velocity vector with a Vector3(x, 0, z)
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;

        public float minPatrolWaitTime;
        public float maxPatrolWaitTime;

        public List<Transform> patrolPoints;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;


         public GameObject playerTarget; // The Player object which the monster chases
        public float detectionRoadius = 10;
        public float killingRadius = 5;
        private NavMeshAgent _agent;
        public LayerMask detectableLayers; 

        [SerializeField]
        private int patrol;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            _agent = GetComponent<NavMeshAgent>();
            StartCoroutine(Wandering());
        }

        private IEnumerator Wandering()
        {
            Debug.Log("Wandering");
            while (true)
            {


                //generate a random index to patrol to
                int patrolIndex = Random.Range(0, patrolPoints.Count);

                patrol = patrolIndex;

                //Patrol to the next point in our list:
                _agent.SetDestination(patrolPoints[patrolIndex].position);

                //Wait for enemy to reach patrol point
                while (Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) > 1) yield return null;

                //wait x seconds
                yield return new WaitForSeconds(Random.Range(minPatrolWaitTime, maxPatrolWaitTime));
                //Generate new move speed

                //Clamp audio to move speed
            }
        }


        void Update()
        {

            /* if (Physics.CheckSphere(transform.position, detectionRoadius, 1 << 6)) //Check if the player is in range or not
             {
                StopCoroutine(Wandering());
                StartCoroutine(ChasePlayer());
             } else
             {
                StopCoroutine(ChasePlayer());
                StartCoroutine(Wandering());
             } */

            //velocity = Vector3.Lerp(velocity, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed, velocityLerpCoef * Time.deltaTime);
            velocity = Vector3.Lerp(velocity, _agent.velocity.normalized * speed, velocityLerpCoef * Time.deltaTime);

            // Assigning velocity to the mimic to assure great leg placement
            myMimic.velocity = velocity;

            transform.position = transform.position + velocity * Time.deltaTime;
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }

        private IEnumerator ChasePlayer()
        {
            Debug.Log("Chasing");
            while (true)
            {
                _agent.SetDestination(playerTarget.transform.position);

                 if (Physics.CheckSphere(transform.position, killingRadius, 1 << 6)) //Check if the player is in killing range or not
                 {
                    print("Kill Player");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                 }

                 yield return null;
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

}