using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject knivesPanel; 
    [SerializeField] private GameObject knifeIcon;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Sprite muteIcon;
    [SerializeField] private Sprite soundOnIcon;

    [SerializeField] private Button soundButton;

    private bool muted;

    void OnEnable()
    {
        LevelManager.UIOnNextLevel += ShowKnivesPanel;
    }
    void OnDisable()
    {
        LevelManager.UIOnNextLevel -= ShowKnivesPanel;
    }

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        soundButton.onClick.AddListener(MuteAudio);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowGameOverPanel()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        gameOverPanel.SetActive(true);
    }

    public void ShowKnivesPanel(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(knifeIcon, knivesPanel.transform);
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
