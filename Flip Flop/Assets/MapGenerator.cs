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
            if (y < -3)
            {
                y = -3;
            }
            else if (y > 2)
            {
                y = 1;
            }
            else if (y < -1)
            {
                y++;
            }
            else if (y > -1)
            {
                y--;
            }
            int choice = Random.Range(0, 20);
            if (choice < 5)
            {
                x = makePlain(x, y, Random.Range(3, 6));
                x = x + Random.Range(0, 4);
                y += Random.Range(-1, 2);
                if (y > 2)
                {
                    y = 2;
                }
                else if (y < -4)
                {
                    y = -4;
                }
            }
            else if (choice == 5)
            {
                for (int i = 0; i < 7; i++)
                {
                    x = makePlain(x, y, 2);
                    x += Random.Range(2, 4);
                }
            }
            else if (choice == 6)
            {
                //Pyramid
                y = -4;
                for (int i = 0; i < 5; i++)
                {
                    makePlain(x, y, 3);
                    x++;
                    y++;
                }
                y--;
                x += 6;

                for (int i = 0; i < 5; i++)
                {
                    makePlain(x, y, 3);
                    x++;
                    y--;
                }

                x += 3;
            }
            else if (choice == 7)
            {
                // SSSSS
                /*         #########
                 *        ##                 
                 * ######    
                 * ############
                 * 
                 */
                if (y > 2)
                {
                    y = 1;
                }
                makePlain(x, y, 4);
                makePlain(x, y - 1, 8);
                x = x + 6;
                y++;
                makePlain(x, y, 2);
                x = x + 1;
                y++;
                x = makePlain(x, y, 5);
            }

            else if (choice == 8)
            {
                while (y < 0)
                {
                    x = makePlain(x, y, 2);
                    x--;
                    y++;
                }

                x += 2;
                // ;)
                y = -2;

                makePlain(x + 4, 2, 2);

                makePlain(x + 11, 2, 2);

                x = makePlain(x, y, 2);
                x = makePlain(x, y-1, 2);
                x = makePlain(x, y-2, 5);
                x = makePlain(x, y-1, 2);
                x = makePlain(x, y, 2);
            }

            else if (choice == 9)
            {
                /*     ##   ##
                 * #####     ######
                 */
                makePlain(x, y, 3);
                x += 2;
                y++;
                makePlain(x, y, 2);
                x += 5;
                makePlain(x, y, 2);
                y--;
                x++;
                x = makePlain(x, y, 4);
            }
            else if (choice == 10)
            {
                /*      #    #     
                 * ## ##  #    #
                 */
                
                makePlain(x, y, 2);
                x += 4;

                makePlain(x, y, 2);
                y++;
                x += 4;
                makePlain(x, y, 2);
                y--;
                x += 5;
                makePlain(x, y, 2);
                y++;
                x += 5;
                makePlain(x, y, 2);
                y--;
                x += 4;
                makePlain(x, y, 2);
            }

            else if (choice == 11)
            {
                /*  #   #  
                 * ##   ##
                 */
                
                makePlain(x, y, 2);
                x += 1;
                y++;
                makePlain(x, y, 1);
                x += 3;
                makePlain(x, y, 1);
                y--;
                x++;
                makePlain(x, y, 2);
            }
            else if (choice == 12)
            {
                /*     ##   ##
                *
                 *  ##    #   ##
                 */


                
                x = makePlain(x, y, 3);
                
                y += 2;
                x = makePlain(x, y, 2);
                y -= 2;
                x = makePlain(x, y, 2);
                y += 2;
                x = makePlain(x, y, 2);
                y -= 2;
                x = makePlain(x, y, 3);
            }

            else if (choice == 13)
            {
                // -_-
                x = makePlain(x, y, 5);
                y--;
                x++;
                x = makePlain(x, y, 2);
                x++;
                y++;
                x = makePlain(x, y, 5);
            }
            else if (choice == 14)
            {
                // _-__-__-__-__
                for (int i = 0; i < 6; i++)
                {
                    makePlain(x, y, 3);
                    x = x + 2;
                    x = makePlain(x, y + 1, 1);
                }
            }
            else if (choice == 15)
            {
                // ___ ---
                x = makePlain(x, y, 3);
                x++;
                y++;
                x = makePlain(x, y, 3);
            }
            else if (choice == 16)
            {
                // --- ___
                x = makePlain(x, y, 3);
                x++;
                y--;
                x = makePlain(x, y, 3);
            }
            else if (choice == 17)
            {
                //       _---
                //  ----
                x++;
                x = makePlain(x, y, 4);
                y++;
                x++;
                x = makePlain(x, y, 1);
                y++;
                x = makePlain(x, y, 3);
            }
            else if (choice == 18)
            {
                //       _---   but like opposite
                //  ----
                x++;
                x = makePlain(x, y, 4);
                y--;
                x++;
                x = makePlain(x, y, 1);
                y--;
                x = makePlain(x, y, 3);
            }
            else if (choice == 19)
            {
                // Hi
                x++;
                makePlain(x, y + 1, 1);
                makePlain(x + 3, y + 1, 1);
                for (int i = y; i > y - 3; i--)
                {
                    makePlain(x, i, 1);
                    makePlain(x + 3, i, 1);
                }
                x = makePlain(x, y, 4);
                x++;
                for (int i = y + 1; i > y - 3; i--)
                {
                    if (i != y)
                    {
                        makePlain(x, i, 1);
                    }
                }
                x++;
                x++;
                x++;
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