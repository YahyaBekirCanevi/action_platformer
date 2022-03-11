using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected float maxHP = 10;
    protected float currentHP;
    protected bool isAttacking = false;
    protected SphereCollider hurtBox;
    protected Rigidbody rb;
    protected AnimationController ac;
    private float gravity = -9.81f;
    private float minYPosition = -40f;
    protected float fallMult = 2.5f;
    protected float lowJumpMult = 2f;
    protected float groundHeight = 0.02f;
    protected float wallSlideSpeed = 2f;
    protected bool TouchingWall {
        get => Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitR, hurtBox.radius + groundHeight) 
                && hitR.collider.CompareTag("Ground") || Physics.Raycast(transform.position, Vector3.left, 
                out RaycastHit hitL, hurtBox.radius + groundHeight) && hitL.collider.CompareTag("Ground");
    }
    protected float GroundDistance {
        get => Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit) &&
                hit.collider.CompareTag("Ground") ? hit.distance : 50;
    }
    protected bool Grounded {
        get => GroundDistance <= hurtBox.radius + groundHeight;
    }
    protected bool ToLand {
        get => GroundDistance <= hurtBox.radius + groundHeight * 5;
    }
    public float HP {
        get => currentHP;
        set { currentHP = value; }
    }
    public bool isGrounded = false;
    public bool isWallSliding = false;
    private float slideTime = 0;
    protected void StartFunc() {
        ac = GetComponentInChildren<AnimationController>();
        rb = GetComponent<Rigidbody>();
        hurtBox = GetComponent<SphereCollider>();
        currentHP = maxHP;
    }
    protected void UpdateFunc(bool jumpState) {
        if(transform.position.y < minYPosition) {
            Destroy(gameObject);
        }
        isGrounded = Grounded;
        isWallSliding = TouchingWall && rb.velocity.y < 0 && !isGrounded;
        currentHP = currentHP > maxHP ? maxHP : currentHP < 0 ? 0 : currentHP;
        
        if(isWallSliding) {
            slideTime += Time.deltaTime;
            rb.velocity = Vector3.up * Mathf.Lerp(-wallSlideSpeed, 0, slideTime);
        }
        else {
            slideTime = 0;
            if(!isGrounded) rb.velocity += Vector3.up * gravity * (!isGrounded && rb.velocity.y < 0 ?
             fallMult - 1 : jumpState && rb.velocity.y > 0 ? lowJumpMult - 1 : 0) * Time.deltaTime;
        }
    }
    protected abstract void Move();
}
