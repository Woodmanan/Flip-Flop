using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Rigidbody2D rigid;

    [SerializeField]
    private int lives;
    [SerializeField]
    private bool invincible;
    [SerializeField]
    private float invincibleTime;

    [SerializeField]
    private Text UISlot;

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
        print("Lives is: " + lives);

        UISlot.text = "P" + playerNum + " lives: " + lives;
        rigid = GetComponent<Rigidbody2D>();
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

        //Non-rigidbody based code (OUTDATED!)
        //transform.position += toMove.normalized * speed;
        rigid.velocity = new Vector2(toMove.x * speed, rigid.velocity.y);
        /* Evil Rigidbody code
        if (Mathf.Abs(rigid.velocity.x) > 7)
        {
            rigid.AddForce(toMove.normalized * speed * 50);
        }
        else
        {
            rigid.AddForce(toMove.normalized * speed * 400);
        }
        
        if (Mathf.Abs(rigid.velocity.x) > 10)
        {
            rigid.velocity = new Vector2(rigid.velocity.x / Mathf.Abs(rigid.velocity.x) * 5, rigid.velocity.y);
        }
        */
        

            



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
        return 4;
    }


    //WHEN UPDATING: MAKE SURE TO UPDATE getNumSwaps()!

    //Swaps controls numTimes
    public string swapByInt(int choice)
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
                return "Left and Right swapped.";
            case 1:
                print("Jump and Left Swapped.");
                //swap jump and left
                holder = jump;
                jump = left;
                left = holder;
                return "Left and Jump swapped.";
            case 2:
                print("Right and Jump swapped.");
                //swap jump and right
                holder = right;
                right = jump;
                jump = holder;
                return "Right and Jump swapped.";
            case 3:
                int rotation = Random.Range(1, 3);
                RotateLeft(rotation);
                return "Controls rotated left " + rotation + "time(s).";
        }
        print("Left is: " + left);
        print("Right is: " + right);
        print("Jump is: " + jump);
        return "Nothing! This is a bug.";
    }

    public void RotateLeft(int count)
    {
        print(this.gameObject.name + "is having its controls rotated " +
               count + " times");
        for (int i = 0; i < count; i++)
        {
            string holder = right;
            right = jump;
            jump = left;
            left = holder;
        }
    }

    //Called on collision with screen edge. Subrtacts life and respawns player of destroys if out of lives.
    public void respawn()
    {
        lives--;
        if (lives > 0)
        {
            invincible = true;
            Invoke("removeInvincible", invincibleTime);
            UISlot.text = "P" + playerNum + " lives: " + lives;
        }
        else
        {
            UISlot.transform.parent.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    private void removeInvincible()
    {
        invincible = false;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public int getPlayerNum()
    {
        return int.Parse(playerNum);
    }

    public GameObject getUISlot()
    {
        return UISlot.transform.parent.gameObject;
    }
}
