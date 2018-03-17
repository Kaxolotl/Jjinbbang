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
        if (!gameOver)
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
}
