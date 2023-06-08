using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 8;
    private Rigidbody2D playerRB;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCtrls;

    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() 
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCtrls[GameManager.Instance().playerId];
    }

    private void FixedUpdate() 
    {
        if(!GameManager.Instance().isAlive)
            return;
        Vector2 _nextVec = inputVec * speed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + _nextVec);
    }

    private void LateUpdate() 
    {
        if(!GameManager.Instance().isAlive)
            return;
        if(inputVec.x != 0)
        {   
            spriteRenderer.flipX = (inputVec.x < 0);
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        animator.SetBool("isMove", inputVec.magnitude > 0.01);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(!GameManager.Instance().isAlive)
            return;

        GameManager.Instance().hp -= Time.deltaTime * 10;

        if(GameManager.Instance().hp < 0)
        {
            for(int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.Instance().GameOver();
        }
    }
}
