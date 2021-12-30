using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] GameObject ChunkPrefab;

    private GameObject[] ChunkHolder;
    private int chunkHolderSize = 10;
    private int chunkHolderIterator = 0;

    private string[] collider_names = {"UpCollider", "RightCollider", "DownCollider", "LeftCollider"};

    void Start()
    {
        ChunkHolder = new GameObject[chunkHolderSize];
        ChunkHolder[chunkHolderIterator] = GameObject.Find("Chunk");
    }

    public void GenerateChunk(int direction, GameObject chunk)
    {
        //0 is up, 1 is right, 2 is down, 3 is left
        int xDir = 0;
        int yDir = 0;

        int destroyWhat = 0;

        switch (direction)
        {
            case 0:
                xDir = 0;
                yDir = 1;
                destroyWhat = 2;
                break;
            case 1:
                xDir = 1;
                yDir = 0;
                destroyWhat = 3;
                break;
            case 2:
                xDir = 0;
                yDir = -1;
                destroyWhat = 0;
                break;
            case 3:
                xDir = -1;
                yDir = 0;
                destroyWhat = 1;
                break;
        }

        RaycastHit hit;

        //Check if chunk to be generated is already there
        if(Physics2D.CircleCast(new Vector2(chunk.transform.position.x + 180 * xDir, chunk.transform.position.y + 145 * yDir), 60, transform.forward, 0, LayerMask.GetMask("Obstacle"), -Mathf.Infinity, Mathf.Infinity))
        {
            return;
        }

        //Wrap iterator
        if (++chunkHolderIterator >= chunkHolderSize)
            chunkHolderIterator = 0;

        ChunkHolder[chunkHolderIterator] = Instantiate(ChunkPrefab, new Vector3(chunk.transform.position.x + 180 * xDir, chunk.transform.position.y + 145 * yDir, 5), Quaternion.identity);
        Destroy(ChunkHolder[chunkHolderIterator].transform.Find(collider_names[destroyWhat]).gameObject);
    }
}
