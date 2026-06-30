using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public Tower selectedTower;

    public void UpgradeTower()
    {
        if (selectedTower == null)
            return;

        selectedTower.Upgrade();
    }
}