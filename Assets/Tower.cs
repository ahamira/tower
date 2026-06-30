using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Level 1")]
    public float range = 5f;
    public float fireRate = 1f;
    public int damage = 10;

    public int level = 1;
    public int upgradeCost = 100;

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            Attack();
            fireTimer = 0;
        }
    }

    void Attack()
    {
       
    }

    public void Upgrade()
    {
        level++;

        range += 1f;
        damage += 5;
        fireRate += 0.5f;

        upgradeCost += 100;

        Debug.Log($"Tower Lv.{level}");
    }
}