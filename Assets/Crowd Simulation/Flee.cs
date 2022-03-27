using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    private GameObject[] agents;

    // Start is called before the first frame update
    private void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Agent");
        Debug.Log("Agent Count: " + agents.Length);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject agent in agents)
            {
                agent.GetComponent<AgentFleeController>().DetectNewObstacle(this.transform.position);
            }
        }
    }
}