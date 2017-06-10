using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [System.Serializable]
    public class MoveSettings
    {
        public float playerSpeed;
        public float jumpVelocity;
        public float gravity;
        public float resetTimeJetpack;
        public float jetpackVelocity;
        public LayerMask ground;
    }
    public MoveSettings moveSettings;
    private Vector2 velocity;
    private float sidewaysInput, jumpInput , jetpackInput;
    private float jetpackTime, airTime;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        //remove reset!
        PlayerStats.reset();
        PlayerStats.weaponList[0] = new MeleeWeapon();
        airTime = 0;
        jetpackTime = moveSettings.resetTimeJetpack;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sidewaysInput =  jumpInput = jetpackInput = 0;
        velocity = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    void FixedUpdate()
    {
        Move();
        Jump();
    }
    void GetInput()
    {
        jumpInput = 0;
        jetpackInput = 0;
        sidewaysInput = Input.GetAxis("Horizontal");
        if (!Grounded())
        {
            airTime += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && Grounded())
        {
            jumpInput = 1.0f;
            airTime = 0;
        }
        if(Input.GetKey(KeyCode.Space) && !Grounded() && airTime > 0.2f)
        {
            jetpackInput = 1.0f;
        }
    }
    private void Move()
    {
        velocity.x = sidewaysInput * moveSettings.playerSpeed;
        velocity.y = rb.velocity.y - moveSettings.gravity;
        rb.velocity = transform.TransformDirection(velocity);
    }
    private void Jump()
    {
        if (jumpInput != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSettings.jumpVelocity);
            jetpackTime = moveSettings.resetTimeJetpack;
        }
        else if(jetpackInput != 0 && jetpackTime >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSettings.jetpackVelocity * Time.deltaTime + rb.velocity.y*0.99f);
            jetpackTime -= Time.deltaTime;
        }
        

    }
    private bool Grounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.2f, moveSettings.ground);
    }



    public void Damage(int damage)
    {
        PlayerStats.playerHealth -= damage;
        if(PlayerStats.playerHealth <= 0)
        {
            //SceneManagement.loadScene("Menue");
        }
    }
}
