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
    
    Rigidbody2D _rigid;
    Vector2 _moveInput;

    bool _canGotcha;

    void Initialize()
    {
        moveSpeed = 2f;
    }

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = _skin.GetComponent<HamsterAnimationControl>();
        _skin = transform.Find("skin");
        Initialize();
    }

    void FixedUpdate()
    {
        Move();
        JjinbbangActivity();
    }

    void Move()
    {
        // Vector2 GetAxisRaw로 입력 받음 
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 물리의 속도값으로 이동 애니메이션 실행여부 결정
        if (_rigid.velocity.x == 0 && _rigid.velocity.y == 0)
        {
            _anim.animator.SetBool("walking", false);
            _rigid.velocity = Vector2.zero;
        }
        else
            _anim.animator.SetBool("walking", true);

        // 입력받은 거에 따라 스프라이트 뒤집음
        if (_moveInput.x > 0)
            _anim.sprite.flipX = true;
        else if (_moveInput.x < 0)
            _anim.sprite.flipX = false;

        // 이동
        _rigid.velocity = _moveInput * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -MOVE_RANGE, MOVE_RANGE), 
                                         Mathf.Clamp(transform.position.y, -MOVE_RANGE, MOVE_RANGE));
    }

    void JjinbbangActivity()
    {
        // 스페이스바로 상호작용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 햄스터갓챠 가능한가?
            if (_canGotcha)
            {
                GameManager.Instance.getHamster = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Hamster"))
        {
            _canGotcha = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Hamster"))
        {
            _canGotcha = false;
        }
    }
}
