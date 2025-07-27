using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCombat : MonoBehaviour
{
    [Header("Infos de base")]
    public string characterName = "Player";
    public int maxHP = 100;
    public int currentHP;

    public int maxMana = 100;
    public int currentMana;

    public int baseAttack = 10;
    public int initiative = 1;
    public float currentInitiative = 0f;

    [Header("UI Elements")]
    public Slider initBar;
    public Slider hpBar;
    public Slider manaBar;
    public TMP_Text hpText;
    public TMP_Text manaText;

    [Header("Statut")]
    public bool isPlayer = false;
    public bool hasFinishedTurn = false;

    void Start()
    {
        currentHP = maxHP;
        currentMana = maxMana;

        // Init UI si assignés
        UpdateUI();
    }

    void Update()
{
	
    UpdateUI();
}

    void UpdateUI()
    {
        if (initBar != null)
            initBar.value = currentInitiative;

	if (hpBar != null)
            hpBar.value = currentHP;

	if (manaBar != null)
            manaBar.value = currentMana;

        if (hpText != null)
            hpText.text = $"HP: {currentHP} / {maxHP}";

        if (manaText != null)
            manaText.text = $"Mana: {currentMana} / {maxMana}";
    }

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);
        UpdateUI();
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public void EnablePlayerControl()
    {
        hasFinishedTurn = false;
        // Affichage de l’UI action, activé depuis le CombatManager
    }

    public void EndPlayerTurn()
    {
        hasFinishedTurn = true;
        // Masquage de l’UI action
    }

    public void DoEnemyAction(CharacterCombat target)
    {
        Debug.Log($"{characterName} attaque !");
        target.TakeDamage(baseAttack);
    }
}
