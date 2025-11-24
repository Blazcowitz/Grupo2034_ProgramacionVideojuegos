using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemigo tocado por: " + collision.name + " | Tag: " + collision.tag);

        if (collision.CompareTag("PlayerProjectile"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage()
    {
        health--;
        Debug.Log("Enemigo recibió daño. Vida restante: " + health);

        if (health <= 0)
        {
            Debug.Log("¡Enemigo destruido por vida = 0!");
            Destroy(gameObject);
        }
    }
}
