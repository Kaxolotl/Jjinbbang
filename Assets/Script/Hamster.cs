using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    [SerializeField]
    public Transform _skin;
    public HamsterAnimationControl AnimControl { get { return _anim; } }

    HamsterAnimationControl _anim;

    public Jjinbbang jjinbbang;
    Rigidbody2D _rigid;
    Vector2 jjinbbangPos;
    Vector2 relativePos;

    CircleCollider2D _circleCol;
    float _moveSpeed;
    bool _byRelativePos;
    bool _byJjinbbang;

    void Initialize()
    {
        _moveSpeed = Random.Range(2.5f, 3f);
        _byJjinbbang = false;
    }

    void Start()
    {
        jjinbbang = GameObject.Find("Jjinbbang").GetComponent<Jjinbbang>();
        _rigid = GetComponent<Rigidbody2D>();
        _circleCol = GetComponent<CircleCollider2D>();
        _anim = _skin.GetComponent<HamsterAnimationControl>();
        _skin = transform.Find("skin");

        Initialize();
        InvokeRepeating("Move", 0, 0.1f);
    }

    private void Update()
    {
    }

    void Move()
    {
        // 찐빵이 범위 밖이면 
        if (!_byJjinbbang)
        {        
            // 플레이어와의 벡터 구함
            jjinbbangPos = jjinbbang.transform.position - transform.position;
            // 목표는 그 벡터에 랜덤 값을 더한 것
            relativePos = new Vector2(jjinbbangPos.x + Random.Range(-1, 1), jjinbbangPos.y + Random.Range(-1, 1));

            if(_rigid.velocity.x <= 3 && _rigid.velocity.y <= 3)
            _rigid.velocity = relativePos.normalized * _moveSpeed;
        }

        // rigid.velocity.x 값에 따라 좌우반전
        if (_rigid.velocity.x > 0)
            _anim.sprite.flipX = true;
        else if (_rigid.velocity.x < 0)
            _anim.sprite.flipX = false;

        // 목표와 거리가 가까우면 속도를 줄임
        if (relativePos.x - gameObject.transform.position.x <= 0.1f ||
            relativePos.y - gameObject.transform.position.y <= 0.1f)
        {
            _rigid.velocity *= 0.9f;
        }

        // 속도값이 0이 아니면 walking
        if (_rigid.velocity.x == 0 && _rigid.velocity.y == 0)
        {
            _anim.animator.SetBool("walking", false);
        }
        else
            _anim.animator.SetBool("walking", true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("JjinbbangSight"))
        {
            _byJjinbbang = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("JjinbbangSight"))
        {
            _byJjinbbang = false;
        }
    }
}
