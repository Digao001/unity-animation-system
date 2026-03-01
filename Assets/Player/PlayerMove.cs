using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Vector2 move;
    float speed = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private PlayerJump jump;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        jump = GetComponent<PlayerJump>();
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!GetComponent<Dash>().isDashing)
        {
            rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
        }
        AlterFace();
        Run();
    }

   private void AlterFace()
    {
        if(rb.linearVelocity.x > 0) 
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x), 
                transform.localScale.y);
        }
        if (rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector2(
                - Mathf.Abs(transform.localScale.x),
                transform.localScale.y);
        }
    }

    private void Run()
    {
        playerAnim.SetBool("run", move.x > 0 || move.x < 0 && jump.isJump == false);
    }

}
