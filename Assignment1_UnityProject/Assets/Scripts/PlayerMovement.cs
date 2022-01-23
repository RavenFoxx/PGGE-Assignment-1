using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #if UNITY_ANDROID
        public FixedJoystick mJoystick;
    #endif
    float hInput;
    float vInput;
    float speed;
    bool jump = false;
    bool crouch = false;
    Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;
    public float mWalkSpeed = 1.5f;
    public float mRotationSpeed = 50.0f;
    public float mTurnRate = 10.0f;
    public float mGravity = -30.0f;
    public float mJumpHeight = 1.0f;
    public bool mFollowCameraForward = false;
    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }
    void Update()
    {

    }
    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public void HandleInputs()
    {
    #if UNITY_STANDALONE
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
    #endif

    #if UNITY_ANDROID
        hInput = 2.0f * mJoystick.Horizontal;
        vInput = 2.0f * mJoystick.Vertical;
    #endif
        speed = mWalkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = mWalkSpeed * 2.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mAnimator.SetTrigger("Excited");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mAnimator.SetTrigger("Disappointed");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mAnimator.SetTrigger("Tired");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            mAnimator.SetTrigger("StretchF");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            mAnimator.SetTrigger("StretchS");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            mAnimator.SetTrigger("Wave");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            mAnimator.SetTrigger("Spin");
        }
    }

    public void Move()
    {
        if (crouch) return;
        if (mAnimator == null) return;
        if (mFollowCameraForward)
        {
            Vector3 eu = Camera.main.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0.0f, eu.y, 0.0f),
                mTurnRate * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;
        mCharacterController.Move(forward * vInput * speed * Time.deltaTime);
        mAnimator.SetFloat("PosX", 0);
        mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed));
        if(jump)
        {
            Jump();
            jump = false;
        }
    }
    void Jump()
    {
        mAnimator.SetTrigger("Jump");
        mVelocity.y += Mathf.Sqrt(mJumpHeight * -2f * mGravity);
    }
    void ApplyGravity()
    {
        mVelocity.y += mGravity * Time.deltaTime;
        if (mCharacterController.isGrounded && mVelocity.y < 0)
            mVelocity.y = 0f;
    }
}
