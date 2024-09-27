using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider Found!");
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Debug.Log("Enemy hit!");
        }
    }
}
