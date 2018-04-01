using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jjinbbang : MonoBehaviour
{
    [SerializeField] public Transform _skin;
    public JBAnimationControl AnimControl { get { return _anim; } }

    JBAnimationControl _anim;

    public float moveSpeed;
    public bool gameOver;

    public LayerMask layerMask;
    public Rigidbody2D rigid;
    Ray ray;
    RaycastHit2D hit;

    void Initialize()
    {
        moveSpeed = 300f;
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
        Vector2 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector2.left;
            _anim.sprite.flipX = false;
            rigid.velocity = Vector2.zero;
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                moveVelocity += Vector2.down;
                rigid.velocity = Vector2.zero;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                moveVelocity += Vector2.up;
                rigid.velocity = Vector2.zero;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector2.right;
            _anim.sprite.flipX = true;
            rigid.velocity = Vector2.zero;
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                moveVelocity += Vector2.down;
                rigid.velocity = Vector2.zero;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                moveVelocity += Vector2.up;
                rigid.velocity = Vector2.zero;
            }
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            moveVelocity += Vector2.down;
            rigid.velocity = Vector2.zero;
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                moveVelocity += Vector2.left;
                _anim.sprite.flipX = false;
                rigid.velocity = Vector2.zero;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                moveVelocity += Vector2.right;
                _anim.sprite.flipX = true;
                rigid.velocity = Vector2.zero;
            }
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            moveVelocity += Vector2.up;
            rigid.velocity = Vector2.zero;
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                moveVelocity += Vector2.left;
                _anim.sprite.flipX = false;
                rigid.velocity = Vector2.zero;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                moveVelocity += Vector2.right;
                _anim.sprite.flipX = true;
                rigid.velocity = Vector2.zero;
            }
        }

        if (moveVelocity.x == 0 && moveVelocity.y == 0)
        {
            _anim.animator.SetBool("walking", false);
            rigid.velocity = Vector2.zero;
        }
        else
            _anim.animator.SetBool("walking", true);


        rigid.AddForce(moveVelocity.normalized * moveSpeed);
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
