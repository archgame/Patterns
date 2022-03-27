using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentFleeController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] goalLocations;

    private void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("Goal");
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        ResetAgent(); //step 1 fleeing
    }

    private void Update()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent(); // step 1
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    //Step 1
    private void ResetAgent()
    {
        float sm = Random.Range(0.1f, 1.5f);
        agent.speed = 2 * sm;
        agent.angularSpeed = 120;
        agent.ResetPath();
    }

    //step 2
    private float detectionRadius = 5;

    private float fleeRadius = 10;

    public void DetectNewObstacle(Vector3 position)
    {
        //if farther than radius, return, which means exit this function (run no more code)
        if (Vector3.Distance(position, this.transform.position) > detectionRadius) { return; }

        Vector3 fleeDir = (this.transform.position - position).normalized;
        Vector3 goal = this.transform.position + fleeDir * fleeRadius;

        agent.SetDestination(goal);

        /*/
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(goal,path);
        if(path.status != NavMeshPathStatus.PathInvalid)
        {
            agent.SetDestination(path.corners[path.corners.Length - 1]);
        }
        //*/
        agent.speed = 10;
        agent.angularSpeed = 500;
    }
}