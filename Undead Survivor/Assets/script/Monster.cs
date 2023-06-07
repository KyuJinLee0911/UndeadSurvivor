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
    WaitForFixedUpdate wait;
    

    bool isAlive;
    private Rigidbody2D monsterRB;
    private Collider2D monsterColl;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        monsterRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterColl = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

    private void OnEnable() 
    {
        target = GameManager.Instance().player.GetComponent<Rigidbody2D>();
        isAlive = true;
        monsterHP = maxHP;

        // 죽을 때 설정한 것들 반대로
        monsterColl.enabled = true;
        monsterRB.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
    }
    private void FixedUpdate() 
    {
        if(!isAlive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        // 살아있는 동안에 플레이어를 따라다니게 함
        Vector2 _dirVec = (target.position - monsterRB.position).normalized;
        Vector2 _nextVec = _dirVec * speed * Time.fixedDeltaTime;

        monsterRB.MovePosition(monsterRB.position + _nextVec);
        monsterRB.velocity = Vector2.zero;
    }

    private void LateUpdate() 
    {
        if(!isAlive)
            return;

        // 살아있는 동안 캐릭터를 바라보게 함(좌우)
        Vector2 _nextDir = (target.position - monsterRB.position).normalized;

        if(_nextDir.x == 0)
            return;

        spriteRenderer.flipX = (_nextDir.x < 0);
    }

    // 레벨에 맞는 몬스터 데이터 설정
    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animCtrl[data.monsterType];
        speed = data.monsterSpeed;
        maxHP = data.monsterHP;
        monsterHP = data.monsterHP;
    }

    // 총알에 맞았을 때
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!GameManager.Instance().isAlive)
            return;
            
        if(!other.CompareTag("Bullet") || !isAlive)
            return;

        monsterHP -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        // 체력이 0보다 크면 맞는 애니메이션
        if(monsterHP > 0)
        {
            // Alive, hit action
            isAlive = true;
            animator.SetTrigger("Hit");
        }
        // 체력이 0보다 작으면 죽음
        else
        {
            // Die
            isAlive = false;
            monsterColl.enabled = false;
            monsterRB.simulated = false;
            spriteRenderer.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.Instance().killCount++;
            GameManager.Instance().GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 하나의 물리 프레임을 딜레이

        Vector3 _playerPos = GameManager.Instance().player.transform.position;
        Vector3 _dir = (transform.position - _playerPos).normalized;

        monsterRB.AddForce(_dir * 3, ForceMode2D.Impulse);
    }

    

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
