using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GridManager : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;
    public static int score = 0;

    public TextMeshProUGUI scoreText;
    public static GridManager instance;
    public GameObject gameOverText;
    public static bool isGameOver = false;

    public static Transform[,] grid = new Transform[10, 20];

    void Awake()
    {
        instance = this;
    }

    public static bool Inside(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public static void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
    }

    public static void AddScore(int lines)
    {
        switch (lines)
        {
            case 1: score += 100; break;
            case 2: score += 300; break;
            case 3: score += 500; break;
            case 4: score += 800; break;
        }

        if (instance != null)
        {
            instance.scoreText.text = "Score: " + score;
        }
    }

    public static void MoveDown(int fromY)
    {
        for (int y = fromY; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        gameOverText.SetActive(true);

        StartCoroutine(Restart());
    }

    System.Collections.IEnumerator Restart()
    {
        yield return new WaitForSeconds(2f);

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}