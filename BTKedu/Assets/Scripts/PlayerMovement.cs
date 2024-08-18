using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D Rigidbody_;
    Animator Animator_;

    public float MoveSpeed = 1.0f;
    public float JumpSpeed = 1.0f;

    bool FacingRight = true;
    bool IsGrounded = false;

    public Transform GroundCheck_Position;
    public float GroundCheck_Radius;
    public LayerMask GroundCheck_Layer;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody_ = this.GetComponent<Rigidbody2D>();
        Animator_ = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();

        if (Rigidbody_.velocity.x < 0 && FacingRight)
        {
            FlipFace();
        }
        else if (Rigidbody_.velocity.x > 0 && !FacingRight)
        {
            FlipFace();
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            Jump();
        }

    }

    void HorizontalMove()
    {
        Rigidbody_.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, Rigidbody_.velocity.y);
        Animator_.SetFloat("Speed", Mathf.Abs(Rigidbody_.velocity.x));
    }

    void Jump()
    {
        Rigidbody_.AddForce(new Vector2(0.0f, JumpSpeed));
    }

    void FlipFace()
    {
        FacingRight = !FacingRight;

        Vector3 TempLocalScale = transform.localScale;
        TempLocalScale.x *= -1;
        transform.localScale = TempLocalScale;

    }

    void OnGroundedCheck()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck_Position.position, GroundCheck_Radius, GroundCheck_Layer);
    }

}
