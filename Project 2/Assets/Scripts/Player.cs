using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {
    public float jumpHeightMax = 4;
    public float jumpHeightMin = 1;
    public float timeToJumpApex = 0.4f;
    public float moveSpeed = 6;
    public float maxLife = 100;
    float currentLife;

    public Animator mAnimator;
    public Transform spriteTransform;
    public Vector3 mScale;
    public Slider healthBar;

    // WallJump
    public float wallSlideSpeed = 3;
    public float wallStickTime = 0.25f;
    float timeToWallUnstick;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    

    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    float velocityXSmoothing;
    float gravity;
    float jumpVelocityMax;
    float jumpVelocityMin;

    Vector3 velocity;
    Controller2D controller;
    Vector3 directionalInput;

    Rigidbody2D espada;
    public bool attacking;

    bool wallSliding;
    int wallDirectionX;
    

    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeightMax) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocityMax = Mathf.Abs(gravity) * timeToJumpApex;
        jumpVelocityMin = Mathf.Sqrt(2 * Mathf.Abs(gravity) * jumpHeightMin);

        currentLife = maxLife;

        //print("Gravity: " + gravity + " |||||| jump velocity: " + jumpVelocityMax);

        espada = this.GetComponentInChildren<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update () {

        CalculateVelocity();
        //HandleWallSliding();

        controller.Move(velocity * Time.deltaTime, directionalInput);

        //Caso alguma colisao seja detectada a cima ou a baixo do player seta sua velocidade y(vertical) para zero
        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }
        if (controller.collisions.below) {
            mAnimator.SetBool("IsGround", true);
        }

        //Mandando os valores para o animator
        mAnimator.SetFloat("MoveSpeed", Mathf.Abs(velocity.x) / moveSpeed);
        //Setando a escala de acordo com a velocidade
        spriteTransform.localScale = Vector3.right * Mathf.Sign(velocity.x) + Vector3.up + Vector3.forward;

        //calculateLife();
    }

    public void SetDirectionalInput(Vector2 input) {
        directionalInput = input;
    }

    public void OnJumpInputDown() {
        mAnimator.SetBool("IsGround", false);
            if (wallSliding) {
                if(wallDirectionX == directionalInput.x) {
                    velocity.y = wallJumpClimb.y;
                    velocity.x = -wallDirectionX * wallJumpClimb.x;
                } else if(directionalInput.x == 0) {
                    velocity.y = wallJumpOff.y;
                    velocity.x = -wallDirectionX * wallJumpOff.x;
                } else {
                    velocity.y = wallLeap.y;
                    velocity.x = -wallDirectionX * wallLeap.x;
                }
            }
        if (controller.collisions.below) {
            velocity.y = jumpVelocityMax;
        }
    }

    public void OnJumpInputUp() {
        if (velocity.y > jumpVelocityMin) {
            velocity.y = jumpVelocityMin;
        }
    }

    void calculateLife()
    {
        healthBar.value = currentLife / maxLife; 
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    void HandleWallSliding()
    {
        wallDirectionX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;

        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeed)
            {
                velocity.y = -wallSlideSpeed;
            }
            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirectionX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    public void takeDamage(float damage)
    {
        currentLife -= damage;
        calculateLife();
        if(currentLife<=0)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void Attack()
    {
        //Debug.Log("Começo de animação");
        mAnimator.SetTrigger("Attack");
        attacking = true;
    }

    public void resetAttack()
    {
        attacking = false;
    }

    public bool isAttacking
    {
        get
        {
            return attacking;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisão - Player");

    }
}
