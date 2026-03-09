using UnityEngine;
using TinyDungeon;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    DungeonGenerator generator;
    Vector2Int currentGridPos;
    float spacing = 1.5f;
    bool inputLocked = false;
    void Start()
    {
        generator = FindObjectOfType<DungeonGenerator>();
        currentGridPos = Vector2Int.zero;
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int dir = Vector2Int.zero;

        if (!inputLocked)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                dir = Vector2Int.up;

            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                dir = Vector2Int.down;

            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                dir = Vector2Int.left;

            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                dir = Vector2Int.right;

            if (dir != Vector2Int.zero)
            {
                TryMove(dir);
                inputLocked = true;
            }
        }

        // unlock input when keys are released
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            inputLocked = false;
        }
    }
    void TryMove(Vector2Int dir)
    {
        Vector2Int nextPos = currentGridPos + dir;

        foreach (var edge in generator.corridors)
        {
            if ((edge.Item1 == currentGridPos && edge.Item2 == nextPos) ||
                (edge.Item2 == currentGridPos && edge.Item1 == nextPos))
            {
                currentGridPos = nextPos;
                transform.position = new Vector3(nextPos.x * spacing, nextPos.y * spacing, 0);
                return;
            }
        }
    }
}
