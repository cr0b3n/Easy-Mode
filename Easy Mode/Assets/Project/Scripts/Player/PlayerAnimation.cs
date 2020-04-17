using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimation : MonoBehaviour {
    public Animator animator;    
    public Rigidbody2D myRB;

    public PlayerPlatformerMovement movement;
    public PlayerPlatformerInput input;

    //public PlayerFourDirectionMovement movement;
    //public PlayerFourDirectionInput input;

    private int speedAnimID;
    private int isJumpingAnimID;
    private int isLandingAnimID;
    private int yVelocityAnimID;
    private int attackAnimID;
    private int isDashingID;

    private bool isActive;

    private void Start() {
        isActive = true;
        //EffectManager.instance.ShowPlayerSpawnEffect(new Vector3(transform.position.x, 1.09f));

        //Set up animation int ID cause they are faster than strings
        speedAnimID = Animator.StringToHash("speed");
        //isJumpingAnimID = Animator.StringToHash("isJumping");
        //isLandingAnimID = Animator.StringToHash("isLanding");
        //yVelocityAnimID = Animator.StringToHash("yVelocity");
        //attackAnimID = Animator.StringToHash("attack");
        //isDashingID = Animator.StringToHash("isDashing"); 
    }

    private void Update() {

        if (!isActive) return;

        //animator.SetBool(isLandingAnimID, movement.isOnGround);
        //animator.SetFloat(yVelocityAnimID, myRB.velocity.y);
        //animator.SetBool(isJumpingAnimID, movement.isJumping);
        //animator.SetBool(attackAnimID, movement.isAttacking);
        //animator.SetFloat(speedAnimID, Mathf.Abs(input.horizontal));
        animator.SetFloat(speedAnimID, Mathf.Abs(myRB.velocity.x));
    }

    //private void OnDisable() {
    //    GameManager.instance.OnYounginDeath -= OnYounginDeath;
    //}
}
