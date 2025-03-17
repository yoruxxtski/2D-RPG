using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats;

    [Header("Bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI coinsTMP;


    [Header("Stats Panel")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI statLevelTMP;
    [SerializeField] private TextMeshProUGUI statDamageTMP;
    [SerializeField] private TextMeshProUGUI statCChanceTMP;
    [SerializeField] private TextMeshProUGUI statCDamageTMP;
    [SerializeField] private TextMeshProUGUI statTotalExpTMP;
    [SerializeField] private TextMeshProUGUI statCurrentExpTMP;
    [SerializeField] private TextMeshProUGUI statRequiredExpTMP;
    [SerializeField] private TextMeshProUGUI attributePointsTMP;
    [SerializeField] private TextMeshProUGUI strengthTMP;
    [SerializeField] private TextMeshProUGUI dexterityTMP;
    [SerializeField] private TextMeshProUGUI intelligenceTMP;

    [Header("Extra Panel")]
    [SerializeField] private GameObject npcQuestPanel;
    [SerializeField] private GameObject playerQuestPanel;
    [Header("Shop Panel")]
    [SerializeField] private GameObject shopPanel;

    [Header("Crafting Panel")]
    [SerializeField] private GameObject craftingPanel;




    private void Update() {
        UpdatePlayerUI();
    }

    private void UpdatePlayerUI() {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.Health / stats.MaxHealth, 10f * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, stats.Mana / stats.MaxMana, 10f * Time.deltaTime);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp / stats.NextLevelExp, 10f * Time.deltaTime);

        levelTMP.text = $"Level {stats.Level}";
        healthTMP.text = $"{stats.Health} / {stats.MaxHealth}";
        manaTMP.text = $"{stats.Mana} / {stats.MaxMana}";
        expTMP.text = $"{stats.CurrentExp} / {stats.NextLevelExp}";
        coinsTMP.text = CoinManager.Instance.Coins.ToString();
    }

    private void UpdateStatsPanel() {
        statLevelTMP.text = stats.Level.ToString();
        statDamageTMP.text = stats.totalDamage.ToString();
        statCChanceTMP.text = stats.criticalChance.ToString();
        statCDamageTMP.text = stats.criticalDamage.ToString();
        statTotalExpTMP.text = stats.totalExp.ToString();
        statCurrentExpTMP.text = stats.CurrentExp.ToString();
        statRequiredExpTMP.text = stats.NextLevelExp.ToString();
        attributePointsTMP.text =$"Points: {stats.AttributePoints.ToString()}";
        strengthTMP.text = stats.Strength.ToString();
        dexterityTMP.text = stats.Dexterity.ToString();
        intelligenceTMP.text = stats.Intelligence.ToString();
    }

    public void OpenCloseStatsPanel() {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if (statsPanel.activeSelf) {
            UpdateStatsPanel();
        }
    }

    private void UpgradeCallback() {
        UpdateStatsPanel();
    }

    private void ExtraInteractionCallback(InteractionType type) {
        switch(type) {
            case InteractionType.Quest:
                OpenCloseNPCQuestPanel(true);
                break;
            case InteractionType.Shop:
                OpenCloseShopPanel(true);
                break;
            case InteractionType.Crafting:
                OpenCloseCraftPanel(true);
                break;
        }
    }

    public void OpenCloseNPCQuestPanel(bool value) {
        npcQuestPanel.SetActive(value);
    }
    public void OpenClosePlayerQuestPanel(bool value) {
        playerQuestPanel.SetActive(value);
    }

    public void OpenCloseShopPanel(bool value) {
        shopPanel.SetActive(value);
    }

    public void OpenCloseCraftingPanel(bool value) {
        shopPanel.SetActive(value);
    }

    public void OpenCloseCraftPanel(bool value) {
        craftingPanel.SetActive(value);
    }

    void OnEnable()
    {
        PlayerUpgrade.OnPlayerUpgradeEvent += UpgradeCallback;
        PlayerExp.PlayerUpgradeEvent += UpgradeCallback;
        DialogueManager.OnExtraInteractionEvent += ExtraInteractionCallback;
    }

    void OnDisable()
    {
        PlayerUpgrade.OnPlayerUpgradeEvent -= UpgradeCallback;
        PlayerExp.PlayerUpgradeEvent += UpgradeCallback;
        DialogueManager.OnExtraInteractionEvent -= ExtraInteractionCallback;

    }
}
