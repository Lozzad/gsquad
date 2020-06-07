using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {
    public int storedResources;
    public int goblinthreshold = 50;
    public GameObject goblinPrefab;

    // Start is called before the first frame update
    void Start () {
        storedResources = 0;
    }

    // Update is called once per frame
    void Update () {
        if (storedResources >= goblinthreshold) {
            SpawnGoblin ();
            storedResources -= goblinthreshold;
        }
    }

    public void DepositMaterial (int amount) {
        storedResources += amount;
    }

    void SpawnGoblin () {
        Vector3 pos = this.gameObject.transform.position + new Vector3 (2, 0, 2);
        var goblin = Instantiate (goblinPrefab, pos, Quaternion.identity);
        goblin.transform.parent = this.gameObject.transform;
    }
}