using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class InitializeAnalytics : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("consent approved");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            Debug.Log("consent not approved");
        }
    }
}
