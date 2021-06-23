using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    // The projectile that been shot from a Cannon.
    private Rigidbody thisRigidbody;
    public Vector3 thisDirection;
    public float thisSpeed;
    [SerializeField] private GameObject thisBloodEffect = null;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisRigidbody.AddForce(thisDirection * thisSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void OnCollisionEnter(Collision collision)
    {
        Boss aBoss = collision.gameObject.GetComponent<Boss>();

        //GameObject aHitObject = collision.gameObject;

        if (aBoss != null)
        {
            print("HitBoss!");

            aBoss.OnDamage(10f);

            Instantiate(thisBloodEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        /*else if (aHitObject != null)
        {
            Destroy(gameObject);
        }*/
    }
}
