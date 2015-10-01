using UnityEngine;
using System.Collections;

public class MoveLeftAndRight : MonoBehaviour
{
    public float speed = 1.0f; //public variable show up in the unity inspector, doesn't save tho when modif in unity
	// Use this for initialization
    //Everytime an object is created, this wil happen once
	void Start ()
    {
       // transform.position += Vector3.right;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += Vector3.right*Input.GetAxis("Horizontal")*Time.deltaTime*speed; //Time.deltaTime time between 2 frame V3*TD= 1 seconde
                            //Input.GetAxis gets control access on keys for expl (default left right on keyboard) return value between -1 and 1
                            //horizontal is defined in edit/projectsettings/input (change keyboard keys for expl)
                            //You have sensitivity and gravity that accelerates or slows depending of the value.
    }
}
