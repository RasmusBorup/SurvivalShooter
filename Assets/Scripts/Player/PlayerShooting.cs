using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.5f;
    public float range = 100f;
	public float shotOrigin = 1;
    public int magazineSize = 5;
    public float reloadTime = 5f;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.05f;
    bool isReloading;
    int bulletsLeftInMagazine;
    public float reloadTimer;
	Slider reloadSlider;
    Text ammoText;
    ShopController shopController;

    void Awake ()
    {
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        reloadSlider = GameObject.Find("ReloadSlider").GetComponent<Slider>();
        reloadSlider.gameObject.SetActive(false);
        shootableMask = LayerMask.GetMask ("Shootable");
        shopController = GameObject.Find("Shopkeeper").GetComponent<ShopController>();
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        
        shootRay = new Ray();
        isReloading = false;
        bulletsLeftInMagazine = magazineSize;
        UpdateAmmoCounter();
        reloadTimer = 0;
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
			Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime) {
            DisableEffects();
        }

        if (isReloading || Input.GetKeyDown("r")) {
            reload();
        }
    }

    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot ()
    {
        if (isReloading || shopController.shopOpen) {
            return;
        }

        timer = 0f;

        gunAudio.Play ();
        gunLight.enabled = true;
        gunParticles.Stop ();
        gunParticles.Play ();
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position - transform.forward.normalized * shotOrigin;
		// - transform.forward.normalized * shotOrigin as the shot would start inside the collider of
		//an enemy who was standing right by you.
		shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
            gunLine.SetPosition (1, shootHit.point);
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            if(enemyHealth != null) {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
        } else {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }

        bulletsLeftInMagazine--;
        UpdateAmmoCounter();

        if (bulletsLeftInMagazine == 0) {
            reload();

            return;
        }
    }

    void reload()
    {
        if (bulletsLeftInMagazine == magazineSize) {
            return;
        }

        reloadTimer += Time.deltaTime;
        reloadSlider.value = reloadTimer / reloadTime * 100;

        if (isReloading && reloadTimer < reloadTime) {
            return;
        }

        if (isReloading && reloadTimer >= reloadTime) {
            reloadTimer = 0;
            bulletsLeftInMagazine = magazineSize;
            isReloading = false;
            reloadSlider.gameObject.SetActive(false);
            ammoText.text = bulletsLeftInMagazine + "/" + magazineSize;

            return;
        }

        isReloading = true;
        reloadSlider.gameObject.SetActive(true);
        bulletsLeftInMagazine = 0;
        UpdateAmmoCounter();
    }

    public void UpdateAmmoCounter()
    {
        ammoText.text = bulletsLeftInMagazine + "/" + magazineSize;
    }
}
