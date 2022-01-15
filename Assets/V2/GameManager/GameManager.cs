using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] GameObject music;
    [SerializeField] AudioClip sndClick;
    private AudioSource audioSource;

    // API
    private ApiManager apiManager;
    void Awake()
    {
        //removing unnecessary GameManager
        int numGameManager = FindObjectsOfType<GameManager>().Length;
        if (numGameManager != 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(gameObject);

        apiManager = gameObject.GetComponent<ApiManager>();

        //init music
        if (!GameObject.FindGameObjectWithTag("Music"))
        {
            audioSource = Instantiate(music).GetComponent<AudioSource>();
        }
    }

    public async void StartLevel(int levelIndex, string theme)
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);

        await apiManager.GetLevel(levelIndex, theme);
        Level level = GameDataV2.CurrentLevelData;

        // Starts the corresponding scene 
        switch (level.Type)
        {
            case "Quiz":
                SceneManager.LoadScene("QuizScene");
                break;
            case "InputQuiz":
                SceneManager.LoadScene("QuizScene");
                break;
            case "TicTacToe":
                SceneManager.LoadScene("TicTacToe");
                break;
            case "Taquin":
                SceneManager.LoadScene("Taquin");
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("NextLevel", GameDataV2.NextLevel);
    }
}
