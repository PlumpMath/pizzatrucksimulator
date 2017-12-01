using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour {


    public List<GameObject> points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    int lengthOfPoints;



    void Start() {

		points = new List<GameObject>();
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Waypoint")) {

			points.Add(o);
            
        }
        agent = GetComponent<NavMeshAgent>();
        lengthOfPoints = points.Count;
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;



        GotoNextPoint();
    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[Random.Range(0, points.Count)].transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.

        //    destPoint = (destPoint + 1) % points.Length;

        //     destPoint = points[2];// % points.Length;

        //	Random.Range(0,points.Length);
    }


    void Update() {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}