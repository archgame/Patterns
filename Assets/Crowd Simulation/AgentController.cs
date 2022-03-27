using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] goalLocations;

    //step 5 .zip file First Person Package, add navmesh obstacle
    private void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("Goal");
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        float sm = Random.Range(0.1f, 1.5f); //step 4
        agent.speed = 2 * sm; //step 4
        ResetAgent(); //step 6 fleeing
    }

    private void Update()
    {
        // step 2
        if (agent.remainingDistance < 1)
        {
            //ResetAgent(); step 6 optional
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
        //step 3 change priority/speed of red agents
    }

    //Step 6
    private void ResetAgent()
    {
        float sm = Random.Range(0.1f, 1.5f);
        agent.speed = 2 * sm;
        agent.angularSpeed = 120;
        agent.ResetPath();
    }

    //step 7
    private float detectionRadius = 5;

    private float fleeRadius = 10;

    public void DetectNewObstacle(Vector3 position)
    {
    }
}