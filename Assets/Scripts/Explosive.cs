using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    Collider2D[] inExplosionRadious = null;

    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private float explosionForceMulti;

    public void Explosion() {
        inExplosionRadious = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in inExplosionRadious) {
            
            Rigidbody2D o_rigiBody = o.GetComponent<Rigidbody2D>();
            if (o_rigiBody != null && o.CompareTag("Player")) {
                Vector2 distanceVector = o.transform.position - transform.position;
                Vector2 forceVector;

                if ( distanceVector.magnitude > 0) {
                    forceVector = new Vector2 (1, 1);
                }
                if (distanceVector.magnitude < 0)
                {
                    forceVector = new Vector2(1, -1);
                }
                else {
                    forceVector = new Vector2(1, 0);
                }
                if (distanceVector.magnitude > 0) {
                    float explosionForce = explosionForceMulti / distanceVector.magnitude;
                    Debug.Log(explosionForce);
                    o_rigiBody.AddForce(forceVector * explosionForce);
                    //o_rigiBody.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
