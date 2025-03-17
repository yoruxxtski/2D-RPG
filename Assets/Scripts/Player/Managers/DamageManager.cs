using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{
    [Header("Config")]
    [SerializeField] private DamageText damageText;

    public void ShowDamageText(float damageAmount, Transform parent) {
        DamageText text = Instantiate(damageText, parent);
        text.transform.position += Vector3.right * 0.5f;
        text.SetDamageText(damageAmount);
    }
}