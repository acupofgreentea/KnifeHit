using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private LevelManager levelManager;

#if UNITY_ANDROID
    string gameId = "4548999";
#else
    string gameId = "4548998";
#endif
    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }
    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    public void PlayRewardedAd()
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
        else
        {
            Debug.Log("error");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "rewardedVideo" && showResult == ShowResult.Finished)
        {
            levelManager.ContinueWithRewardAd();
        }
    }
}
