using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AnimState currentState;
    private SpriteRenderer spriteRenderer;
    public bool facingRight = true;
    private void Start() {
        
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        spriteRenderer.flipX = !facingRight;
    }
    public void ChangeState(AnimState newState){
        if (newState == currentState) return;
        anim.Play(newState.ToString());
        currentState = newState;
    }
    public void Play(string newState){
        anim.Play(newState);
    }
    public void SetProperty<T>(string name, T value) {
        if(typeof(T) == typeof(float)) {
            anim.SetFloat(name, (float)System.Convert.ChangeType(value, typeof(float)));
        }
        else if(typeof(T) == typeof(bool)) {
            anim.SetBool(name, (bool)System.Convert.ChangeType(value, typeof(bool)));
        } 
        else {
            return;
        }
    }
    public T GetProperty<T>(string name) {
        if(typeof(T) == typeof(float)) {
            return (T)System.Convert.ChangeType(anim.GetFloat(name), typeof(T));
        }
        else if(typeof(T) == typeof(bool)) {
            return (T)System.Convert.ChangeType(anim.GetBool(name), typeof(T));
        } 
        else {
            return (T)System.Convert.ChangeType(0, typeof(T));
        }
    }
    public bool GetCurrentState(AnimState name){
        return currentState == name;
    }
    public void ResetState(){
        if(CheckCurrentState(AnimState.Idle))
            currentState = AnimState.Idle;
        else if(CheckCurrentState(AnimState.Run))
            currentState = AnimState.Run;
        else return;
    }
    public bool CheckCurrentState(AnimState test) => anim.GetCurrentAnimatorStateInfo(0).IsName((AnimState.Idle).ToString());
    public bool CheckCurrentState(string name) => anim.GetCurrentAnimatorStateInfo(0).IsName(name);
}
public enum AnimState { Idle, Run, Jump, WallSlide }