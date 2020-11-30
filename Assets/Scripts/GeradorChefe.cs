using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour {

    public float TempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    public Transform[] PosicoesDeGeracao;

    private float tempoProximaGeracao;
    private ControlaInterface scriptControlaInterface;
    private Transform jogador;

    private void Start() {
        tempoProximaGeracao = TempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update() {
        if(Time.timeSinceLevelLoad > tempoProximaGeracao) {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.AparecerTextoChefeCriado();
            tempoProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador() {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;
        foreach(Transform posicao in PosicoesDeGeracao) {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreOJogador > maiorDistancia) {
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }
        return posicaoDeMaiorDistancia;
    }
}
