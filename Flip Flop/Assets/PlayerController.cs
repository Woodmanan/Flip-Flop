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
        print("ON STARTUP:");
        print("Left is: " + left);
        print("Right is: " + right);
        print("Jump is: " + jump);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toMove = new Vector3(0, 0, 0);
        if (getSterilizedInput(left) > 0)
        {
            toMove += new Vector3(-1, 0, 0);
        }
        if (getSterilizedInput(right) > 0)
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

    private float getSterilizedInput(string inputName)
    {
        if (Input.GetAxis(inputName) > 0)
        {
            return Input.GetAxis(inputName);
        }
        return 0;
    }

    //Needs to return the max number of choices
    //Has to be hardcoded. This is terrible but can't really be fixed.
    public int getNumSwaps()
    {
        return 3;
    }


    //WHEN UPDATING: MAKE SURE TO UPDATE getNumSwaps()!

    //Swaps controls numTimes
    public void swapByInt(int choice)
    {
        print(this.gameObject.name + " is being switched with num " + choice);
        string holder;
        switch(choice)
        {
            case 0:
                print("Left and Right swapped.");
                //swap left and right
                holder = left;
                left = right;
                right = holder;
                break;
            case 1:
                print("Jump and Left Swapped.");
                //swap jump and left
                holder = jump;
                jump = left;
                left = holder;
                break;
            case 2:
                print("Right and Jump swapped.");
                //swap jump and right
                holder = right;
                right = jump;
                jump = holder;
                break;
        }
        print("Left is: " + left);
        print("Right is: " + right);
        print("Jump is: " + jump);
    }
}
