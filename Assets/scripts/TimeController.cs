using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Oculus.Interaction.Context;

/// <summary>
/// This class rotates the "sun" (main directional light) around the x-axis depending on the time of day.
/// </summary>
public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    [SerializeField] private float timeMultiplier; // controls how fast time passes
    [SerializeField] private float startHour; // the time of day the day/night cycle starts at
    [SerializeField] private float sunriseHour; // time the sun should rise (i.e. 7)
    [SerializeField] private float sunsetHour; // time sun should set (i.e. 20.5)

    // the "sun" (directional light)
    private GameObject sun;

    // convert to TimeSpan values to aid in calculations
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime; 

    private DateTime currentTime; // keep track of current time

    void Awake()
    {
        // make it so only one instance of the ScoreController can exist and that it persists between scenes
        if (Instance == null)
        {
            Instance = this; // set the instance to this object
            DontDestroyOnLoad(gameObject); // mark this GameObject to not be destroyed
        }
        else
        {
            Destroy(gameObject); // if an instance already exists, destroy this new one
        }
    }

    // finds the correct sun object at the start of each new scene
    private void OnEnable()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours((int)sunriseHour);
        sunsetTime = TimeSpan.FromHours((int)sunsetHour);

        sun = GameObject.FindWithTag("Sun");
        if (sun == null)
        {
            Debug.LogError("Could not find the sun.");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier); // update the current time each frame
    }

    /// <summary>
    /// This method calculates the proper rotation of the sun depending on if it is between sunset and sunrise such that it is day time or night time 
    /// and updates the rotation of the sun (directional light) accordingly.
    /// </summary>
    private void RotateSun()
    {
        float sunRotation;

        // check if current time of day is between sunrise and sunset (daylight) and update the sun's rotation accordingly
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime); // calculate time between sunrise ans sunset
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay); // calculate how much time has passed since sunrise

            // calculate what percentage of the day time has passed
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;


            // this percentage then determines the rotation of the sun, 0 at sunrise and slowly increasing to 180 at sunset
            // we lerp between 0 and 180, because this is where the directional light appears as "daytime" 
            // we use the percentage as the interpolation value to determine where the sun should be between the two values 0 and 180
            sunRotation = Mathf.Lerp(0, 180, (float) percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            // this percentage then determines the rotation of the sun, 180 at sunset and 360 at sunrise
            // we lerp between 180 and 360 because this is where the directional light appears as "night time" 
            // we use the percentage as the interpolation value to determine where the sun should be between the two values 180 and 360
            sunRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        // set the sunlight rotation be applied such that it rotates around the x-axis
        sun.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
        Debug.Log("sun rotation: " + sunRotation);
    }

    /// <summary>
    /// Takes a parameter fromTime and toTime and calculates the difference between the two times.
    /// </summary>
    /// <returns></returns>
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = toTime - fromTime;

        // if the difference is negative, the two times span the transition from one day to another
        // add 24 hours to the difference to get the correct time
        if (diff.TotalSeconds < 0)
            diff += TimeSpan.FromHours(24);

        return diff;
        
    }
}
