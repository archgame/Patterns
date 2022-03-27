using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public FlockManager Manager;
    private float Speed;

    // Start is called before the first frame update
    private void Start()
    {
        Speed = Random.Range(Manager.MinSpeed, Manager.MaxSpeed);
    }

    // step 2
    /*/
    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * Speed); //z is forward direction
    }
    //*/

    //step 3
    public void BoidUpdate()
    {
        GameObject[] boids;
        boids = Manager.boids;

        Vector3 groupCenter = Vector3.zero;
        float groupSpeed = 0.01f;
        int groupSize = 0; //group within distance
        Vector3 avoid = Vector3.zero;

        //update calculations
        //1. Move toward average position of the group.
        //2. Align with the average heading of the group.
        //3. Avoid crowding other group members.
        foreach (GameObject go in boids)
        {
            if (go == this.gameObject) { continue; } //next, skip

            float distance = Vector3.Distance(go.transform.position, this.transform.position);
            if (distance > Manager.NeighborDistance) { continue; } //next skip

            groupCenter += go.transform.position;
            groupSize++;

            if (distance < 1.0f) //hard coded, how close we can be to another boid before avoiding it
            {
                avoid = avoid + (this.transform.position - go.transform.position);
            }

            Boid boidScript = go.GetComponent<Boid>();
            groupSpeed += boidScript.Speed;
        }

        if (groupSize > 0)
        {
            groupCenter = groupCenter / groupSize;
            groupSpeed = groupSpeed / groupSize;

            Vector3 direction = (groupCenter + avoid) - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion quat = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    this.transform.rotation,
                    quat,
                    Manager.RotationSpeed * Time.deltaTime);
            }
        }

        //move boid
        transform.Translate(0, 0, Time.deltaTime * Speed); //z is forward direction
    }
}