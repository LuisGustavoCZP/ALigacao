using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    public Personagem personagem = null;
    public Transform moverTarget = null;
    public Transform olharTarget = null;
    
    public float olharSensitive = 15;
    private Vector3 olharDirecao = Vector3.forward;

    public Text interativoText = null;
    public float interativoDistancia = 5;
    public LayerMask interativosMask = new LayerMask();

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        personagem.moverMira = moverTarget;
        personagem.olharMira = olharTarget;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        personagem.moverMira = null;
        personagem.olharMira = null;
    }

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
                var eletro = hit.rigidbody.GetComponent<Eletronico>();
                if (eletro)
                {
                    interativoText.text = $"{eletro.name}";
                }
            } 
            else
            {
                var eletro = hit.collider.GetComponentInParent<Eletronico>();
                if (eletro)
                {
                    interativoText.text = $"{eletro.name}";
                }
            }
        }
        else
        {
            interativoText.text = string.Empty;
        }
    }

}
