using UnityEngine;
using GoogleMobileAds.Api;

public class AdsInitializer : MonoBehaviour
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOsGameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;
    private string _gameId;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

}