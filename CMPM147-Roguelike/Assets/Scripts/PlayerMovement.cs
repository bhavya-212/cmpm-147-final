using UnityEngine;
using TinyDungeon;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    DungeonGenerator generator;
    Vector2Int currentGridPos;
    float spacing = 1.5f;
    bool isMoving = false;

    void Start()
    {
        generator = FindObjectOfType<DungeonGenerator>();
        currentGridPos = Vector2Int.zero;
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) return; 
        Vector2Int dir = Vector2Int.zero;

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
            Vector2Int nextPos = currentGridPos + dir;
            if (IsConnected(currentGridPos, nextPos))
            {
                StartCoroutine(MoveTo(nextPos));
            }
        }
    }

    bool IsConnected(Vector2Int from, Vector2Int to)
    {
        foreach (var edge in generator.corridors)
        {
            if ((edge.Item1 == from && edge.Item2 == to) || (edge.Item2 == from && edge.Item1 == to))
                return true;
        }
        return false;
    }
    System.Collections.IEnumerator MoveTo(Vector2Int nextPos)
    {
        isMoving = true;
        Vector3 start = transform.position;
        Vector3 end = new Vector3(nextPos.x * spacing, nextPos.y * spacing, 0);
        float t = 0f;
        float speed = 2f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, end, Mathf.Min(t, 1f));
            yield return null;
        }

        currentGridPos = nextPos;
        isMoving = false;
    }
}
