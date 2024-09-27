using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float nockTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Rigidbody2D>(out var enemy))
            {
                enemy.isKinematic = false;
                var difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(nockTime);
        enemy.velocity = Vector2.zero;
        enemy.isKinematic = true;
    }
}
