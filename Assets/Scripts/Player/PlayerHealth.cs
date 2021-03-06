using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image content;
    [SerializeField] private float lerpSpeed;

    private ThirdPersonCharacterControl thirdPersonCharacterControl;
    private PlayerAnimatorController animatorController;

    private float health = 100;
    private bool isDead;

    public float Health { get => health; set => health = value; }

    private void Awake()
    {
        thirdPersonCharacterControl = gameObject.GetComponent<ThirdPersonCharacterControl>();
        animatorController = gameObject.GetComponent<PlayerAnimatorController>();
    }

    void Update()
    {
        float amount = Map(Health, 100, 1);
        content.fillAmount = Mathf.Lerp(content.fillAmount, amount, Time.deltaTime * lerpSpeed);

        if (health <= 0 && !isDead)
        {
            isDead = true;
            animatorController.DeathAnim();
            thirdPersonCharacterControl.enabled = false;
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }

    private float Map(float value, float max, float min)
    {
        return value * min / max;
    }

    public void IncreaseHealthValue(float count)
    {
        if (count <= 100)
        {
            float healthTemp = health + count;

            if (healthTemp > 100)
            {
                health = 100;
            }
            else
            {
                health = healthTemp;
            }
        }
    }

    public void DecreaseHealthValue(float count)
    {
        if (count >= 0)
        {
            float healthTemp = health - count;

            if (healthTemp < 0)
            {
                health = 0;
            }
            else
            {
                health = healthTemp;
            }
        }
    }
}