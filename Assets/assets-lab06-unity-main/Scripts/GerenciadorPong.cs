using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GerenciadorPong : MonoBehaviour
{
    [Header("Configurações do Jogo")]
    [SerializeField] private int metaPontos = 5;
    [SerializeField] private TMP_Text textoPlacar;
    [SerializeField] private TMP_Text textoMensagem;
    [SerializeField] private ParticleSystem particulasCoracao;
    [SerializeField] private Transform[] pontosComemoracao; 

   
    private int pontosFelpudo = 0;
    private int pontosFofura = 0;
    private bool jogoFinalizado = false; 

    private void Start()
    {
        AtualizarInterface();
        if (textoMensagem != null) textoMensagem.text = "";
    }

    public void GolMarcado(string lado)
    {
        if (jogoFinalizado) return;

        if (lado == "Esquerda")
        {
            pontosFofura++;
        }
        else if (lado == "Direita")
        {
            pontosFelpudo++;
        }

        DispararEfeitosGol();
        AtualizarInterface();
        VerificarCondicaoVitoria();
    }

    private void VerificarCondicaoVitoria()
    {
        if (pontosFelpudo >= metaPontos)
        {
            jogoFinalizado = true;
            textoMensagem.text = "Felpudo Venceu!";
            ExecutarComemoracaoFinal();
        }
        else if (pontosFofura >= metaPontos)
        {
            jogoFinalizado = true;
            textoMensagem.text = "Fofura Venceu!";
            ExecutarComemoracaoFinal();
        }
    }

    private void ExecutarComemoracaoFinal()
    {
        if (particulasCoracao == null || pontosComemoracao == null) return;

        for (int i = 0; i < pontosComemoracao.Length; i++)
        {
            if (pontosComemoracao[i] != null)
            {
                Instantiate(particulasCoracao, pontosComemoracao[i].position, Quaternion.identity);
            }
        }
    }

    private void DispararEfeitosGol()
    {
        if (particulasCoracao != null)
        {
            particulasCoracao.Play();
        }
    }

    private void AtualizarInterface()
    {
        if (textoPlacar != null)
        {
            textoPlacar.text = pontosFelpudo + " x " + pontosFofura;
        }
    }

    public bool JogoEstaAtivo()
    {
        return !jogoFinalizado;
    }
}