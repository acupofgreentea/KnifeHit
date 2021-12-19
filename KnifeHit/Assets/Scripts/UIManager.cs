using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
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
    void Start()
    {
        soundButton.onClick.AddListener(MuteAudio);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void GameOverPanel()
    {
        scoreText.text = GameManager.Instance.score.ToString();
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
