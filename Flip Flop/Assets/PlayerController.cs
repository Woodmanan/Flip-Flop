using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This class controls player movement and the like
    //Setting the playernum initializes the strings that store the input axis names.
    //We store these as strings, so that way we can easily swap them.
    //GetSterilizedInput is used so that on controllers we can make the joystics act like buttons if we need to

    //Speed is self explanatory, jumpforce is how much force is applied to the rigidbody to make the jump happen
    //JumpCooldownTime is how long between jumps before a new force can be applied (Stops flying when touching walls)

    //Lives is how many lives the player has. One is subtracted with each screen edge collision. Start with 3 and lose at 0.   
    //invincible is a boolean used for respawn. When invincible, player floats in midle of camera and cant interact with level.
    //When any move key is pressed, or 5 seconds have passed, invincible becomes false.

    [SerializeField]
    private string playerNum;
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpCooldownTime;

    [SerializeField]
    private int lives;
    [SerializeField]
    private bool invincible;
    [SerializeField]
    private float invincibleTime;

    private bool onGround, jumpCooldown;

    private string left, right, jump;
    // Start is called before the first frame update
    void Start()
    {
        invincibleTime = 5; 
        left = "P" + playerNum + "Left";
        right = "P" + playerNum + "Right";
        jump = "P" + playerNum + "Jump";
        lives = 3;
        print("ON STARTUP:");
        print("Left is: " + left);
        print("Right is: " + right);
        print("Jump is: " + jump);
        print("Lives is: " + lives);
    }

    // Update is called once per frame
    void Update()
    {
        //player floats until they move
        if (invincible)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + new Vector3(0, 3, 10);

            if (getSterilizedInput(left) > 0 || getSterilizedInput(right) > 0)
            {
                removeInvincible();
            }
        }
        
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

    //Called on collision with screen edge. Subrtacts life and respawns player of destroys if out of lives.
    public void respawn()
    {
        lives--;
        if (lives > 0)
        {
            invincible = true;
            Invoke("removeInvincible", invincibleTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void removeInvincible()
    {
        invincible = false;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
