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
    Ray ray;
    RaycastHit2D hit;

    void Initialize()
    {
        moveSpeed = 4f;
        gameOver = false;
    }

    void Start()
    {
        _anim = _skin.GetComponent<JBAnimationControl>();
        _skin = transform.Find("skin");
        Initialize();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!gameOver && !RayCheck(/*방향*/))
        {
            float moveX = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * moveX * moveSpeed * Time.deltaTime);
            float moveY = Input.GetAxis("Vertical");
            transform.Translate(Vector2.up * moveY * moveSpeed * Time.deltaTime);

            if(moveX == 0 && moveY == 0)
            {
                _anim.animator.SetBool("walking", false);

            }

            if (moveX < 0)
            {
                _anim.animator.SetBool("walking", true);
                _anim.sprite.flipX = false;
            }
            else if(moveX > 0)
            {
                _anim.animator.SetBool("walking", true);
                _anim.sprite.flipX = true;
            }
        }
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
