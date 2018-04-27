using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jjinbbang : MonoBehaviour
{
    const float MOVE_RANGE = 12.5f;

    [SerializeField]
    public Transform _skin;
    public HamsterAnimationControl AnimControl { get { return _anim; } }

    HamsterAnimationControl _anim;

    public float moveSpeed;
    public bool gameOver;

    public LayerMask layerMask;
    public Rigidbody2D rigid;
    public Vector2 moveInput;
    
    RaycastHit2D hit;

    void Initialize()
    {
        moveSpeed = 2f;
        gameOver = false;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        _anim = _skin.GetComponent<HamsterAnimationControl>();
        _skin = transform.Find("skin");
        Initialize();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Vector2 GetAxisRaw로 입력 받음 
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 물리의 속도값으로 이동 애니메이션 실행여부 결정
        if (rigid.velocity.x == 0 && rigid.velocity.y == 0)
        {
            _anim.animator.SetBool("walking", false);
            rigid.velocity = Vector2.zero;
        }
        else
            _anim.animator.SetBool("walking", true);

        // 입력받은 거에 따라 스프라이트 뒤집음
        if (moveInput.x > 0)
            _anim.sprite.flipX = true;
        else if (moveInput.x < 0)
            _anim.sprite.flipX = false;

        // 이동
        rigid.velocity = moveInput * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -MOVE_RANGE, MOVE_RANGE), 
                                         Mathf.Clamp(transform.position.y, -MOVE_RANGE, MOVE_RANGE));
    }
}
