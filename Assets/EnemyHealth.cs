using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]
    public int health = 5;

    [Header("Sprite de derrota")]
    public Sprite defeatedSprite; // sprite cuando muere
    private SpriteRenderer spriteRenderer;
    private bool isDefeated = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        if (isDefeated) return;

        health--;
        Debug.Log("Enemigo recibió daño. Vida restante: " + health);

        if (health <= 0)
        {
            Debug.Log("¡Enemigo derrotado!");
            Defeat();
        }
    }

    void Defeat()
    {
        isDefeated = true;

        // Cambiar sprite
        if (spriteRenderer != null && defeatedSprite != null)
        {
            spriteRenderer.sprite = defeatedSprite;
        }

        // Desactivar colisión
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Avisar al script Enemy para detener movimiento/disparo
        Enemy enemyScript = GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.isDefeated = true;
        }

        // Opcional: destruir después de unos segundos
        // Destroy(gameObject, 2f);
    }
}
