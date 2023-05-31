using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed;
    public float monsterHP;
    public float maxHP;
    public RuntimeAnimatorController[] animCtrl;
    Animator animator;
    public Rigidbody2D target;
    

    bool isAlive;
    private Rigidbody2D monsterRB;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        monsterRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        target = GameManager.Instance().player.GetComponent<Rigidbody2D>();
        isAlive = true;
        monsterHP = maxHP;
    }
    private void FixedUpdate() 
    {
        if(!isAlive)
            return;

        Vector2 _dirVec = (target.position - monsterRB.position).normalized;
        Vector2 _nextVec = _dirVec * speed * Time.fixedDeltaTime;

        monsterRB.MovePosition(monsterRB.position + _nextVec);
        monsterRB.velocity = Vector2.zero;
    }

    private void LateUpdate() 
    {
        if(!isAlive)
            return;

        Vector2 _nextDir = (target.position - monsterRB.position).normalized;

        if(_nextDir.x == 0)
            return;

        spriteRenderer.flipX = (_nextDir.x < 0);
    }
    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animCtrl[data.monsterType];
        speed = data.monsterSpeed;
        maxHP = data.monsterHP;
        monsterHP = data.monsterHP;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Bullet"))
            return;

        monsterHP -= other.GetComponent<Bullet>().damage;
        
        if(monsterHP > 0)
        {
            // Alive, hit action
            isAlive = true;
            animator.SetTrigger("Hit");
        }
        else
        {
            // Die
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
