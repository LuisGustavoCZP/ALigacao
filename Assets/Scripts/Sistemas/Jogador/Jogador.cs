using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Jogador que controla o Personagem principal, ou qualquer outro que o jogo permitir
/// </summary>
public class Jogador : MonoBehaviour
{
    /// <summary>
    /// O personagem que é controlado pelo Jogador
    /// </summary>
    public Personagem personagem = null;
    /// <summary>
    /// O alvo a ser controlado pelo Jogador, ao qual o personagem se move em encontro
    /// </summary>
    public Transform moverTarget = null;
    /// <summary>
    /// O alvo a ser controlado pelo Jogador, ao qual o personagem se vira em direcao
    /// </summary>
    public Transform olharTarget = null;
    /// <summary>
    /// Direcao que o personagem está olhando agora, uso interno
    /// </summary>
    private Vector3 olharDirecao = Vector3.forward;
    /// <summary>
    /// Menu do Jogador mostrando a interacao do Personagem
    /// </summary>
    public UIInterativo uiInterativo = null;
    /// <summary>
    /// Distancia limite para a interacao do Personagem
    /// </summary>
    public float interativoDistancia = 5;
    /// <summary>
    /// Layers possiveis para a interacao do Personagem
    /// </summary>
    public LayerMask interativosMask = new LayerMask();

    /// <summary>
    /// Analisa o objeto interativo e mostra as acões existentes
    /// </summary>
    /// <param name="interativo"></param>
    void OlharAlvo(Interativo interativo)
    {
        personagem.interagirAlvo = interativo;
        uiInterativo.Change(interativo);
    }

    /// <summary>
    /// Quando o script for iniciado
    /// </summary>
    private void Start()
    {
        
    }

    /// <summary>
    /// Quando o script do Jogador for ativado, como forma de ligar o jogador tambem pelas Animacoes do Unity
    /// </summary>
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        personagem.moverAlvo = moverTarget;
        personagem.olharAlvo = olharTarget;
    }

    /// <summary>
    /// Quando o script do Jogador for desativado, como forma de desligar o jogador tambem pelas Animacoes do Unity
    /// </summary>
    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        personagem.moverAlvo = null;
        personagem.olharAlvo = null;
    }

    /// <summary>
    /// Atualiza uma vez a cada frame
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { enabled = false; return; }
        var moverDir = personagem.transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        moverTarget.position = personagem.transform.position + moverDir;

        var camT = personagem.cabecaCamera.transform;
        var camPos = camT.position;
        var olharDir = personagem.cabeca.TransformDirection(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0));
        olharDirecao += olharDir * Time.deltaTime;
        olharDirecao.Normalize();
        olharTarget.position = camPos + olharDirecao;

        if(Physics.Raycast(camPos, camT.forward, out RaycastHit hit, interativoDistancia, interativosMask))
        {
            if (hit.rigidbody)
            {
                OlharAlvo(hit.rigidbody.GetComponent<Interativo>());
            } 
            else
            {
                OlharAlvo(hit.collider.GetComponentInParent<Interativo>());
            }
        }
        else
        {
            OlharAlvo(null);
        }
    }

}
