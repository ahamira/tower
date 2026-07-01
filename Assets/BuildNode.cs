using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower;

    private void OnMouseDown()
    {
        if (tower != null) return;

        tower = Instantiate(
            towerPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}