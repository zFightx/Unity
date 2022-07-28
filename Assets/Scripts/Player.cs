using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator anim;

    [DllImport("__Internal")]
    private static extern void JSAlert(string mensagem);

    [DllImport("__Internal")]
    private static extern int GetHello();

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    // Função que move o personagem
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0f)
        {
            Vector3 movement = new Vector3(horizontal, 0f, 0f);
            transform.position += movement * Time.deltaTime * Speed;

            anim.SetBool("walk", true);

            if(horizontal > 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }

            anim.SetBool("jump", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        JSAlert("Parabéns, você chegou ao checkpoint!");
        JSAlert("Recebendo dado JS: " + GetHello());
    }
}
