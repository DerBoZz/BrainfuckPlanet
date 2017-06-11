using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
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
    private float sidewaysInput, jumpInput, jetpackInput, switchWeaponInput, fire;
    private float jetpackTime, airTime;
    private Rigidbody2D rb;

    private Animator anim;
    private bool attacking = false;

    public SpriteRenderer arm;
    public SpriteRenderer shoulder;
    // Use this for initialization
    void Start()
    {
        //remove reset!
        PlayerStats.reset();

        PlayerStats.weaponList[0] = GetComponentInChildren<MeleeWeapon>();
        PlayerStats.weaponList[0].equipable = true;
        PlayerStats.weaponList[1] = GetComponentInChildren<RangedWeapon>();
        ((RangedWeapon)PlayerStats.weaponList[1]).GatherAmmo(9999999);
        arm.enabled = false;
        shoulder.enabled = false;
        PlayerStats.weaponList[0].gameObject.GetComponent<SpriteRenderer>().enabled = true;

        anim = GetComponent<Animator>();

        jetpackTime = moveSettings.resetTimeJetpack;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sidewaysInput = airTime = jumpInput = jetpackInput = switchWeaponInput = fire = 0;
        velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Footsteps();
    }

    void FixedUpdate()
    {
        FireWeapon();
        Move();
        Jump();
        Animation();
        if (switchWeaponInput != 0.0f)
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
        if (Input.GetKey(KeyCode.Space) && !Grounded() && airTime > 0.2f)
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

        Vector3 mousedir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        if (mousedir.x <= transform.position.x && transform.localScale.x > 0.0f)
        {
            //mouse left and facing right -> switch to facing left
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (mousedir.x > transform.position.x && transform.localScale.x < 0.0f)
        {
            //mouse right and facing left -> switch to facing right
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (PlayerStats.weaponList[PlayerStats.equipedWeapon].GetType().ToString() == "RangedWeapon")
        {
            //RangedWeapon look at mouse
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (transform.localScale.x > 0.0f)
            {
                //Facing Right
                PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else if (transform.localScale.x < 0.0f)
            {
                //FacingLeft
                PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.transform.Rotate(new Vector3(0, 0, 1), 180);
            }
        }

    }
    private void FireWeapon()
    {
        if (fire != 0.0f)
        {
            PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.GetComponent<Weapon>().Attack();
            attacking = true;
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
        else if (jetpackInput != 0 && jetpackTime >= 0)
        {

            rb.velocity = new Vector2(rb.velocity.x, moveSettings.jetpackVelocity * Time.deltaTime + rb.velocity.y * 0.99f);
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
        if (PlayerStats.playerHealth <= 0)
        {
            SceneManagement.loadSceneByIndex(0);
        }
    }

    private void Animation()
    {
        if (!Grounded() && jetpackInput != 0 && jetpackTime >= 0)
        {
            //Jetpack
            anim.SetBool("Flying", true);
            anim.SetBool("Moving", false);
            anim.SetBool("Jumping", true);
        }
        else if (!Grounded())
        {
            //Jumping
            anim.SetBool("Jumping", true);
            anim.SetBool("Moving", false);
            anim.SetBool("Flying", false);
        }
        else if (sidewaysInput != 0)
        {
            //Moving
            anim.SetBool("Moving", true);
            anim.SetBool("Flying", false);
            anim.SetBool("Jumping", false);
        }
        else
        {
            //Idle
            anim.SetBool("Moving", false);
            anim.SetBool("Flying", false);
            anim.SetBool("Jumping", false);
        }
        if (attacking && PlayerStats.weaponList[PlayerStats.equipedWeapon].gameObject.GetComponent<Weapon>().GetType().ToString() == "MeleeWeapon")
        {
            anim.SetBool("Melee", true);

            StartCoroutine(WaitForAttack());
        }
    }

    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(0.75f);

        anim.SetBool("Melee", false);
        attacking = false;
    }
    private void Footsteps()
    {
        if (Grounded() && anim.GetBool("Moving"))
        {
            GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }
}
