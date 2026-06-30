using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp = 100;

    private int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}