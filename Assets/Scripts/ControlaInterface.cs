using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {

    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    public Text TextoQuantidadeZumbisMortos;
    public Text TextoAvisoDeChefe;

    private ControlaJogador scriptControlaJogador;
    private float tempoPontuacaoSalvo;
    private int quantidadeDeZumbisMortos;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;

        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        Debug.Log(scriptControlaJogador.StatusJogador);

        SliderVidaJogador.maxValue = scriptControlaJogador.StatusJogador.Vida;
        AtualizaSliderVidaJogador();
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizaSliderVidaJogador() {
        SliderVidaJogador.value = scriptControlaJogador.StatusJogador.Vida;
    }

    public void GameOver() {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int) (Time.timeSinceLevelLoad / 60);
        int segundos = (int) (Time.timeSinceLevelLoad % 60);

        TextoTempoDeSobrevivencia.text = string.Format("Você sobreviveu por {0}min e {1}s", minutos, segundos);

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima(int min, int seg) {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo) {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if(TextoPontuacaoMaxima.text == "") {
            min = (int)(tempoPontuacaoSalvo / 60);
            seg = (int)(tempoPontuacaoSalvo % 60);
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
        }
    }

    public void Reiniciar() {
        SceneManager.LoadScene("game");
    }

    public void AtualizarQuantidadeDeZumbisMortos() {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeZumbisMortos.text = string.Format("X {0}", quantidadeDeZumbisMortos);
    }

    public void AparecerTextoChefeCriado() {
        StartCoroutine(DesaparecerTexto(2, TextoAvisoDeChefe));
    }

    IEnumerator DesaparecerTexto(float tempoDeSumico, Text textoParaSumir) {
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        textoParaSumir.gameObject.SetActive(true);
        yield return new WaitForSeconds(tempoDeSumico);
        float contador = 0;
        while(textoParaSumir.color.a > 0) {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if(textoParaSumir.color.a <= 0) {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}