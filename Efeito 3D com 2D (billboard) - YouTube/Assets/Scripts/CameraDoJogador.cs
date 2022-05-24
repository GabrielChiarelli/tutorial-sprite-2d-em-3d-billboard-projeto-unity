using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDoJogador : MonoBehaviour
{
    public Transform orientacao;

    public float sensibilidadeHorizontal;
    public float sensibilidadeVertical;

    private float mouseX;
    private float mouseY;

    private float rotacaoX;
    private float rotacaoY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }

    // Update is called once per frame
    void Update()
    {   
        // registra os movimentos do mouse (horizontais e verticais)
        mouseX = Input.GetAxisRaw("Mouse X") * sensibilidadeHorizontal * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensibilidadeVertical * Time.deltaTime;

        rotacaoY += mouseX;
        rotacaoX -= mouseY;

        // impede que o jogador vire a cabeça num ângulo maior que 180 graus
        rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);

        // rotaciona a câmera
        transform.rotation = Quaternion.Euler(rotacaoX, rotacaoY, 0);

        // rotaciona o jogador
        orientacao.rotation = Quaternion.Euler(0, rotacaoY, 0);
    }
}
