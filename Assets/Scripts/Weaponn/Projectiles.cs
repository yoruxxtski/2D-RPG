using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    public Vector3 direction {get ; set;}
    public float  damage { get; set; }

    void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    } 

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamageable>()?.TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
