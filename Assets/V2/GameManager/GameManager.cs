using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] GameObject music;
    [SerializeField] AudioClip sndClick;
    private AudioSource audioSource;
    SoundManager soundManager;
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

        // Caching objects
        soundManager = GameObject.FindGameObjectWithTag("Music").GetComponent<SoundManager>();
    }

    void Start()
    {
        //Initializing callback for active scene changes
        SceneManager.activeSceneChanged += changedActiveScene;

        Mute(GameDataV2.Mute);
    }

    public async void StartLevel(int levelIndex, string theme)
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);

        await apiManager.GetLevel(levelIndex, theme);
        StartCoroutine(waitDuringApiCallToGetLevel());
    }

    //This function can be called to mute the all audio sources
    public void Mute(bool val)
    {
        AudioSource[] sources;
        sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource asource in sources)
        {
            asource.mute = val;
        }
    }

    //Callback used in order to stop the music if we are not in levels map
    void changedActiveScene(Scene current, Scene next)
    {
        if (next.buildIndex > 0)
        {
            soundManager.StopMusic();
        }
        else
        {
            soundManager.PlayMusic();
        }
    }

    IEnumerator waitDuringApiCallToGetLevel()
    {
        yield return new WaitForSeconds(0.4f);
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
        PlayerPrefs.SetInt("CoinsNumber", GameDataV2.Coins);
        PlayerPrefs.SetInt("HeartsNumber", GameDataV2.Hearts);
        PlayerPrefs.SetInt("NumberLostLevels", GameDataV2.NumberLostLevels);
        PlayerPrefs.SetInt("NumberWonLevels", GameDataV2.NumberWonLevels);
        PlayerPrefs.SetInt("mute", (GameDataV2.Mute ? 1 : 0));
    }
}
