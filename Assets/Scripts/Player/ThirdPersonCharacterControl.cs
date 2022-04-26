using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    private enum State { IDLE, WALKING, RUNNING }

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float speedMultiplyer = 2.5f;

    private PlayerAnimatorController animatorController;

    private float speed;
    private State state = State.IDLE;
    private bool storyMode = false;

    public bool IsStoryMode { get => storyMode; set => storyMode = value; }

    private void Awake()
    {
        animatorController = gameObject.GetComponent<PlayerAnimatorController>();
    }

    private void Start()
    {
        speed = walkingSpeed;
    }

    private void Update()
    {
        if (!storyMode)
        {
            PlayerMovement();
            HandleAnimations();
            speed = state == State.WALKING ? walkingSpeed : walkingSpeed * speedMultiplyer;
        }
        else
        {
            HandleIdle();
        }
    }

    void PlayerMovement()
    {
        float ver = Input.GetAxis("Vertical");

        if (ver > 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            state = State.WALKING;
        }
        else if (ver > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            state = State.RUNNING;
        }
        else
        {
            state = State.IDLE;
        }

        Vector3 playerMovement = new Vector3(0f, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    private void HandleAnimations()
    {
        switch (state)
        {
            case State.WALKING: HandleWalk();
                break;
            case State.RUNNING: HandleRun();
                break;
            default: HandleIdle();
                return;
        }
    }

    private void HandleIdle()
    {
        animatorController.IdleAnim();
    }

    private void HandleWalk()
    {
        animatorController.WalkAnim();
    }

    private void HandleRun()
    {
        animatorController.RunAnim();
    }

    public void HandleDead()
    {
        animatorController.DeathAnim();
    }
}