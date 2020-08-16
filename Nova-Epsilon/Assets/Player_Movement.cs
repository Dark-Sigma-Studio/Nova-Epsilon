using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;

    public float TurnTime = 0.1f;
    private float turnvel;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.05f)
        {
            float targetangle = Mathf.Atan2(direction.x, direction.z) *  Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnvel, TurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;

            controller.Move(movDir.normalized * speed * Time.deltaTime);
        }
    }
}
