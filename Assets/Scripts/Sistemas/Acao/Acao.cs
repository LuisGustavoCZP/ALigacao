using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public abstract class Acao : ScriptableObject
{
    public string animation = string.Empty;
    public abstract IEnumerator Agir(Personagem personagem, Interativo interativo);
    /*{
        while (interativo == personagem.interagirAlvo)
        {

            yield return null;
        }
    }*/
}
