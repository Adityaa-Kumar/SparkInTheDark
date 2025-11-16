using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("TextMeshProUGUI component to display coin count")]
    public TextMeshProUGUI coinText;

    [Header("Coin Settings")]
    [Tooltip("Starting number of coins")]
    public int startingCoins = 0;

    [Header("Win Condition")]
    [Tooltip("Number of coins needed to win")]
    public int coinsToWin = 83;

    [Tooltip("Win panel to show when player wins")]
    public GameObject winPanel;

    private int currentCoins;
    private bool hasWon = false;

    // Singleton instance
    public static CoinManager Instance { get; private set; }

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentCoins = startingCoins;
        UpdateCoinUI();

        // Make sure win panel is disabled at start
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    public void AddCoins(int amount)
    {
        if (hasWon) return; // Don't add coins after winning

        currentCoins += amount;
        UpdateCoinUI();

        // Check win condition
        if (currentCoins >= coinsToWin)
        {
            TriggerWin();
        }
    }

    public void RemoveCoins(int amount)
    {
        currentCoins -= amount;
        if (currentCoins < 0) currentCoins = 0;
        UpdateCoinUI();
    }

    public int GetCoinCount()
    {
        return currentCoins;
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {currentCoins}/{coinsToWin}";
        }
        else
        {
            Debug.LogWarning("Coin Text UI not assigned in CoinManager!");
        }
    }

    void TriggerWin()
    {
        if (hasWon) return; // Prevent multiple win triggers

        hasWon = true;

        // Pause the game
        Time.timeScale = 0f;

        // Show win panel
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Win Panel not assigned in CoinManager!");
        }

        Debug.Log("You Win!");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
