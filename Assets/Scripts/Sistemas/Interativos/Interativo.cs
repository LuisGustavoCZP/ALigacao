using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AcaoTransicao
{
    public Condicao[] condicoes;
    public Acao acao;

    public bool Condicao (Personagem personagem, Interativo interativo)
    {
        foreach (var condicao in condicoes)
        {
            if (!condicao.SatisfazCondicao(personagem, interativo)) return false;
        }
        return true;
    }

    public void Executar (Personagem personagem, Interativo interativo)
    {
        foreach (var condicao in condicoes)
        {
            condicao.ExecutarCondicao(personagem, interativo);
            personagem.interagirAcao = acao;
        }
    }

    public AcaoTransicao(Acao _acao, params Condicao[] _condicoes)
    {
        acao = _acao;
        condicoes = _condicoes;
    }
}

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public abstract class Interativo : MonoBehaviour
{
    #region Componentes basicos
    [SerializeField, HideInInspector]
    internal new Transform transform = null;
    [SerializeField, HideInInspector]
    internal new Rigidbody rigidbody = null;
    #endregion

    public List<AcaoTransicao> acoes = new List<AcaoTransicao>();

    // Start is called before the first frame update
    internal virtual void Start()
    {
        
    }

#if UNITY_EDITOR
    internal virtual void Reset()
    {
        //Quando resetar as config, ele adiciona os componentes e configurações basicas
        transform = base.transform;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }
#endif
}
