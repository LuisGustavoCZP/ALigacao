using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base dos Eletronicos que tem tela, adicionar a um GameObject referente a um eletronico
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Eletronico : MonoBehaviour
{
    #region Componentes basicos
    [SerializeField, HideInInspector]
    private new Transform transform = null;
    [SerializeField, HideInInspector]
    private Animator animator = null;
    [SerializeField, HideInInspector]
    private new Rigidbody rigidbody = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    private void Reset()
    {
        //Quando resetar as config, ele adiciona os componentes e configurações basicas
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        transform = base.transform;
        rigidbody.isKinematic = true;
    }
#endif
}
