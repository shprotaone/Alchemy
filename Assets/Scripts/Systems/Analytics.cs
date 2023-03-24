using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    private const string dayName = "day";
    private const string pointName = "pointResult";
    private const string maxPointName = "maxPointResult";
    private const string leftPointName = "leftPoint";
    private const string isLevelComplete = "levelComplete";
    private const string nameOfCustomEvent = "endDayResult";

    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
            Debug.Log("InitComplete");
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.Message);
        }
    }

    public void SendAnalytics(string dayNumber, int point, int taskPoint, bool levelComplete)
    {
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            {dayName,dayNumber},
            {pointName,point},
            {maxPointName,taskPoint},
            {leftPointName, (taskPoint - point) },
            {isLevelComplete,levelComplete }
        };

        AnalyticsService.Instance.CustomData(nameOfCustomEvent, data);

        Debug.Log("Complete");
    }
}
