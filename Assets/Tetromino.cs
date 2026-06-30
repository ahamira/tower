using UnityEngine;
using UnityEngine.InputSystem;

public class Tetromino : MonoBehaviour
{
    public float fallSpeed = 0.8f;
    float fallTimer;

    void Start()
    {
        fallTimer = Time.time;
    }

    void Update()
    {
        HandleInput();
        HandleFall();
    }

    void HandleInput()
    {
        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            transform.position += Vector3.left;

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            transform.position += Vector3.right;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            RotateWithKick();

        if (!IsValid())
        {
            transform.position = oldPos;
            transform.rotation = oldRot;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            HardDrop();
    }

    void HandleFall()
    {
        float speed = Keyboard.current.downArrowKey.isPressed
            ? 0.05f
            : fallSpeed;

        if (Time.time >= fallTimer + speed)
        {
            fallTimer = Time.time;

            transform.position += Vector3.down;

            if (!IsValid())
            {
                transform.position += Vector3.up;
                Lock();
            }
        }
    }
    void RotateWithKick()
    {
        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;

        transform.Rotate(0, 0, 90);

        if (IsValid()) return;

        Vector3[] kicks = {
            Vector3.right,
            Vector3.left,
            Vector3.right * 2,
            Vector3.left * 2,
            Vector3.up
        };

        foreach (var k in kicks)
        {
            transform.position = oldPos + k;

            if (IsValid()) return;
        }

        transform.rotation = oldRot;
        transform.position = oldPos;
    }

    void HardDrop()
    {
        while (IsValid())
        {
            transform.position += Vector3.down;
        }

        transform.position += Vector3.up;
        Lock();
    }


    void Lock()
    {
        Transform[] blocks = GetComponentsInChildren<Transform>();

        foreach (Transform block in blocks)
        {
            if (block == transform)
                continue;

            int x = Mathf.RoundToInt(block.position.x);
            int y = Mathf.RoundToInt(block.position.y);

            GridManager.grid[x, y] = block;
            block.SetParent(null);
        }

        Spawner spawner = FindFirstObjectByType<Spawner>();

        if (spawner != null)
        {
            spawner.CheckLines();
            spawner.SpawnNext();
        }

        Destroy(gameObject);
    }

    bool IsValid()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            if (x < 0 || x >= GridManager.width ||
                y < 0 || y >= GridManager.height)
                return false;

            if (y < GridManager.height && GridManager.grid[x, y] != null)
                return false;
        }
        return true;
    }
}
