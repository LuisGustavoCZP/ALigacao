using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base dos Eletronicos, adicionar a um GameObject referente a um eletronico
/// </summary>
[RequireComponent(typeof(Animator))]
public class Eletronico : Interativo
{
    #region Componentes basicos
    [SerializeField, HideInInspector]
    internal Animator animator = null;
    #endregion

    public Transform olharPivo = null;
    public Transform moverPivo = null;

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    internal override void Reset()
    {
        //Quando resetar as config, ele adiciona os componentes e configurações basicas
        animator = GetComponent<Animator>();
    }
#endif
}
