using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string playerNum;
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpCooldownTime;

    private bool onGround, jumpCooldown;

    private string left, right, jump;
    // Start is called before the first frame update
    void Start()
    {
        left = "P" + playerNum + "Left";
        right = "P" + playerNum + "Right";
        jump = "P" + playerNum + "Jump";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toMove = new Vector3(0, 0, 0);
        if (Input.GetAxis(left) > 0)
        {
            toMove += new Vector3(-1, 0, 0);
        }
        if (Input.GetAxis(right) > 0)
        {
            toMove += new Vector3(1, 0, 0);
        }

        transform.position += toMove.normalized * speed;



        if (Input.GetAxis(jump) > 0 && onGround && !jumpCooldown)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            jumpCooldown = true;
            Invoke("removeJumpCooldown", jumpCooldownTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            onGround = false;
        }
    }

    private void removeJumpCooldown()
    {
        jumpCooldown = false;
    }
}
