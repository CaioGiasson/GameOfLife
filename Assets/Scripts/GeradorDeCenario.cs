using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeCenario : MonoBehaviour{

	public Celula celula;

	private float delay = 0.01f;
	void Start(){
		StartCoroutine( GerarGrid() );
	}

	IEnumerator GerarGrid(){
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < GridController.controller.largura; i++){
			for (int j = 0; j < GridController.controller.altura; j++){
				GridController.controller.grid[i, j] = Instantiate(celula);
				GridController.controller.grid[i, j].SetPosicao(i, j);

				if (i == 0) yield return new WaitForSeconds(delay);
			}
			yield return new WaitForSeconds(delay);
		}

		CameraController.cam.TerminouCarregar();
		yield return new WaitForSeconds(1.0f);
		GridController.controller.AtivarBotoes();

	}

}
