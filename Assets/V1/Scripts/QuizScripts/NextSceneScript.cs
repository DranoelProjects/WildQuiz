using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] GameObject winningCoins;
    [SerializeField] Button nextLvlButton;
    [SerializeField] Text textOuputCoinsValue, textMultiplier;
    int randomMultiplier = 0;
    int numberOfCoinsWon = 10;

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
}
