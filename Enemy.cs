using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimiento vertical")]
    public float moveSpeed = 2f;
    public float moveRange = 3f;
    private Vector3 startPos;
    private int direction = 1;

    [Header("Disparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float shootCooldown = 0.5f; // tiempo mínimo entre disparos
    private float lastShootTime = -999f;

    [Header("Audio")]
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MoveUpDown();

        if (IsNearBottom() && Time.time > lastShootTime + shootCooldown)
        {
            ShootLeft();
            lastShootTime = Time.time;
        }
    }

    void MoveUpDown()
    {
        transform.Translate(Vector2.up * direction * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - startPos.y) >= moveRange)
        {
            direction *= -1;
        }
    }

    bool IsNearBottom()
    {
        return transform.position.y <= startPos.y - moveRange + 0.1f;
    }

    void ShootLeft()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Enemy: projectilePrefab o firePoint no están asignados.");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Collider2D projCol = projectile.GetComponent<Collider2D>();
        Collider2D[] enemyCols = GetComponentsInChildren<Collider2D>();
        foreach (var eCol in enemyCols)
        {
            if (projCol != null && eCol != null)
                Physics2D.IgnoreCollision(projCol, eCol);
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.linearVelocity = Vector2.left * projectileSpeed;
        }

        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound, 1f);
    }
}
