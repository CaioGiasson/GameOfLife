using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    public static CameraController cam;
	private void Awake() { cam = this; }

    Animator anim;
	void Start(){
        anim = GetComponent<Animator>();
    }

	public void TerminouCarregar() {
		anim.SetTrigger("TerminouCarregar");
	}
}
