using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel {

    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface ScriptControlaInterface;
    public AudioClip SomDeDano;
    public Status StatusJogador;

    private Vector3 direcao;
    private AnimacaoPersonagem animacaoJogador;
    private MovimentoJogador movimentaJogador;

    void Start() {
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        movimentaJogador = GetComponent<MovimentoJogador>();
        StatusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update () {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);
	}

    void FixedUpdate() {
        movimentaJogador.Movimentar(direcao, StatusJogador.Velocidade);
        movimentaJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano) {
        StatusJogador.Vida -= dano;
        ScriptControlaInterface.AtualizaSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if(StatusJogador.Vida <= 0) {
            Morrer();
        }
    }

    public void Morrer() {
        ScriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura) {
        StatusJogador.Vida += quantidadeDeCura;
        if(StatusJogador.Vida > StatusJogador.VidaInicial) {
            StatusJogador.Vida = StatusJogador.VidaInicial;
        }
        ScriptControlaInterface.AtualizaSliderVidaJogador();
    }
}
