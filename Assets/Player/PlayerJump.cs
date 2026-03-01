using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isJump = false;
    float jumpForce = 10f;
    public float time;
    public Animator playerAnim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ActionController.OnJump += Jump;
    }

    private void OnDisable()
    {
        ActionController.OnJump -= Jump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
            playerAnim.SetBool("jump", isJump);
            JumpGravity(1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = true;
        }
    }

    
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            ActionController.OnJump?.Invoke();
        }
    }

   private void Jump()
    {
        if (isJump) return;

        isJump = true;
        playerAnim.SetBool("jump", isJump);
        StartCoroutine(JumpGravity(2));
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("executa dps do Jump");
    }

    private IEnumerator JumpGravity(int value) 
    {
        rb.gravityScale = value;
        yield return new WaitUntil(() => !isJump);
        rb.gravityScale = 1;

    } 



}
