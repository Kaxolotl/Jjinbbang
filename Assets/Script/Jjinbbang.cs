using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jjinbbang : MonoBehaviour
{
    [SerializeField]
    public Transform _skin;
    public JBAnimationControl AnimControl { get { return _anim; } }

    JBAnimationControl _anim;

    public float moveSpeed;
    public bool gameOver;

    public LayerMask layerMask;
    public Rigidbody2D rigid;
    public Vector2 moveInput;

    Vector2 moveVelocity;
    Ray ray;
    RaycastHit2D hit;

    void Initialize()
    {
        moveSpeed = 3f;
        moveVelocity = Vector3.zero;
        gameOver = false;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        _anim = _skin.GetComponent<JBAnimationControl>();
        _skin = transform.Find("skin");
        Initialize();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {

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
    }

    bool RayCheck(Vector2 JBdirection)
    {
        if (Physics2D.Raycast(gameObject.transform.position, JBdirection, 4f, layerMask))
        {
            Debug.Log(hit.collider.gameObject.name);

            hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            return true;
        }
        else
            return false;
    }
}
