using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] 
    GameObject knivesPanel; 

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField] 
    GameObject knifeIcon;

    [SerializeField] Sprite muteIcon;
    [SerializeField] Sprite soundOnIcon;

    [SerializeField] Button soundButton;

    private bool muted;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        soundButton.onClick.AddListener(MuteAudio);

        if(AudioListener.pause == false)
        {
            soundButton.image.sprite = soundOnIcon;
        }
        else
        {
            soundButton.image.sprite = muteIcon;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }

    public void GameOverPanel()
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
    public void DecreaseUsedKnives()
    {
        Destroy(knivesPanel.transform.GetChild(0).gameObject); 
    }

    public void MuteAudio()
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
            soundButton.image.sprite = muteIcon;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
            soundButton.image.sprite = soundOnIcon;
        }
    }
}
