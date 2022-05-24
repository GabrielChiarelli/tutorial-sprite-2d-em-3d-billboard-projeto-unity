using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDoJogador : MonoBehaviour
{
    [Header("Referências Gerais")]
    public Transform orientacao;
    public Rigidbody oRigidbody;

    [Header("Movimento")]
    public float velocidadeDeMovimento;

    public float dragDoChao;
    public float dragDoAr;

    public float alturaDoPulo;
    public float tempoDeEsperaDoPulo;
    public float multiplicadorDoAr;
    private bool podePular;

    [Header("Checagem de Chão")]
    public Transform localDoRaio;
    public float tamanhoDoRaio;
    public LayerMask layerDoChao;
    private bool estaNoChao;

    private float inputHorizontal;
    private float inputVertical;

    private Vector3 direcaoDoMovimento;

    // Start is called before the first frame update
    void Start()
    {
        // diz que o jogador já pode pular no início da partida
        ResetarPulo();
    }

    // Update is called once per frame
    void Update()
    {
        // verifica se o jogador esta no chão
        estaNoChao = Physics.Raycast(localDoRaio.position, Vector3.down, tamanhoDoRaio, layerDoChao);

        MovimentosRecebidos();
        ControlarVelocidade();

        // aplica o drag no chão/no ar
        if(estaNoChao == true)
        {
            oRigidbody.drag = dragDoChao;
        }
        else
        {
            oRigidbody.drag = dragDoAr;
        }
    }

    void FixedUpdate()
    {
        MoverJogador();
    }

    private void MovimentosRecebidos()
    {
        // recebe o input horizontal
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        // recebe o input vertival
        inputVertical = Input.GetAxisRaw("Vertical");

        // verifica se apertou o botão de pular e se está no chão
        if(Input.GetButton("Jump") && estaNoChao == true)
        {
            Pular();
            podePular = false;
            // roda o método 'ResetarPulo' depois do tempo que colocamos na variável 'tempoDeEsperaDoPulo'
            Invoke(nameof(ResetarPulo), tempoDeEsperaDoPulo);
        }
    }

    private void MoverJogador()
    {
        // calcula a direcao do movimento
        direcaoDoMovimento = orientacao.right * inputHorizontal + orientacao.forward * inputVertical;

        // verifica se o jogador está no chão
        if(estaNoChao == true)
        {
            // move o jogador no chão se basenado na sua velocidade
            oRigidbody.AddForce(direcaoDoMovimento * velocidadeDeMovimento, ForceMode.Force);
        }
        else
        {
            // move o jogador no ar se baseando na sua velocidade e multiplicador do ar
            oRigidbody.AddForce(direcaoDoMovimento * velocidadeDeMovimento * multiplicadorDoAr, ForceMode.Force);
        }
    }

    private void ControlarVelocidade()
    {
        // armazena a velocidade atual (não limitada) do Rigidbody
        Vector3 velocidadeAtual = new Vector3(oRigidbody.velocity.x, 0f, oRigidbody.velocity.z);

        // verifica se o jogador está acima da velocidade máxima
        if(velocidadeAtual.magnitude > velocidadeDeMovimento)
        {
            // limita a velocidade se necessário
            Vector3 velocidadeLimitada = velocidadeAtual.normalized * velocidadeDeMovimento;
            oRigidbody.velocity = new Vector3(velocidadeAtual.x, oRigidbody.velocity.y, velocidadeLimitada.z);
        }
    }

    private void Pular()
    {
        // reseta a velocidade do eixo Y (faz com que o jogador sempre pule a mesma altura)
        oRigidbody.velocity = new Vector3(oRigidbody.velocity.x, 0f, oRigidbody.velocity.z);

        // faz o jogador pular
        oRigidbody.AddForce(transform.up * alturaDoPulo, ForceMode.Impulse);
    }

    private void ResetarPulo()
    {
        // diz que o jogador pode pular novamente
        podePular = true;
    }
}
