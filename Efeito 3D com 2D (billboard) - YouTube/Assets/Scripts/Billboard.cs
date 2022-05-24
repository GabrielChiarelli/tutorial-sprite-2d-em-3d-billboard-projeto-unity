using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // referência a câmera do jogador
    private Camera cameraDoJogador;

    // Start is called before the first frame update
    void Start()
    {
        // coloca a câmera do jogo como valor da variável 'cameraDoJogador'
        cameraDoJogador = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // roda o método 'RotacionarSprite()'
        RotacionarSprite();
    }

    private void RotacionarSprite()
    {
        // gira o sprite inteiro se baseando na rotação da câmera
        transform.rotation = cameraDoJogador.transform.rotation;
        // garante que somente o eixo 'y' do sprite gire
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
