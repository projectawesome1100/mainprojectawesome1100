using UnityEngine;
using System.Collections;

public class GibOnTrigger : MonoBehaviour {

    public GameObject gib;

    void OnTriggerEnter()
    {
        Instantiate(gib, transform.position, Quaternion.identity); //Quaternion.identity = Osef rotation
        Destroy(gameObject);
    }
}
