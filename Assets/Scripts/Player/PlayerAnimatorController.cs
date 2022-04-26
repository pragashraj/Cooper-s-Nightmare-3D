using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void IdleAnim()
    {
        animator.SetFloat("Movement", 0f, 0.1f, Time.deltaTime);
    }

    public void WalkAnim()
    {
        animator.SetFloat("Movement", 0.5f, 0.1f, Time.deltaTime);
    }

    public void RunAnim()
    {
        animator.SetFloat("Movement", 1f, 0.1f, Time.deltaTime);
    }

    public void DeathAnim()
    {
        animator.SetTrigger("Death");
    }
}