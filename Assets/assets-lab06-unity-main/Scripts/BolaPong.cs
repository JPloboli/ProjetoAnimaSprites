using UnityEngine;

public class BolaPong : MonoBehaviour
{
    [Header("Configurações de Velocidade")]
    [SerializeField] private float velocidadeInicial = 7f;
    [SerializeField] private float incrementoVelocidade = 0.5f;

    [Header("Efeitos")]
    [SerializeField] private ParticleSystem particulaCoracao;

    private Rigidbody2D rb;
    private Vector3 posicaoInicial;
    private GerenciadorPong gerenciador;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position;
    }

    private void Start()
    {
        gerenciador = FindObjectOfType<GerenciadorPong>();
        LancarBola();
    }

    public void LancarBola()
    {
        transform.position = posicaoInicial;
        rb.linearVelocity = Vector2.zero;

        float direcaoX = Random.value < 0.5f ? -1f : 1f;
        float direcaoY = Random.Range(-0.5f, 0.5f);

        Vector2 direcao = new Vector2(direcaoX, direcaoY).normalized;
        rb.linearVelocity = direcao * velocidadeInicial;
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Player"))
        {
            float diferencaY = transform.position.y - colisao.transform.position.y;
            Vector2 novaDirecao = new Vector2(rb.linearVelocity.x > 0 ? 1 : -1, diferencaY).normalized;

            rb.linearVelocity = novaDirecao * (rb.linearVelocity.magnitude + incrementoVelocidade);
        }
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Gol"))
        {
            if (particulaCoracao != null)
            {
                particulaCoracao.transform.position = transform.position;
                particulaCoracao.Play();
            }

            if (gerenciador != null)
            {
                if (outro.name.Contains("Esquerda"))
                {
                    gerenciador.GolMarcado("Esquerda");
                }
                else
                {
                    gerenciador.GolMarcado("Direita");
                }

                if (gerenciador.JogoEstaAtivo())
                {
                    Invoke(nameof(LancarBola), 1f);
                }
            }
        }
    }
}