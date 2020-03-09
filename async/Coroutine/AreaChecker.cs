using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    public float checkRadius = 5f;
    public LayerMask targetLayer;

    private ai myAi;

    private int myPatrolEncounter = 0;

    private void Awake() {
        myAi = GetComponent<ai>();
    }

    private void Update() {
        patrol();
    }


    IEnumerator patrolCoroutine() {
        while (true) {
            patrol();
            yield return new WaitForSeconds(.25f);
            yield return null;
        }
    }

    public void patrol() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, targetLayer);

        Collider targetCollider = findClosest(hitColliders);

        if(targetCollider != null) {
            myAi.track(targetCollider.transform.position);
        } else {
            myAi.stopTracking();
        }
     
        patrolCounter();

    }

    private Collider findClosest(Collider[] hitColliders) {
        float closestDistance = int.MaxValue;
        Collider closestCollider = null;

        for (int i = 0; i < hitColliders.Length; i++) {
            float distance = getcurrentDistance(hitColliders[i].transform.position);

            if(distance < closestDistance) {
                closestCollider = hitColliders[i];
                closestDistance = distance;
            }
        }

        return closestCollider;
    }

    private float getcurrentDistance(Vector3 position) {
        return Vector3.Distance(position, transform.position);
    }

    private void patrolCounter() {
        Debug.Log(myPatrolEncounter);
        myPatrolEncounter++;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
