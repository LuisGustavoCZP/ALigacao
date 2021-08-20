using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteracao : MonoBehaviour
{
    public Jogador jogador = null;
    public Interativo interativo = null;
    public Text nomeText = null;
    public ScrollRect acaoView = null;

    public void Change(Interativo _interativo)
    {
        interativo = _interativo;
        
        if(interativo)
        {
            nomeText.text = $"{interativo.name}";

            HashSet<AcaoTransicao> transicoes = new HashSet<AcaoTransicao>();
            foreach (var acaoT in interativo.acoes)
            {
                if(acaoT.Condicao(jogador.personagem, interativo))
                {
                    acaoT.Executar(jogador.personagem, interativo);
                }
            }

            gameObject.SetActive(true);
        } 
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    //Se está rodando no editor, instrucao para o compilador
#if UNITY_EDITOR
    //Quando resetar as config, ele adiciona os componentes e configurações basicas
    private void Reset()
    {
        //Dizendo pro menu quem é o jogador dele (:
        jogador = GetComponentInParent<Jogador>();
    }
#endif
}
