using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
    [SerializeField]
    public Tilemap map;
    [SerializeField]
    public Tile tile;
    public int[] block1;
    public int[] block2;
    public int[][] blocks;
    public int prev = 0;
    public int index = Random.Range(0, 2);
    // Start is called before the first frame update
    void Start()
    {
        string s3 = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
        int[] block3 = System.Array.ConvertAll(s3.Split(','), int.Parse);
        string s4 = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
        int[] block4 = System.Array.ConvertAll(s4.Split(','), int.Parse);

        block1 = new int[200];
        for (int i = 0; i < 20; i++)
        {
            block1[120 + i] = 1;

        }
            block2 = new int[200];
            for (int i = 0; i < 20; i++)
            {
                block2[100 + i] = 1;
            }
        blocks = new int[4][];
        blocks[0] = block1;
        blocks[1] = block2;
        blocks[2] = block3;
        blocks[3] = block4;
    }

        // Update is called once per frame
        void Update()
        {
            Vector3Int cam = (map.WorldToCell(transform.position));
            if ((cam.x) % 20 == 0 && prev != 0)
            {
                index = Random.Range(0, 4);
                print(index);
                for (int i = 0; i < 200; i++)
                {
                    if (blocks[index][i] == 1)
                    {
                        map.SetTile(cam + new Vector3Int(10 + (i % 20), 3 - (i / 20), 0), tile);
                    }

                }
            }
            prev = (cam.x) % 20;
            //print(map.WorldToCell(transform.position));

        }
    }

