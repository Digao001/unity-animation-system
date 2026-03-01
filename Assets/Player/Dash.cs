using UnityEngine;
using UnityEngine.InputSystem;
public class Dash : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    public float dashForce = 30f;
    public Rigidbody2D rb;
    public bool isDashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ActionController.OnDash += DashPlayer;
    }

    private void OnDisable()
    {
        ActionController.OnDash -= DashPlayer;
    }

    public void OnDash(InputValue value)
    {
        ActionController.OnDash?.Invoke();
    }

    private void DashPlayer()
    {
        if (isDashing) return;

        isDashing = true;
        playerAnim.SetBool("dash", isDashing);
        float direcao = transform.localScale.x > 0 ? 1 : -1;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(direcao * dashForce, 0f), ForceMode2D.Impulse);

        Invoke(nameof(StopDash), 0.2f);
    }

    private void StopDash()
    {
        isDashing = false;
        playerAnim.SetBool("dash", isDashing);
    }

}
