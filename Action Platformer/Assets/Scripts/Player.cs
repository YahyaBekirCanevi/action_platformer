using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float moveSpeed = 4f;
    private float jumpStr = 8f, dashPower = 3f;
    private int jumpCount = 0, fallCount = 0;
    private int jumpControl = 0, fallControl = 0;
    private float dashCount = 0, dashControl = 0;
    private int maxJump = 2, maxFall = 1, maxDash = 2;
    private float minCameraZoom = 2.5f, maxCameraZoom = 5f;
    private Transform lastPos, cam;
    private Character target;
    private AnimState animState;
    private Vector3 move = Vector3.zero;
    private float ver = 0, hor = 0;
    private void Start() {
        animState = AnimState.Idle;
        cam = Camera.main.transform;
        StartFunc();
    }
    private void Update() {
        ac.ChangeState(animState);
        ac.ResetState();
        Move();
        Skill();
        UpdateFunc(!Input.GetKey(Manager.Input.Up));
        CameraMove();
    }
    protected override void Move() {
        ver = Input.GetKeyDown(Manager.Input.Up) ? 1 : Input.GetKeyDown(Manager.Input.Down) ? -1 : 0;
        hor = (Input.GetKey(Manager.Input.Right) ? 1 : 0) + (Input.GetKey(Manager.Input.Left) ? -1 : 0);

        ac.facingRight = hor == 0 ? ac.facingRight : hor > 0;
        if (!ac.GetCurrentState(AnimState.Jump)) 
            animState = hor == 0 ? AnimState.Idle : AnimState.Run;
        animState = isGrounded ? isWallSliding ? AnimState.WallSlide : animState : AnimState.Jump;

        if(isGrounded || !isWallSliding) {
            dashCount = Input.GetKeyDown(Manager.Input.Dash) && dashCount < maxDash ? dashCount + 1 : isGrounded || isWallSliding ? 0 : dashCount;
            dashControl = dashCount != 0 ? dashControl : 0;
            if(dashCount - dashControl == 1){
                move = Vector3.right * hor * dashPower;
                dashControl = dashCount;
            }
            else{
                move = Vector3.right * hor * moveSpeed * Time.deltaTime;
            }
            transform.Translate(move);
        }
        ac.SetProperty<float>("Distance", GroundDistance);
    }
    private void Skill(){
        jumpCount = (isGrounded || isWallSliding) ? 0 : jumpCount;
        jumpControl = jumpCount == 0 ? 0 : jumpControl;
        if(ver == 1 && jumpCount < maxJump) {
            jumpCount++;
            if(jumpCount - jumpControl == 1) {
                rb.velocity = Vector3.up * jumpStr;
                jumpControl = jumpCount;
            }
        }
        if(ac.CheckCurrentState("Fall") && ToLand) ac.Play("Land");
        
        fallCount = isGrounded || isWallSliding ? 0 : fallCount;
        fallControl = fallCount == 0 ? 0 : fallControl;
        if(ver == -1 && fallCount < maxFall) {
            fallCount++;
            rb.velocity = Vector3.down * jumpStr;
        }
    }
    private void CameraMove() {
        cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(cam.GetComponent<Camera>().orthographicSize, 
             target is null ? maxCameraZoom : minCameraZoom, Time.deltaTime * 2f);
        cam.position = Vector3.Lerp(cam.position, (target is null ? transform.position : Vector3.Lerp(transform.position,
             target.transform.position, .5f)) + Vector3.back * 10, Time.deltaTime * 8f);
    }
}
