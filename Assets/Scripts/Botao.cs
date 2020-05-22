using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour{

	public Material mLivre, mSelec;
	bool selec;

	public GameObject[] elementosUI;
	Renderer[] rs;

	private void Start(){
		rs = new Renderer[elementosUI.Length];
		for ( int i=0; i<elementosUI.Length; i++ )
			rs[i] = elementosUI[i].GetComponent<Renderer>();
	}

	void Update(){
		if (selec) foreach(Renderer r in rs) r.material = mSelec;
		else foreach (Renderer r in rs) r.material = mLivre;

		if ( Input.GetMouseButtonUp(0) && selec ){
			switch (name){

				case "BtnSpeed1":
					GridController.controller.tempoEntrePassos = 0.5f;
					break;

				case "BtnSpeed2":
					GridController.controller.tempoEntrePassos = 0.3f;
					break;

				case "BtnSpeed3":
					GridController.controller.tempoEntrePassos = 0.1f;
					break;

				case "BtnPlay":
					GridController.controller.Play();
					break;

				case "BtnPause":
					GridController.controller.Pause();
					break;

				case "BtnStep":
				default:
					GridController.controller.PlayUmPasso();
					break;

			}
		}
	}

	private void OnMouseEnter(){
		selec = true;
	}

	private void OnMouseExit(){
		selec = false;
	}

}
