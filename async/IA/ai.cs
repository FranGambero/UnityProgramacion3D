using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
    public float maxMoveDistance = 5;

    private NavMeshAgent myAgent;

    private bool myTracking = false;
    private Vector3 myMovePosition;

    private void Awake() {
        myAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        think();
    }

    public void think() {
        float distance = Vector3.Distance(myMovePosition, transform.position);

        if(!myTracking && (distance < 2 || distance > 20)) {
            randonMove();
        }
    }

    public void track(Vector3 position) {
        myTracking = true;
        myMovePosition = position;
        myAgent.SetDestination(myMovePosition);
    }

    public void stopTracking() {
        myTracking = false;
    }

    private void randonMove() {
        NavMeshHit hit;
        NavMesh.SamplePosition(Random.insideUnitSphere * maxMoveDistance, out hit, maxMoveDistance, NavMesh.AllAreas);
        myMovePosition = hit.position;
        myAgent.SetDestination(myMovePosition);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(myMovePosition, 1f);
    }
}