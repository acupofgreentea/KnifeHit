using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject knivesPanel; 
    [SerializeField] private GameObject knifeIcon;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject bossLevelText;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Sprite muteIcon;
    [SerializeField] private Sprite soundOnIcon;

    [SerializeField] private Button soundButton;

    private bool muted;

    private void OnEnable()
    {
        LevelManager.UIOnNextLevel += ShowKnivesPanel;
    }
    private void OnDisable()
    {
        LevelManager.UIOnNextLevel -= ShowKnivesPanel;
    }

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        soundButton.onClick.AddListener(MuteAudio);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowBossLevelText()
    {
        bossLevelText.SetActive(true);
    }

    public void HideBossLevelText()
    {
        bossLevelText.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowKnivesPanel(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(knifeIcon, knivesPanel.transform);
        }
    }

    // Destroy knives from knivespanel for continue with reward ad
    // DestroyUsedKnives() destroy knives from knivespanel when i throw a knife
    public void DestroyAllKnives()
    {
        if(knivesPanel.transform.childCount != 0)
        {
            foreach (Transform children in knivesPanel.transform)
            {
                Destroy(children.gameObject);
            }
        }
    }
    public void DestroyUsedKnives()
    {
        Destroy(knivesPanel.transform.GetChild(0).gameObject); 
    }

    public void MuteAudio()
    {
        if(muted)
        {
            muted = !muted;
            AudioListener.pause = muted;
            soundButton.image.sprite = soundOnIcon;
        }
        else
        {
            muted = !muted;
            AudioListener.pause = muted;
            soundButton.image.sprite = muteIcon;
        }
    }
}
