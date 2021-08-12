using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Classe do Jogador, adicionar a um GameObject referente ao jogador
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
public class Personagem : MonoBehaviour
{
    #region Cabeca
    /// <summary>
    /// Transform da cabeca do jogador (privada)
    /// </summary>
    [SerializeField, HideInInspector]
    private Transform m_cabeca = null;
    /// <summary>
    /// Camera na cabeca do jogador (privada)
    /// </summary>
    [SerializeField, HideInInspector]
    private Camera m_cabecaCamera = null;
    /// <summary>
    /// Transform da cabeca do jogador
    /// </summary>
    public Transform cabeca
    {
        get
        {
            return m_cabeca;
        }
    }
    /// <summary>
    /// Camera na cabeca do jogador
    /// </summary>
    public Camera cabecaCamera
    {
        get
        {
            return m_cabecaCamera;
        }
    }
    #endregion

    #region Configuracoes
    /// <summary>
    /// Velocidade que o jogador olha para outra direcao
    /// </summary>
    public float olharSpeed = 30;
    /// <summary>
    /// Velocidade que o jogador move para outra direcao
    /// </summary>
    public float moverSpeed = 5;
    #endregion

    #region Acoes
    /// <summary>
    /// Mira para qual o jogador está olhando
    /// </summary>
    public Transform olharMira = null;
    /// <summary>
    /// Mira para qual o jogador está movendo
    /// </summary>
    public Transform moverMira = null;
    /// <summary>
    /// Mira para qual o jogador está movendo
    /// </summary>
    public bool interagir = false;
    #endregion

    #region Componentes basicos
    [SerializeField, HideInInspector]
    private new Transform transform = null;
    [SerializeField, HideInInspector]
    private NavMeshAgent navAgent = null;
    [SerializeField, HideInInspector]
    private new Rigidbody rigidbody = null;
    #endregion

    // Update chamado todo frame
    void Update()
    {
        //Tempo medio entre frames
        var deltaT = Time.smoothDeltaTime;
        var camPos = cabecaCamera.transform.position;
        //Rotacionar a cabeça na direcao e velocidade desejadas
        if(olharMira) cabeca.rotation = Quaternion.Lerp(cabeca.rotation, Quaternion.LookRotation(olharMira.position - camPos), deltaT * olharSpeed);
        //Mover o agente na velocidade moverSpeed na direcao da mira, relativo a rotacao da cabeca
        if(moverMira) navAgent.velocity = (moverMira.position - camPos).normalized * (moverSpeed);
    }

#if UNITY_EDITOR
    private void Reset()
    {
        //Quando resetar as config, ele adiciona os componentes e configurações basicas
        navAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        m_cabecaCamera = GetComponentInChildren<Camera>();
        m_cabeca = m_cabecaCamera.transform.parent;

        rigidbody.isKinematic = true;
        navAgent.updateRotation = false;
    }
#endif
}
