using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Config")]
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponManaTMP;

    public void EquipWeapon(Weapons weapon) {
        weaponIcon.sprite = weapon.Icon;
        weaponIcon.gameObject.SetActive(true);
        weaponManaTMP.text = weapon.RequiredMana.ToString();
        weaponManaTMP.gameObject.SetActive(true);
        GameManager.Instance.Player.PlayerAttack.EquipWeapon(weapon);
    }

}