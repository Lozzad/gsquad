using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposit : MonoBehaviour {

    public int amountPerGather;

    // Start is called before the first frame update
    void Start () {
        amountPerGather = 25;
    }

    // Update is called once per frame
    void Update () {

    }

    public int GatherResources () {

        return Random.Range (1, amountPerGather);
    }

}