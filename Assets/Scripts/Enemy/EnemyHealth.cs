using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

//	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
//	{
//		if (stream.isWriting) 
//		{
//			int syncHealth = currentHealth;
//			stream.Serialize(ref syncHealth);
//			Debug.Log("Syncing Health sending");
//		} 
//		else 
//		{
//			int syncHealth = 0;
//			stream.Serialize(ref syncHealth);
//			currentHealth = syncHealth;
//			Debug.Log("Syncing Health receiving");
//		}
//	}

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        RemoveBufferedInstantiate(gameObject.networkView.viewID);
        Destroy (gameObject, 2f);
    }

    //Done this way to minimize the bandwidth requirement. 
    [RPC]
    void RemoveBufferedInstantiate (NetworkViewID viewID) 
    {
        if (Network.isServer) 
        {
            Network.RemoveRPCs (viewID);
        } 
//        else 
//        {
//            networkView.RPC ("RemoveBufferedInstantiate", RPCMode.Server, viewID);
//        }
    }
}
