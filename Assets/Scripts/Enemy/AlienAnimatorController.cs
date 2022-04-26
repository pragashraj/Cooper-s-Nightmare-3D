using UnityEngine;

public class AlienAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void CrouchToStandAnim()
    {
        animator.SetTrigger("CrouchToStand");
    }
}