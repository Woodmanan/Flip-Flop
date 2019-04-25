using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

public class PlayerControlBeta : MonoBehaviour
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
    [SerializeField] private PlayerIndex ControllerNum;
    private GamePadState inp;

    [SerializeField]
    private float speed;
    private float vel;
    [SerializeField]
    private float slide;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpCooldownTime;
    private bool jumping;

    private Rigidbody2D rigid;

    [SerializeField]
    private int lives;
    [SerializeField]
    private bool invincible;
    [SerializeField]
    private float invincibleTime;

    [SerializeField]
    private Text UISlot;

    [SerializeField] private AudioClip[] noises;
    [SerializeField] private AudioClip death;

    private bool cancelsVelocity;

    private bool onGround, jumpCooldown;
    private float deathTime;

    private char left, right, jump;
    // Start is called before the first frame update
    void Start()
    {
        inp = GamePad.GetState(ControllerNum);
        if (inp.IsConnected)
        {
            print("Controller connected for player" + playerNum +  "!");
        }

        left = 'l';
        right = 'r';
        jump = 'j';
        print("ON STARTUP:");
        print("Left is: " + left);
        print("Right is: " + right);
        print("Jump is: " + jump);
        print("Lives is: " + lives);
        print("Test of string behavior.");
        vel = 0;

        jumping = false;

        UISlot.text = "P" + playerNum + " lives: " + lives;
        UISlot.color = GetComponent<SpriteRenderer>().color;
        rigid = GetComponent<Rigidbody2D>();

        cancelsVelocity = true;

#if UNITY_EDITOR
        print("Editor Controls Enabled");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        inp = GamePad.GetState(ControllerNum);
        //player floats until they move
        if (invincible)
        {
            //Box collider Below?
            GetComponent<CircleCollider2D>().enabled = false;
            transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + new Vector3(0, 3, 10) + new Vector3(2.5f - getPlayerNum(), 0, 0);

            //Reset the controller vibration after death. This was apparently an issue.
            if (Time.time - deathTime > .75)
            {
                GamePad.SetVibration(ControllerNum, 0, 0);
            }

            if (Time.time - deathTime < 2)
            {
                UISlot.text = "RT: " + (2 - (int) (Time.time - deathTime));
            }
            else
            {
                UISlot.text = "P" + playerNum + " lives: " + lives;
                if (((int)(Time.time * 4)) % 2 == 0)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }


            if ((getSterilizedInput(left) > 0 || getSterilizedInput(right) > 0) && (Time.time - deathTime > 2))
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


        /*
        Our wonderful moving code. If going left and right no longer works, reimplement this.
        rigid.velocity = new Vector2(toMove.x * speed, rigid.velocity.y);
        */

        //New Movement controls
        float change = (toMove.x * speed / slide);


        if (change != 0)
        {
            vel += change;
            if (vel > speed)
            {
                vel = speed;
            }
            if (vel < -1 * speed)
            {
                vel = -1 * speed;
            }
            rigid.velocity = new Vector2(vel, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(rigid.velocity.x * (1 - 1 / slide), rigid.velocity.y);
        }

        





        if (getSterilizedInput(jump) > 0 && !jumpCooldown && canJump())
        {
            if (cancelsVelocity)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(new Vector2(0, jumpForce));
            }
            else
            {
                //Bouncing jumps. Oof.
                rigid.velocity += new Vector2(0, rigid.velocity.y).normalized * 4;
            }
            
            jumpCooldown = true;
            Invoke("removeJumpCooldown", jumpCooldownTime);
            jumping = true;
            AudioSource aud = GetComponent<AudioSource>();
            aud.clip = noises[Random.Range(0, noises.Length)];
            aud.Play();
        }

        if (getSterilizedInput(jump) == 0 && jumping)
        {
            //rigid.velocity = new Vector2(rigid.velocity.x, 0);
            if (rigid.velocity.y > -.2)
            {
                rigid.AddForce(new Vector2(0, -100));
            }
            jumping = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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

    private float getSterilizedInput(char inputName)
    {
#if UNITY_EDITOR
        if (Input.GetAxis("" + inputName) > 0)
        {
            return Input.GetAxis("" + inputName);
        }
        else
        {
            return 0;
        }
#endif
        if (inputName == 'l')
        {
            if (inp.ThumbSticks.Left.X < 0)
            {
                return Mathf.Abs(inp.ThumbSticks.Left.X);
            }
            else
            {
                return 0;
            }
        }
        else if (inputName == 'r')
        {
            if (inp.ThumbSticks.Left.X > 0)
            {
                return inp.ThumbSticks.Left.X;
            }
            else
            {
                return 0;
            }
        }
        else if (inputName == 'j')
        {
            if(inp.Buttons.A == ButtonState.Pressed)
            {
                return 1;
            }
            else
            {
                return 0;
            }
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
    public string swapByInt(int choice, int turn)
    {
        print(this.gameObject.name + " is being switched with num " + choice);
        char holder;
        switch (choice)
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
                int rotation = turn;
                RotateLeft(rotation);
                if (rotation == 1)
                {
                    return "Controls rotated left";
                }
                return "Controls rotated right";
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
            char holder = right;
            right = jump;
            jump = left;
            left = holder;
        }
    }

    //Called on collision with screen edge. Subrtacts life and respawns player of destroys if out of lives.
    public void respawn()
    {
        lives--;
        deathTime = Time.time;
        GetComponent<AudioSource>().clip = death;
        GetComponent<AudioSource>().Play();
        if (lives > 0)
        {
            invincible = true;
            Invoke("removeInvincible", invincibleTime);
            UISlot.text = "P" + playerNum + " lives: " + lives;
            GetComponent<SpriteRenderer>().enabled = false;
            GamePad.SetVibration(ControllerNum, 1, 1);
        }
        else
        {
            UISlot.transform.parent.gameObject.SetActive(false);
            GamePad.SetVibration(ControllerNum, 0, 0);
            Destroy(this.gameObject);
        }
        
    }

    private void removeInvincible()
    {
        invincible = false;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GamePad.SetVibration(ControllerNum, 0, 0);
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public int getPlayerNum()
    {
        return int.Parse(playerNum);
    }

    public GameObject getUISlot()
    {
        return UISlot.transform.parent.gameObject;
    }

    public void halfGravity()
    {
        jumpForce = jumpForce * .75f;
        GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale / 2;
    }

    public void doubleGravity()
    {
        //jumpForce = jumpForce * 2;
        GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale * 2;
    }

    public void setColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
        UISlot.color = c;
    }

    public void modSlide(float mod)
    {
        slide += mod;
    }

    public bool respawing()
    {
        return invincible;
    }

    private bool canJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .45f, LayerMask.GetMask("Default"));
        if (hit.collider == null || !hit.collider.tag.Equals("Ground"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void velocityCancel(bool b)
    {
        cancelsVelocity = b;
    }
}
