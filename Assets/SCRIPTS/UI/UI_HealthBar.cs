using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity;
    private RectTransform myTransform;
    private CharacterStats myStats;
    private Slider slider;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<CharacterStats>();

        entity.onFlipped += FlipUI;
        myStats.onHealthChange += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHPValue();
        slider.value = myStats.currentHealth;
    }


    private void FlipUI() => myTransform.Rotate(0, 180, 0);
    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        myStats.onHealthChange -= UpdateHealthUI;
    }
}
