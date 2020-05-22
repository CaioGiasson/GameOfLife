using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour{

    public static GridController controller;
	private void Awake() { controller = this; }

    public Celula celula;
    public int largura = 30, altura = 30;
	public float tempoEntrePassos = 0.15f;

    public GameObject[] botoes;

    public Celula[,] grid;
    Celula[,] novaGrid;

	bool playing;

	void Start(){
        grid = new Celula[largura,altura];
        novaGrid = new Celula[largura,altura];

        foreach (GameObject b in botoes) b.SetActive(false);

		StartCoroutine(AutoPlay());
	}

	public void AtivarBotoes(){
        foreach (GameObject b in botoes) b.SetActive(true);
    }

	public IEnumerator AutoPlay(){
		if (playing) PlayUmPasso();
		yield return new WaitForSeconds(tempoEntrePassos);
		StartCoroutine(AutoPlay());
	}

	public void Play(){
		playing = true;
	}

	public void Pause(){
		playing = false;
	}

	public void PlayUmPasso(){

		// CLONANDO O ARRAY ATUAL EM MEMÓRIA, JÁ SUBSTITUINDO A BOOL "viva"
        foreach (Celula c in grid){

            // CLONANDO A CÉLULA
            Celula nova = Instantiate(celula);

			// VERIFICANDO VIZINHANÇAS
            int n = c.checkVizinhos();
			if ( n<2 ) nova.viva = false;
			else if ( n==2 ) nova.viva = c.viva;
			else if ( n==3 ) nova.viva = true;
			else nova.viva = false;

			//if (nova.viva) print( $"A CÉLULA {nova.i} {nova.j} ESTÁ VIVA");

			// SUBSTITUINDO NA SUPOSTA NOVA GRID
			novaGrid[c.i, c.j] = nova;
		}

		// TROCANDO SOMENTE OS ÍNDICES QUE TIVERAM ALTERAÇÃO
		// ATENÇÃO: É NECESSÁRIO PRIMEIRO CALCULAR A NOVA GRID
		// E SOMENTE DEPOIS DISSO SUBSTITUIR EM TELA
		// PARA EVITAR QUE UMA CÉLULA RECÉM SUBSTITUÍDA
		// INTERFIRA NO RECÁLCULO DAS ADJACENTES
		foreach (Celula c in grid){

			if ( c.viva == novaGrid[c.i, c.j].viva ){
				Destroy(novaGrid[c.i, c.j].gameObject);
			} else{
				int i = c.i;
				int j = c.j;
				novaGrid[i,j].SetPosicao(i,j);
				Destroy(c.gameObject);
				grid[i, j] = novaGrid[i, j];
			}

		}

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
