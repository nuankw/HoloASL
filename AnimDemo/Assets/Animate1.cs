using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate1 : MonoBehaviour {

    public Animator Anim1;

	// Use this for initialization
	void Start () {
        Anim1 = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Anim_A started.");
            Anim1.Play("Anim_A");
            Debug.Log("Anim_A stopped.");
        }
	}
}
