using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public Personagem personagem = null;
    public Transform moverTarget = null;
    public Transform olharTarget = null;

    void OnEnable()
    {
        personagem.moverMira = moverTarget;
        personagem.olharMira = olharTarget;
    }

    void OnDisable()
    {
        personagem.moverMira = null;
        personagem.olharMira = null;
    }

    // Update is called once per frame
    void Update()
    {
        var moverDir = personagem.transform.position + new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")).normalized;
        var olharDir = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0).normalized;
    }
}
