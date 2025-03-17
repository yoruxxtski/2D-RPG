using UnityEngine;

public class EnemySelected : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject SelectorSprite;

    private EnemyBrain enemyBrain;
    void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    private void EnemySelectedCallBack(EnemyBrain enemySelected) {
        if (enemySelected == enemyBrain) {
            SelectorSprite.SetActive(true);
        } else {
            SelectorSprite.SetActive(false);
        }
    }

    public void NoSelectedCallBack() {
        SelectorSprite.SetActive(false);
    }

    void OnEnable()
    {
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallBack;
        SelectionManager.OnNoSelectedEvent += NoSelectedCallBack;
    }

    void OnDisable()
    {
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallBack;
        SelectionManager.OnNoSelectedEvent -= NoSelectedCallBack;
    }
}