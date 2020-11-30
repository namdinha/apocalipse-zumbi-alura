using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour {

    public static AudioSource instancia;

    private AudioSource meuAudioSource;

    // Use this for initialization
    void Awake () {
        meuAudioSource = GetComponent<AudioSource>();
        instancia = meuAudioSource;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
