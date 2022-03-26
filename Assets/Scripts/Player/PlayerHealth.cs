using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
    int healthIncrease = 10;
    float regenIncrease = 0.1f;
    float regenTimer;
    float regenAccumulator;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	Slider healthSlider;
	Image damageImage;
	Text healthText;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    void Awake ()
	{
		GameObject slider = GameObject.Find ("HealthSlider");
		healthSlider = slider.gameObject.GetComponent<Slider> ();
		
		GameObject image = GameObject.Find ("DamageImage");
        damageImage = image.GetComponent<Image> ();

		healthText = GameObject.Find ("HealthText").GetComponent<Text>();

		anim = GetComponent <Animator> ();
		playerAudio = GetComponentInChildren <AudioSource> ();
		playerMovement = GetComponentInChildren <PlayerMovement> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
        startingHealth = startingHealth + StateManager.HealthUpgrades * healthIncrease;
		currentHealth = startingHealth;
        regenTimer = 0;
        regenAccumulator = 0;
    }

	void Start()
	{
	}

    void Update ()
    {
        healthText.text = currentHealth + "/" + startingHealth;
        
        if (currentHealth < startingHealth) {
            regenTimer += Time.deltaTime;

            if (regenTimer >= 1 / (regenIncrease * StateManager.RegenUpgrades)) {
                currentHealth = System.Math.Min(currentHealth + 1, startingHealth);
                regenTimer = 0;
            }
        }

        if(damaged) {
            damageImage.color = flashColour;
        } else {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void TakeDamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
