using UnityEngine;

public class RaquetePong : MonoBehaviour
{
    [SerializeField] private float velocidade = 8f;
    [SerializeField] private KeyCode teclaCima = KeyCode.W;
    [SerializeField] private KeyCode teclaBaixo = KeyCode.S;
    [SerializeField] private float limiteY = 4f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float direcaoY = 0f;

        if (Input.GetKey(teclaCima)) direcaoY += 1f;
        if (Input.GetKey(teclaBaixo)) direcaoY -= 1f;

        rb.linearVelocity = new Vector2(0f, direcaoY * velocidade);

        float posicaoYTravada = Mathf.Clamp(transform.position.y, -limiteY, limiteY);
        transform.position = new Vector3(transform.position.x, posicaoYTravada, transform.position.z);
    }
}
