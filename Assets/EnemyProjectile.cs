using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // se destruye solo por tiempo
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Proyectil enemigo tocó: " + collision.name + " | Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
            Destroy(gameObject); // solo se destruye si golpea al jugador
        }
        else if (collision.CompareTag("Enemy"))
        {
            // nunca dañar al enemigo
            Destroy(gameObject);
        }

        // ? No hay condición para Obstacle ni Ground
        // El proyectil simplemente los atraviesa
    }
}
