using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] RoadBlocks;
    public GameObject StartBlock;

    public GameObject[] Barriers;
    
    float blockXPos = 0;
    int blocksCount = 7;
    int barriersCount = 5;
    float blockLength = 30;

    int safeZone = 50;

    float[] positionZ = {-1.5f, 0, 1.5f };

    public Transform PlayerTransform;

    public Vector3 startPlayerPos;

    List<GameObject> CurrentBlocks = new List<GameObject>();
    List<GameObject> CurrentBarriers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        blockXPos = StartBlock.transform.position.x;
        blockLength = 30;// StartBlock.GetComponent<BoxCollider>().bounds.size.x;
        startPlayerPos = PlayerTransform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSpawn();
    }

    public void StartGame()
    {
        blockXPos -= blocksCount * blockLength;

        foreach (var block in CurrentBlocks)
        {
            DestroyImmediate(block);
        }

        CurrentBlocks.Clear();

        foreach (var barrier in CurrentBarriers)
        {
            if(barrier != null)
            DestroyImmediate(barrier);
        }

        CurrentBarriers.Clear();

        for (int i = 0; i < blocksCount; i++)
        {
            SpawnBlock();
        }

        PlayerTransform.position = startPlayerPos;
    }

    void CheckForSpawn()
    {
        if(PlayerTransform.position.x - safeZone > (blockXPos - blocksCount * blockLength))
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    void SpawnBlock()
    {
        GameObject block = Instantiate(RoadBlocks[Random.Range(0, RoadBlocks.Length)], transform);

        blockXPos += blockLength;

        block.transform.position = new Vector3(blockXPos, 0, 0);

        SpawnBarriers();
        CurrentBlocks.Add(block);
    }

    void DestroyBlock()
    {
        DestroyBarriers();
        Destroy(CurrentBlocks[0]);
        CurrentBlocks.RemoveAt(0);
    }

    void SpawnBarriers()
    {
        int deltaX = 3;
        int numBarriers = Random.Range(0, barriersCount);
        for(int i = 0; i < numBarriers; i++)
        {
            GameObject barrier = Instantiate(Barriers[Random.Range(0, Barriers.Length)], transform);
            int lineNumber = Random.Range(0, 3);

            barrier.transform.position = new Vector3(blockXPos + (i+1)*deltaX, 1, positionZ[lineNumber]);
            CurrentBarriers.Add(barrier);
        }
    }

    void DestroyBarriers()
    {
        while(true)
        {
            if (CurrentBarriers[0] != null)
            {
                if (CurrentBarriers[0].transform.position.x < blockXPos - blocksCount * blockLength)
                {
                    Destroy(CurrentBarriers[0]);
                }
                else
                {
                    break;
                }
            }
            CurrentBarriers.RemoveAt(0);
        }
    }
}
