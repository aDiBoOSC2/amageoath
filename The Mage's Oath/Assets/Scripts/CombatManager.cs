using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public CharacterCombat player;
    public CharacterCombat enemy;

    public Spell[] playerSpells;

    public TMP_Text logText;
    public Button[] spellButtons;

    private bool isPlayerTurn = false;
    private bool waitingForPlayerInput = false;

    void Start()
    {
        // Initialise l'initiative
        player.currentInitiative = 0;
        enemy.currentInitiative = 0;

        // Configure les boutons
        for (int i = 0; i < spellButtons.Length; i++)
        {
            int index = i;
            spellButtons[i].GetComponentInChildren<TMP_Text>().text = playerSpells[i].name;
            spellButtons[i].onClick.AddListener(() => PlayerCastSpell(index));
        }

        DisableButtons();
    }

    void Update()
    {
        ProcessTurn(Time.deltaTime);
    }

    void ProcessTurn(float deltaTime)
    {
        // Si l’un des deux est mort, on ne fait rien
        if (player.IsDead() || enemy.IsDead()) return;

        // Si on attend que le joueur clique, on ne continue pas à remplir l'initiative
        if (waitingForPlayerInput) return;

        player.currentInitiative += player.initiative * deltaTime;
        enemy.currentInitiative += enemy.initiative * deltaTime;

        if (player.currentInitiative >= 100)
        {
            player.currentInitiative -= 100;
            waitingForPlayerInput = true;
            EnableButtons();
            logText.text = "À toi de jouer !";
        }
        else if (enemy.currentInitiative >= 100)
        {
            enemy.currentInitiative -= 100;
            EnemyTurn();
        }
    }

    void PlayerCastSpell(int spellIndex)
    {
        if (!waitingForPlayerInput) return;

        Spell spell = playerSpells[spellIndex];
        if (player.currentMana < spell.manaCost)
        {
            logText.text = "Pas assez de mana !";
            return;
        }

        player.currentMana -= spell.manaCost;
        enemy.TakeDamage(spell.damage);
        logText.text = $"Tu lances {spell.name}, infliges {spell.damage} dégâts.";

        if (enemy.IsDead())
        {
            logText.text += "\nEnnemi vaincu !";
            DisableButtons();
            return;
        }

        DisableButtons();
        waitingForPlayerInput = false;
    }

    void EnemyTurn()
    {
        int damage = Random.Range(5, 15);
        player.TakeDamage(damage);
        logText.text = $"L'ennemi attaque et inflige {damage} dégâts.";

        if (player.IsDead())
        {
            logText.text += "\nTu es mort...";
            DisableButtons();
        }
    }

    void EnableButtons()
    {
        foreach (var b in spellButtons)
            b.interactable = true;
    }

    void DisableButtons()
    {
        foreach (var b in spellButtons)
            b.interactable = false;
    }
}
