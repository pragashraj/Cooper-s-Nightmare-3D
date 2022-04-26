using UnityEngine;

public class CharacterFloraController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void HandleStandAndIdle()
    {
        animator.SetTrigger("Stand");
    }

    public void HandleIdleAnim()
    {
        animator.SetFloat("Movement", 0f, 0.1f, Time.deltaTime);
    }

    public void HandleWalkAnim()
    {
        animator.SetFloat("Movement", 0.5f, 0.1f, Time.deltaTime);
    }

    public void HandleRunAnim()
    {
        animator.SetFloat("Movement", 1f, 0.1f, Time.deltaTime);
    }
}