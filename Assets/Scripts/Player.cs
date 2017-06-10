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
    private float sidewaysInput, jumpInput , jetpackInput, switchWeaponInput, fire;
    private float jetpackTime, airTime;
    private Rigidbody2D rb;

    public SpriteRenderer arm;
    public SpriteRenderer shoulder;
    // Use this for initialization
    void Start () {
        //remove reset!
        PlayerStats.reset();

        PlayerStats.weaponList[0] = GetComponentInChildren<MeleeWeapon>();
        PlayerStats.weaponList[0].equipable = true;
        PlayerStats.weaponList[1] = GetComponentInChildren<RangedWeapon>();
        ((RangedWeapon)PlayerStats.weaponList[1]).GatherAmmo(50);
        arm.enabled = false;
        shoulder.enabled = false;
        PlayerStats.weaponList[0].gameObject.GetComponent<SpriteRenderer>().enabled = true;


        airTime = 0;
        jetpackTime = moveSettings.resetTimeJetpack;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sidewaysInput =  jumpInput = jetpackInput = switchWeaponInput=fire= 0;
        velocity = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
    }

    void FixedUpdate()
    {
        FireWeapon();
        Move();
        Jump();
        if (switchWeaponInput!=0.0f)
        {
            SwitchWeapon();
            switchWeaponInput = 0.0f;
        }
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switchWeaponInput = 1.0f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            fire = 1.0f;
        }

        float AngleRad = Mathf.Atan2(Input.mousePosition.y - transform.position.y, Input.mousePosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
    private void FireWeapon()
    {
        if (fire != 0.0f)
        {
            PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.GetComponent<Weapon>().Attack();
            if (!PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.GetComponent<Weapon>().equipable)
            {
                SwitchWeapon();
            }
            fire = 0.0f;
        }
    }
    private void SwitchWeapon()
    {
            PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.GetComponent<SpriteRenderer>().enabled = false;
            do
            {
                PlayerStats.equipedWeapon = (PlayerStats.equipedWeapon + 1) % PlayerStats.weaponList.Length;
            } while (!PlayerStats.weaponList[PlayerStats.equipedWeapon].equipable);
            PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, moveSettings.ground);
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
