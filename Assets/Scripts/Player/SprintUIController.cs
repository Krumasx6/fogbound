using UnityEngine;

public class SprintUIController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public RectTransform sprintBar;

    [Header("Bar Settings")]
    public float maxValue = 500f;

    private float leftValue = 0f;
    private float rightValue = 0f;
    void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = FindFirstObjectByType<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleSprintValues();
        UpdateUI();
    }

    private void HandleSprintValues()
    {
        float staminaUsed = playerMovement.maxStamina - playerMovement.currentStamina;
        float staminaUsedPercent = staminaUsed / playerMovement.maxStamina;
        
        leftValue = staminaUsedPercent * maxValue;
        rightValue = staminaUsedPercent * maxValue;
    }

    private void UpdateUI()
    {
        if (sprintBar != null)
        {
            sprintBar.offsetMin = new Vector2(leftValue, sprintBar.offsetMin.y);
            sprintBar.offsetMax = new Vector2(-rightValue, sprintBar.offsetMax.y);
        }
    }

    public float GetLeftValue() => leftValue;
    public float GetRightValue() => rightValue;
}
