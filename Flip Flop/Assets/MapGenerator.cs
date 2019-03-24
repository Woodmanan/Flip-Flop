using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    Tilemap map;

    [SerializeField]
    Tile beginning;
    [SerializeField]
    Tile middle;
    [SerializeField]
    Tile end;

    private int x;
    private int y;
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
        x = -3;
        y = -3;
        x = makePlain(x, y, 15);

        while (x < 600)
        {
            x = makePlain(x, y, Random.Range(3, 6));
            x = x + Random.Range(0, 4);
            y += Random.Range(-1, 2);
            if (y > 2)
            {
                y = 2;
            }
            if (y < -4)
            {
                y = -4;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int makePlain(int x, int y, int length)
    {
        int to = x + length;
        map.SetTile(new Vector3Int(x, y, 0), beginning);
        for (int i = x + 1; i < to; i++)
        {
            map.SetTile(new Vector3Int(i, y, 0), middle);
        }
        map.SetTile(new Vector3Int(to, y, 0), end);
        return to + 1;
    }
}
