using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jjinbbang : MonoBehaviour
{
    const float MOVE_RANGE = 12.5f;

    [SerializeField] Transform _skin;
    [SerializeField] GameObject _jjinbbangActivity;

    public HamsterAnimationControl AnimControl { get { return _anim; } }

    HamsterAnimationControl _anim;

    public float moveSpeed;
    
    Rigidbody2D _rigid;
    Vector2 _moveInput;

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
        // 스페이스바 상호작용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Activity());
        }
    }

    IEnumerator Activity()
    {
        //찐빵주변의 상호작용콜라이더 0.1초 생성
        _jjinbbangActivity.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _jjinbbangActivity.gameObject.SetActive(false);
    }
}
