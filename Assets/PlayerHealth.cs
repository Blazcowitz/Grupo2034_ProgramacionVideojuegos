using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxLives = 3;
    private int currentLives;

    [Header("UI")]
    public Slider healthBar;         // Asigna el Slider en el Inspector
    public Image fillImage;          // Asigna el objeto Fill del Slider

    [Header("Audio")]
    public AudioClip damageSound;    // ?? Clip de daño (asignar en Inspector)
    private AudioSource audioSource; // ?? Componente de audio

    void Start()
    {
        currentLives = maxLives;

        if (healthBar != null)
        {
            healthBar.maxValue = maxLives;
            healthBar.value = currentLives;
        }

        UpdateFillColor();

        // Inicializar AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        currentLives = Mathf.Max(currentLives - 1, 0);

        if (healthBar != null)
        {
            healthBar.value = currentLives;
        }

        UpdateFillColor();

        // ?? Reproducir sonido de daño cada vez que se recibe daño
        if (damageSound != null)
        {
            // Usamos PlayClipAtPoint para asegurar que siempre suene
            AudioSource.PlayClipAtPoint(damageSound, transform.position, 1f);
        }

        if (currentLives <= 0)
        {
            Die();
        }
    }

    void UpdateFillColor()
    {
        if (fillImage == null) return;

        float percent = (float)currentLives / maxLives;

        if (percent > 0.5f)
            fillImage.color = Color.green;
        else if (percent > 0.2f)
            fillImage.color = new Color(1f, 0.65f, 0f); // naranja
        else
            fillImage.color = Color.red;
    }

    void Die()
    {
        Debug.Log("¡Game Over!");

        // ?? Sonido final de daño antes de reiniciar
        if (damageSound != null)
            AudioSource.PlayClipAtPoint(damageSound, transform.position, 1f);

        // Retraso para que el sonido se escuche antes de reiniciar
        Invoke(nameof(ReloadScene), 0.5f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    public int GetLives()
    {
        return currentLives;
    }
}
