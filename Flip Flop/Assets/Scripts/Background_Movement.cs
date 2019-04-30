using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Movement : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float grav;
    public int time;

    private float spawn;
    // Start is called before te first frame update
    void Start()
    {
        spawn = Time.time;
        int rollx = Random.Range(1, 5); // 1, 2 or 3
        float numx = 0.14f; // if you roll a 1, it keep this
        if (rollx == 2) numx = -0.14f;
        if (rollx == 3) numx = 0.19f;
        if (rollx == 4) numx = -0.19f;
        speedX = numx;




        int rolly = Random.Range(1, 4); // 1, 2 or 3
        float numy = 0.28f; // if you roll a 1, it keep this
        if (rolly == 2) numy = 0.25f;
        if (rolly == 3) numy = 0.29f;
        if (rolly == 4) numy = 0.21f;
        speedY = numy;
        grav = 0.005f;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        RotateLeft();
        transform.position += new Vector3(speedX, speedY, 0) * Time.deltaTime * 20;
        speedY -= grav * Time.deltaTime * 20;
        time += 1;
        if (time % 20 == -1) {
            speedX = 0.2f;
            speedY = 0.2f;
            }      
        if (Time.time - spawn > 10)
        {
            Destroy(this.gameObject);
        }
    }
    void RotateLeft()
    {
        transform.Rotate(Vector3.forward * -3 * Time.deltaTime * 20);
    }
}
