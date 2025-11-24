using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public AudioClip throwSound;

    private PlayerControls controls;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Shoot.performed += ctx => Shoot();
    }

    void OnEnable()
    {
        if (controls != null)
            controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        if (controls != null)
            controls.Gameplay.Disable();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Shoot()
    {
        if (throwSound != null && audioSource != null)
            audioSource.PlayOneShot(throwSound);

        if (projectilePrefab == null) return;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();

        float direction = spriteRenderer != null && spriteRenderer.flipX ? -1f : 1f;

        if (rbProjectile != null)
            rbProjectile.linearVelocity = new Vector2(direction * projectileSpeed, 0f);

        projectile.transform.localScale = new Vector3(direction, 1f, 1f);
    }
}
