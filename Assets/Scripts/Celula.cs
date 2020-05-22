using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celula : MonoBehaviour {

    public Material mMorto, mVivo, mSelec;
    public bool viva, selec;
	public int i, j, nVizinhos;

	Renderer r;
	private void Start(){
		r = GetComponent<Renderer>();
	}

	void Update() {
		if (selec) r.material = mSelec;
		else if (viva) r.material = mVivo;
		else r.material = mMorto;

		if (Input.GetMouseButton(0) && selec) viva = !viva;
    }

	private void OnMouseEnter(){
		selec = true;
	}

	private void OnMouseExit(){
		selec = false;
	}

	public void SetPosicao(int x, int z){
		i = x;
		j = z;
		transform.position = new Vector3(i * 1.1f, 0, j * 1.1f);
	}

	public int checkVizinhos(){
		int n = 0;

		int iMax = GridController.controller.largura-1;
		int jMax = GridController.controller.altura-1;

		// NW
		if ( i>0 && j>0 ){
			if (GridController.controller.grid[i - 1, j - 1].viva ){
				n++;
			}
		}

		// N
		if ( j>0 ){
			if (GridController.controller.grid[i, j - 1].viva){
				n++;
			}
		}


		// NE
		if ( i<iMax && j>0 ){
			if (GridController.controller.grid[i + 1, j - 1].viva){
				n++;
			}
		}


		// W
		if ( i>0 ){
			if (GridController.controller.grid[i - 1, j].viva){
				n++;
			}
		}


		// E
		if ( i<iMax){
			if (GridController.controller.grid[i + 1, j].viva){
				n++;
			}
		}

		// SW
		if ( i>0 && j<jMax){
			if (GridController.controller.grid[i - 1, j + 1].viva){
				n++;
			}
		}

		// S
		if ( j<jMax ){
			if (GridController.controller.grid[i, j + 1].viva){
				n++;
			}
		}

		// SE
		if ( i<iMax && j<jMax ){
			if (GridController.controller.grid[i + 1, j + 1].viva){
				n++;
			}
		}

		nVizinhos = n;
		return n;
	}


}


/*

REGRAS

1. Qualquer célula viva com menos de dois vizinhos vivos morre de solidão.
	vizinhos.Length<2

2. Qualquer célula viva com mais de três vizinhos vivos morre de superpopulação.
	vizinhos.length>3

3. Qualquer célula com exatamente três vizinhos vivos se torna uma célula viva.
	vizinhos.length==3

4. Qualquer célula com dois vizinhos vivos continua no mesmo estado para a próxima geração.
	vizinhos.length==2
 
*/
