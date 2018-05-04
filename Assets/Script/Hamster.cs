using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    const float LIMIT_DIS = 2; 

    [SerializeField]
    Transform _skin;
    HamsterAnimationControl AnimControl { get { return _anim; } }

    HamsterAnimationControl _anim;

    public Jjinbbang jjinbbang;
    Rigidbody2D _rigid;
    Vector2 jjinbbangPos;
    Vector2 relativePos;

    float _moveSpeed;
    bool _byRelativePos;
    bool _friend;

    void Initialize()
    {
        _moveSpeed = Random.Range(2.5f, 3f);
        _friend = false;
        _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Start()
    {
        jjinbbang = GameObject.Find("Jjinbbang").GetComponent<Jjinbbang>();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = _skin.GetComponent<HamsterAnimationControl>();
        _skin = transform.Find("skin");

        Initialize();
        InvokeRepeating("Move", 0, 0.1f);
    }

    public void BeFriend()
    {
        StartCoroutine(CanMoveHamster());
    }

    void Move()
    {
        // 찐빵이 동료가 아니라면
        if (!_friend)
            return;

        // 찐빵이 동료라면
        else
        {
            // 찐빵이와의 벡터 구함
            jjinbbangPos = jjinbbang.transform.position - transform.position;
            // 목표는 그 벡터에 랜덤 값을 더한 것
            relativePos = new Vector2(jjinbbangPos.x + Random.Range(-1, 1), jjinbbangPos.y + Random.Range(-1, 1));
            // 찐빵이와의 벡터가 일정 수치 이상이라면 이동
            if (Mathf.Abs(jjinbbangPos.x) >= LIMIT_DIS || Mathf.Abs(jjinbbangPos.y) >= LIMIT_DIS)
                _rigid.velocity = relativePos.normalized * _moveSpeed;
        }

        // rigid.velocity.x 값에 따라 좌우반전
        if (_rigid.velocity.x > 0)
            _anim.sprite.flipX = true;
        else if (_rigid.velocity.x < 0)
            _anim.sprite.flipX = false;

        // 속도값이 0이 아니면 walking
        if (_rigid.velocity.x == 0 && _rigid.velocity.y == 0)
            _anim.animator.SetBool("walking", false);
        else
            _anim.animator.SetBool("walking", true);
        
        // 목표와 거리가 가까우면 속도를 줄임
        if (relativePos.x - gameObject.transform.position.x <= 0.1f ||
            relativePos.y - gameObject.transform.position.y <= 0.1f)
        {
            _rigid.velocity *= 0.9f;
        }
    }

    // 찐빵에게 합류할 때 
    public IEnumerator CanMoveHamster()
    {
        if (_friend == true)
            yield return 0;

        yield return new WaitForSeconds(0.1f);
        // Freeze 해제
        _rigid.constraints = RigidbodyConstraints2D.None;
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        _friend = true;
        gameObject.tag = "Friend";

        // messageWindow 3초 생성, 내용 수정방법 생각해야됨
        UIManager.Instance.messageWindow.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        UIManager.Instance.messageWindow.gameObject.SetActive(false);

    }
}
