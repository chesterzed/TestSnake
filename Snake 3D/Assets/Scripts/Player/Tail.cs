using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] private Transform SnakeHead;
    [SerializeField] float _cubeEdgeSize;


    private List<Transform> snakeTiles = new List<Transform>();
    private List<float> positions = new List<float>();

    void Start()
    {
        positions.Add(SnakeHead.position.x);
        AddTile();
        AddTile();
    }

    void Update()
    {
        float distance = (SnakeHead.position.x - positions[0]);

        if (distance > _cubeEdgeSize)
        {
            float direction = SnakeHead.position.x - positions[0];

            positions.Insert(0, positions[0] + direction * _cubeEdgeSize);
            positions.RemoveAt(positions.Count);

            distance -= _cubeEdgeSize;
        }

        for (int i = 0; i < snakeTiles.Count; i++)
        {
            Vector3 before = new Vector3(positions[i + 1], SnakeHead.position.y, SnakeHead.position.z - _cubeEdgeSize * (i + 1));
            Vector3 after = new Vector3(positions[i], SnakeHead.position.y, SnakeHead.position.z - _cubeEdgeSize * i);
            snakeTiles[i].position = Vector3.Lerp(before, after, distance);
        }
    }

    public void AddTile()
    {
        Transform tile = Instantiate(SnakeHead, new Vector3(positions[positions.Count - 1], 0, 0), Quaternion.identity, transform);
        snakeTiles.Add(tile);
        positions.Add(tile.position.x);
    }
}
