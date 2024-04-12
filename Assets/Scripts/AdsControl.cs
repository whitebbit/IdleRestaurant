using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SocialPlatforms;
using System;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class AdsControl : MonoBehaviour
{


    protected AdsControl()
    {
    }

    private static AdsControl _instance;

    public static AdsControl Instance { get { return _instance; } }

    void Awake()
    {
        if (FindObjectsOfType(typeof(AdsControl)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); //Already done by CBManager


    }




    public void showAds()
    {
    
        
    }



    public void ShowRewardVideo()
    {
    

    }

    public void PlayCallbackRewardVideo(Action<ShowResult> _action)
    {
        
    }

   public void PlayDelegateRewardVideo(Action<bool> onVideoPlayed)
    {
      
    
    }
}

