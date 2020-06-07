using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AgentController : MonoBehaviour {
    NavMeshAgent navMeshAgent;
    public GameObject destination;
    BaseController homeBase;

    public enum AgentState { IDLE, TRAVELLING, INTERACTING, }

    public AgentState state;

    public int carriedResources;

    // Start is called before the first frame update
    void Start () {
        //get the navmesh controller
        navMeshAgent = GetComponent<NavMeshAgent> ();
        if (navMeshAgent == null) {
            Debug.LogError ("NO NAVMESH CONTROLLER ON " + this);
        }

        //initialise variables
        carriedResources = 0;
        homeBase = FindObjectOfType<BaseController> ();

        //set the initial state
        state = AgentState.IDLE;
    }

    // Update is called once per frame
    void Update () {
        //switch for state
        switch (state) {
            //idle - just hanging around
            case AgentState.IDLE:
                if (carriedResources > 0) {
                    destination = homeBase.gameObject;
                } else {
                    destination = GameObject.FindGameObjectWithTag ("resource");
                }
                state = AgentState.TRAVELLING;
                break;

                //travelling between destinations
            case AgentState.TRAVELLING:
                navMeshAgent.SetDestination (destination.transform.position);
                break;

                //interacting with objects
            case AgentState.INTERACTING:

                break;
            default:
                Debug.LogError ("Default state for " + this);
                break;
        }
    }

    void OnTriggerEnter (Collider col) {
        if (col.gameObject == destination) {
            StartCoroutine (StopForSeconds ());

            if (col.gameObject == homeBase.gameObject) {
                homeBase.DepositMaterial (carriedResources);
                carriedResources = 0;
                state = AgentState.IDLE;
            }
            if (col.gameObject.tag == "resource") {
                Debug.Log (this.gameObject.name + "collided with resource ");
                destination = homeBase.gameObject;
                carriedResources = col.gameObject.GetComponent<ResourceDeposit> ().GatherResources ();
                state = AgentState.IDLE;
            }
        }
    }

    public IEnumerator StopForSeconds () {
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds (2f);
        navMeshAgent.isStopped = false;
    }
}