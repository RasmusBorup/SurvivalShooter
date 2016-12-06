using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.5f;
    public float range = 100f;
	public float shotOrigin = 1;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.05f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && networkView.isMine)
        {
			networkView.RPC ("Shoot", RPCMode.All);
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime && networkView.isMine)
        {
            networkView.RPC ("DisableEffects", RPCMode.All);
        }
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting) 
        {
            int syncDamage = damagePerShot;
            float syncFireRate = timeBetweenBullets;
            
            stream.Serialize(ref syncDamage);
            stream.Serialize(ref syncFireRate);
        } 
        else 
        {
            int syncDamage = 0;
            float syncFireRate = 0;

            stream.Serialize(ref syncDamage);
            stream.Serialize(ref syncFireRate);

            damagePerShot = syncDamage;
            timeBetweenBullets = syncFireRate;
        }
    }

	[RPC]
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

	[RPC]
    void Shoot ()
    {
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

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
