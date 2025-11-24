using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 moveInput;
    private PlayerControls controls;

    // ?? Audio
    private AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioClip throwSound;
    public AudioClip damageSound;

    // ? Animator y SpriteRenderer
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Shoot.performed += ctx => Shoot();
    }

    void OnEnable() => controls.Gameplay.Enable();
    void OnDisable() => controls.Gameplay.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ? inicializar SpriteRenderer
    }

    void Update()
    {
        // ? Usar linearVelocity como recomienda Unity
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // ?? Sonido de correr en loop mientras se mueve
        if (isGrounded && Mathf.Abs(moveInput.x) > 0.1f)
        {
            if (!audioSource.isPlaying || audioSource.clip != runSound)
            {
                audioSource.clip = runSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip == runSound && audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }
        }

        // ? Actualizar parámetros del Animator
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        animator.SetBool("isGrounded", isGrounded);

        // ? Voltear sprite según dirección
        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }

        // Opcional: animación de caída
        bool isFalling = !isGrounded && rb.linearVelocity.y < -0.1f;
        animator.SetBool("isFalling", isFalling);
    }

    void Jump()
    {
        if (isGrounded)
        {
            audioSource.clip = jumpSound;
            audioSource.loop = false;
            audioSource.Play();

            animator.SetTrigger("Jump");

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(throwSound, 1f);
        // Aquí iría tu lógica de disparo
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(damageSound);
            // Aquí puedes restar vida al jugador
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
