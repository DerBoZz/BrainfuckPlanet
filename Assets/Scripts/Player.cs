using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
    private Vector3 velocity;
    private float sidewaysInput, jumpInput , jetpackInput;
    private float jetpackTime, airTime;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        airTime = 0;
        jetpackTime = moveSettings.resetTimeJetpack;
        rb = gameObject.GetComponent<Rigidbody>();
        sidewaysInput =  jumpInput = jetpackInput = 0;
        velocity = new Vector3(0, 0, 0);
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
        if (Input.GetKeyDown(KeyCode.Space) && Grounded())
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
            rb.velocity = new Vector3(rb.velocity.x, moveSettings.jumpVelocity, 0);
            jetpackTime = moveSettings.resetTimeJetpack;
        }else if(jetpackInput != 0 && jetpackTime >= 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, moveSettings.jetpackVelocity, 0);
            jetpackTime -= Time.deltaTime;
        }
        

    }
    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f, moveSettings.ground);
    }
    


    public void Damage(int damage)
    {
        PlayerStats.playerHealth -= damage;
        if(PlayerStats.playerHealth <= 0)
        {
            SceneManagement.loadScene("Menue");
        }
    }
}
