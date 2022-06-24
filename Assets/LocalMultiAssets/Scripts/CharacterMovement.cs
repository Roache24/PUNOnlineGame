using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Tooltip("MovementValues")]
    [SerializeField] float speedMultiplier, rotationSpeed, gravityForce, jumpForce, groundCastRange;

    //Components
    CharacterController cc;
    public Animator anim;
    Vector3 movementDirection;
    Vector3 playerVelocity;
    bool playerGrounded;
    public float WaitTime = 3;

    PlayerInput inputs;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        inputs = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        playerGrounded = cc.isGrounded;
        if (playerGrounded && playerVelocity.y < 0)
        {
            
            if (anim.GetBool("Jumping")) anim.SetBool("Jumping", false);
            
            playerVelocity.y = 0f;
        }

        if (anim.GetBool("Dead")) return;

        var h = Input.GetAxis(inputs.horizontal);
        var v = Input.GetAxis(inputs.vertical);

        if( h != 0 || v != 0)
        {
            movementDirection.Set(h, 0, v);
            cc.Move(movementDirection * speedMultiplier * Time.deltaTime);
            anim.SetBool("HasInput", true);
        }
        else
        {
            anim.SetBool("HasInput", false);
        }

        var desiredDirection = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredDirection, rotationSpeed);

        var animationVector = transform.InverseTransformDirection(cc.velocity);

        anim.SetFloat("ForwardMomentum", animationVector.z);
        anim.SetFloat("SideMomentum", animationVector.x);

        ProcessGravity();

    }

    public void ProcessGravity()
    {
        if(Input.GetKeyDown(inputs.jump) && playerGrounded)
        {
            anim.SetBool("Jumping", true);
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityForce);
        }
        playerVelocity.y += gravityForce * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }

   
}
