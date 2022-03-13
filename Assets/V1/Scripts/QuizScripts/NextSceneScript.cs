using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] GameObject winningCoins;
    [SerializeField] Button nextLvlButton;
    [SerializeField] Text textOuputCoinsValue, textMultiplier;
    int randomMultiplier = 0;
    int numberOfCoinsWon = 10;
    private GameManager gameManager;
    private UIScript uiScript;

    private void Awake()
    {
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Start()
    {
        if (GameDataV2.NextLevel == 86 && GameDataV2.CurrentLevelData.Index == 85)
        {
            nextLvlButton.interactable = false;
        }
    }

    // Winning coins are just the number of coins that we display in the following scene panel
    public void ActiveWinningCoins(bool active, int value= 10)
    {
        textOuputCoinsValue.text = "+ " + value.ToString();
        winningCoins.SetActive(active);
        numberOfCoinsWon = value;
        randomMultiplier = Random.Range(2, 5);
        textMultiplier.text = randomMultiplier.ToString();
    }

    // Go back to level map
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // On click start the next level
    public void StartNextSceneLevel()
    {
        string btnLabel = nextLvlButton.GetComponentInChildren<Text>().text;
        if (GameDataV2.Hearts > 0)
        {
            if (btnLabel == "Rejouer")
            {
                SceneManager.LoadScene("TicTacToe");
            }
            else
            {
                gameManager.StartLevel(GameDataV2.CurrentLevelData.Index + 1, GameDataV2.CurrentLevelData.NextLevelTheme);
            }
        }
        else
        {
            uiScript.PanelNoHeart.SetActive(true);
            StartCoroutine(forceBackToMenu());
        }
    }

    // Wait 1 sec and then force back to menu
    IEnumerator forceBackToMenu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("LevelMap");
    }
}
