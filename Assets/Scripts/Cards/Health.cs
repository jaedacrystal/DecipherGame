using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth;
    public int currentHealth;
    public string ownerTag;

    [Header("Text Placeholders")]
    public TextMeshProUGUI prompt;
    public TextMeshProUGUI turnText;

    [Header("Health")]
    public HealthBar healthBar;
    public TextMeshProUGUI healthBarText;

    private string playerName;
    private bool playerCheck;

    public bool isPlayer = false;
    public LevelLoader start;

    private GameObject player;

    private void Start()
    {
        prompt.gameObject.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(true);

        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void TakeDamage(int damage)
    {
        PlayerStats stats = GetComponent<PlayerStats>();

        int previousHealth = currentHealth;
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        DOTween.To(() => previousHealth, x =>
        {
            healthBar.SetHealth(x);
            healthBarText.text = $"{x}/{maxHealth}";
        }, currentHealth, 0.5f).SetEase(Ease.OutQuad);

        if (stats != null && stats.isInvulnerable)
        {
            Debug.Log(stats.name + " blocked the damage!");
            return;
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        int previousHealth = currentHealth;
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        DOTween.To(() => previousHealth, x =>
        {
            healthBar.SetHealth(x);
            healthBarText.text = $"{x}/{maxHealth}";
        }, currentHealth, 0.5f).SetEase(Ease.OutQuad);
    }

    private IEnumerator ApplyShieldAndRetaliate(PlayerStats targetStats, Health targetHealth)
    {
        targetStats.isInvulnerable = true;

        yield return new WaitForSeconds(1.5f);

        targetStats.isInvulnerable = false;

        targetHealth.TakeDamage(5);
    }

    private void Die()
    {
        playerCheck = isPlayer;
        playerName = playerCheck ? PlayerPrefs.GetString("PlayerName", "Player") : ownerTag;

        prompt.text = playerName + " has been defeated";
        prompt.gameObject.SetActive(true);
        turnText.gameObject.SetActive(false);

        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            gameObject.SetActive(false);
            healthBar.gameObject.SetActive(false);
        });

        //if (!isPlayer)
        //{
        //    PlayerPrefs.SetInt("NPC_Dialogue_Index", 2);
        //    PlayerPrefs.Save();

        //    start.LoadPreviousScene();
        //}
    }
}
