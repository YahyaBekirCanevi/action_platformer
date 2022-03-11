using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float minDistance = .1f, maxDistance = 3f, attackDistance = 3f;
    private float lastXVelocity = 0;
    private float idleTimer = 0, waitIdle = 2;
    private Vector3 initPosition, destination;
    [SerializeField] private State state;
    private Character target;
    private void Start() {
        StartFunc();
        initPosition = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit) ? hit.point : transform.position;
        minDistance += hurtBox.radius;
        Move();
    }
    private void Update() {
        UpdateFunc(true);
        if(target is null) target = Manager.Instance.player;
        state = InSight() ? (Vector3.Distance(transform.position, target.transform.position) > attackDistance ? State.Follow : State.Attack) :
            (Vector3.Distance(transform.position, destination) < minDistance ? State.Idle : State.Patrol);
        ApplyStates();
    }
    protected override void Move() {
        lastXVelocity = rb.velocity.x != 0 ? rb.velocity.x : lastXVelocity;
        ac.facingRight = lastXVelocity != 0 ? lastXVelocity > 0 : ac.facingRight;
        destination = state == State.Patrol ? (initPosition + Vector3.right * (ac.facingRight ? 1 : -1) * maxDistance) : state == State.Follow ? target.transform.position : transform.position;  
    }
    private bool InSight(){
        return !(Vector3.SignedAngle(transform.right * (ac.facingRight ? 1 : -1), target.transform.position - transform.position, Vector3.forward) > 0 ^ ac.facingRight);
    }
    private void ApplyStates(){
        Helpers.Timer(idleTimer, waitIdle, () => { 
            state = State.Patrol;
            return true;
        });
        switch (state) {
            case State.Patrol : {
                idleTimer += Time.deltaTime * 8f;
                Move();
                transform.position = Vector3.MoveTowards(transform.position, destination, .8f);
                break;
            }
            case State.Attack : {
                target.HP -= 1;
                break;
            }
            case State.Follow : {
                Move();
                transform.position = Vector3.MoveTowards(transform.position, destination, .8f);
                break;
            }
            default: break;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(destination, 1);
        Gizmos.color = Color.cyan;
    }
    private enum State { Idle, Patrol, Attack, Follow }
}