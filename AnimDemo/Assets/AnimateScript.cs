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
        if (Input.GetKeyDown("1"))
        {
            HalfSpeed();
        }
        if (Input.GetKeyDown("0"))
        {
            FullSpeed();
        }
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

    void HalfSpeed()
    {
        Avatar.speed = 0.5f;
    }

    void FullSpeed()
    {
        Avatar.speed = 1.0f;
    }
}
