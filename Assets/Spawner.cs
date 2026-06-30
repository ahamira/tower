using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] pieces;

    private List<GameObject> bag = new List<GameObject>();

    void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (GridManager.isGameOver)
            return;

        if (bag.Count == 0)
            FillBag();

        GameObject piece = bag[0];
        bag.RemoveAt(0);

        GameObject obj = Instantiate(piece, transform.position, Quaternion.identity);
      
        if (IsGameOver(obj))
        {

            GridManager.instance.GameOver();
            return;
        }
    }
    void FillBag()
    {
        List<GameObject> temp = new List<GameObject>(pieces);

        while (temp.Count > 0)
        {
            int i = Random.Range(0, temp.Count);
            bag.Add(temp[i]);
            temp.RemoveAt(i);
        }
    }

    public void CheckLines()
    {
        int linesCleared = 0;

        for (int y = 0; y < GridManager.height; y++)
        {
            if (IsFull(y))
            {
                GridManager.DeleteLine(y);
                GridManager.MoveDown(y + 1);
                y--;
                linesCleared++;
            }
        }

        if (linesCleared > 0)
        {
            GridManager.AddScore(linesCleared);
        }
    }

    bool IsFull(int y)
    {
        for (int x = 0; x < GridManager.width; x++)
        {
            if (GridManager.grid[x, y] == null)
                return false;
        }
        return true;
    }
    bool IsGameOver(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            if (x < 0 || x >= GridManager.width ||
                y < 0 || y >= GridManager.height)
            {
                continue;
            }

            if (GridManager.grid[x, y] != null)
                return true;
        }
        return false;
    }
}
