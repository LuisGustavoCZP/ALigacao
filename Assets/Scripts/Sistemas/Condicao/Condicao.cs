using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condicao : ScriptableObject
{
    public abstract bool SatisfazCondicao(Personagem personagem, Interativo interativo);
    public abstract void ExecutarCondicao(Personagem personagem, Interativo interativo);
}
