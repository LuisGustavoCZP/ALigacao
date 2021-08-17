using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInterativo : MonoBehaviour
{
    public Interativo interativo = null;
    public Text nomeText = null;
    public RectTransform acaoContainer = null; 


    public void Change(Interativo _interativo)
    {
        interativo = _interativo;
        
        if(_interativo)
        {
            nomeText.text = $"{interativo.name}";

            foreach (var acaoT in interativo.acoes)
            {

            }
            gameObject.SetActive(true);
        } 
        else
        {
            gameObject.SetActive(false);
        }
        
    }

}
