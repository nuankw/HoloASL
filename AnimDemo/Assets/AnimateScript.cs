using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateScript : MonoBehaviour {

    public Animator Avatar;

	// Use this for initialization
	void Start () {
        Avatar = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Anim_A started.");
            Avatar.Play("Anim_A");
            Debug.Log("Anim_A stopped.");
        }
        if (Input.GetKeyDown("c"))
        {
            Debug.Log("Anim_Hi started.");
            Avatar.Play("Anim_Hi");
            Debug.Log("Anim_Hi stopped.");
        }
	}
}
