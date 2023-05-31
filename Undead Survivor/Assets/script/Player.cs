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

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        Vector2 _nextVec = inputVec * speed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + _nextVec);
    }

    private void LateUpdate() 
    {
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
}
