using UnityEngine;

public class C4 : MonoBehaviour
{
    public float MaxDamage = 100;
    public float maxForce = 10;
    public float explosionRadius;
    public float TimeDelay = 2f;
    public ParticleSystem effect;

    private SphereCollider c4Collider;
    private float timer;

    private void Awake()
    {
        c4Collider = GetComponent<SphereCollider>();
        explosionRadius = c4Collider.radius;
    }

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > TimeDelay)
        {
            c4Collider.enabled = true;
            effect.gameObject.SetActive(true);
        }

        if (timer > TimeDelay + 0.2f)
        {
            c4Collider.enabled = false;

            if (effect && !effect.IsAlive())
            {
                effect.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode explode = other.gameObject.GetComponent<Explode>();

        if (explode == null) return;

        float distance = Vector3.Distance(transform.position, other.transform.position);
        float damage = Mathf.Clamp(MaxDamage * (1 - distance / explosionRadius), 0, MaxDamage);

        explode.GetComponent<IHP>().HP -= damage;
        explode.Explosion(transform.position, explosionRadius, maxForce);
    }
}
