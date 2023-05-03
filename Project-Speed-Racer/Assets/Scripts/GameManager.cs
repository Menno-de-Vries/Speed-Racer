using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{

    #region Static Manager

    private static GameManager m_instance;

    #endregion

    #region Settings

    [Tooltip("The waypoints will be saved in here so the finish knows you really have raced the whole track")]
    public GameObject[] Waypoints;

    [Tooltip("The amount of Waypoint there are")]
    public int WaypointAmount;

    [Tooltip("The maximum of waypoint in the map")]
    public int MaximumWaypointAmount;

    [Tooltip("The amount of laps you need to race")]
    public int TheAmountOfLaps;
    public int maxAmountOflaps = 3;
    public int lapsToWin = 4;

    [Header("Text Objects")]
    public TextMeshProUGUI laps;
    public TextMeshProUGUI Timer;

    [Header("Timer")]
    public float currentTime;
    bool stopwatchActive = true;
    #endregion

    #region Standard Methods

    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    public void Start()
    {
        maxAmountOflaps = 3;

        currentTime = 0;
    }

    public void Update()
    {
        laps.text = ("Laps " + TheAmountOfLaps + ":" + maxAmountOflaps);

        if (TheAmountOfLaps >= lapsToWin)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if(stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            Timer.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopWatch()
    {
        stopwatchActive = true;
    }

    public void StopStopWatch()
    {
        stopwatchActive = false;
        currentTime = 0;
    }

    #endregion

    #region Safe point and finish system

    /// <summary>
    /// Checks if you are riding the route correctly so you can race all laps
    /// </summary>
    /// <param name="_WaypointOrFinish"></param>
    public void CheckAndSafeWaypointLap(GameObject _WaypointOrFinish)
    {
        if (_WaypointOrFinish.GetComponent<FinishTag>())
        {
            CheckForFinish();
            return;
        }
        
        else if (_WaypointOrFinish.GetComponent<WaypointTag>())
        {
            // Grabs the script of the waypoint and then checks if it is the right one
            // if not then your not following the race track in the good direction
            WaypointTag _WaypointScript = _WaypointOrFinish.GetComponent<WaypointTag>();

            if (_WaypointScript.WaypointIndentifiër == WaypointAmount)
            {
                // Fills the right index in the array with the right Waypoint
                Waypoints[WaypointAmount] = _WaypointOrFinish;

                // Sets the Waypoint amount to the next waypoint it needs
                WaypointAmount++;

            }
            else
                Debug.Log("Your going the wrong way");

        }
    }

    /// <summary>
    /// if you have raced the track correctly then if you finish it then it will lower the amount of laps remaining and start the waypoint saving system over again
    /// </summary>
    /// <param name="_WaypointOrFinish"></param>
    public void CheckForFinish()
    {
        if (Waypoints[Waypoints.Length - 1] != null)
        {
            // Clears the waypoint array to race the new lap
            for (int i = 0; i < Waypoints.Length; i++)
            {
                Waypoints[i] = null;
                StopStopWatch();
               
                StartStopWatch();
            }

            // Sets the amount of laps remaining
            TheAmountOfLaps++;

            // Sets the waypoint to the first one you need to collect
            WaypointAmount = 0;

        }
    }

    #endregion

}
