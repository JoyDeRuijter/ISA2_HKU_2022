// Written by Joy de Ruijter
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform vfxHitHuman;
    [SerializeField] private Transform vfxHitTerrain;

    private Rigidbody bulletRB;

    #endregion

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 30f;
        bulletRB.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            // Hit target
            Instantiate(vfxHitHuman, transform.position, Quaternion.identity);
        }
        else
        {
            // Hit something else
            Instantiate(vfxHitTerrain, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }
}
