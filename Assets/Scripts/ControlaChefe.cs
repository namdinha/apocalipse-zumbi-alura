﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel {

    public GameObject KitMedicoPrefab;
    public Slider SliderVidaChefe;
    public Image ImagemSlider;
    public Color CorVidaMaxima, CorVidaMinima;
    public GameObject ParticulaSangue;

    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;


    private void Start() {
        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();
        agente.speed = statusChefe.Velocidade;
        SliderVidaChefe.maxValue = statusChefe.VidaInicial;
        AtualizarInterface();
    }

    private void Update() {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if(agente.hasPath) {
            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;
            if(estouPertoDoJogador) {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            }
            else {
                animacaoChefe.Atacar(false);
            }
        }
    }

    void AtacaJogador() {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano) {
        statusChefe.Vida -= dano;
        AtualizarInterface();
        if(statusChefe.Vida <= 0) {
            Morrer();
        }
    }

    public void Morrer() {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 3);
    }

    public void SoltarParticulaSangue(Vector3 posicao, Quaternion rotacao) {
        Instantiate(ParticulaSangue, posicao, rotacao);
    }

    void AtualizarInterface() {
        SliderVidaChefe.value = statusChefe.Vida;
        float porcentagemVida = (float) statusChefe.Vida / statusChefe.VidaInicial;
        Color corDaVida = Color.Lerp(CorVidaMinima, CorVidaMaxima, porcentagemVida);
        ImagemSlider.color = corDaVida;
    }
}
