using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public fields
    public float jumpHeight = 1;
    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    public Transform cam;

    

    bool isJump
    {
        get { return Input.GetAxisRaw("Jump") > 0; }
    }
    public bool isADS
	{
		get { return Input.GetAxisRaw("Fire2") > 0; }
	}
    #endregion

    private struct PRIVATE
	{
        public Vector3 vel;
        public CharacterController charcon;
        public float turnTime;
        public PlayerRace.Race PRace;
    }
    private PRIVATE p = new PRIVATE();

    // Start is called before the first frame update
    void Start()
    {
        p.charcon = GetComponent<CharacterController>();
        p.PRace = GetComponent<PlayerRace>().race;
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(vertical, 0, horizontal).normalized;

        if (direction.magnitude >= 0.1)
        {
            p.PRace.moveMag = direction.magnitude;

            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, isADS ? cam.eulerAngles.y : targetAngle, ref p.turnTime, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 movDir = (Quaternion.Euler(0, targetAngle, 0) * Vector3.forward).normalized;

            p.vel.x = movDir.x * speed;
            p.vel.z = movDir.z * speed;
        }
        else
        {
            p.PRace.moveMag = 0;

            p.vel.x = 0;
            p.vel.z = 0;
        }

        if (p.charcon.isGrounded)
        {
            if (isJump)
            {
                p.vel.y = Mathf.Sqrt(-2 * jumpHeight * Physics.gravity.y);
                p.PRace.hasJumped = true;
            }
            else
            {
                p.vel.y = 0;
            }
        }
		else
		{
            p.vel += Physics.gravity * Time.deltaTime; 
		}

        p.charcon.Move(p.vel * Time.deltaTime);
    }

}
