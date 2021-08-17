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
    /// Alvo para qual o jogador est� olhando
    /// </summary>
    public Transform olharAlvo = null;
    /// <summary>
    /// Alvo para qual o jogador est� movendo
    /// </summary>
    public Transform moverAlvo = null;
    /// <summary>
    /// Alvo que o jogador est� interagindo
    /// </summary>
    public Interativo interagirAlvo = null;
    public Acao interagirAcao = null;
    //public bool interagindo = false;

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
        var camT = cabecaCamera.transform;
        var camPos = camT.position;

       
        //Mover o agente na velocidade moverSpeed na direcao da mira, relativo a rotacao da cabeca
        if (moverAlvo) navAgent.velocity = (moverAlvo.position - camPos).normalized * (moverSpeed);

        if(!interagirAcao && interagirAlvo)
        {
            StartCoroutine(InteracaoCoroutine());
        }
    }

    IEnumerator InteracaoCoroutine()
    {
        var interativo = interagirAlvo;
        yield return interagirAcao.Agir(this, interativo);
        interagirAcao = null;
    }

    void LateUpdate()
    {
        //Rotacionar a cabe�a na direcao e velocidade desejadas
        var deltaT = Time.smoothDeltaTime;
        var camT = cabecaCamera.transform;
        var camPos = camT.position;

        if (olharAlvo)
        {
            Vector3 olharDir = (olharAlvo.position - camPos).normalized,
                olharHorizontal = transform.TransformDirection(olharDir),
                olharVertical = transform.TransformDirection(olharDir);

            olharHorizontal.y = 0;
            //olharVertical.x = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.InverseTransformDirection(olharHorizontal)), deltaT * olharSpeed);

            cabeca.rotation = Quaternion.Lerp(cabeca.rotation, Quaternion.LookRotation(transform.InverseTransformDirection(olharVertical)), deltaT * olharSpeed);
        }
    }

#if UNITY_EDITOR
        private void Reset()
    {
        //Quando resetar as config, ele adiciona os componentes e configura��es basicas
        navAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        m_cabecaCamera = GetComponentInChildren<Camera>();
        m_cabeca = m_cabecaCamera.transform.parent;
        transform = base.transform;
        rigidbody.isKinematic = true;
        navAgent.updateRotation = false;
    }
#endif
}
