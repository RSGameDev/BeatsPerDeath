using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class BeatManager : Singleton<BeatManager>
{
    #region Private & Const Variables

    /// <summary>
    /// Time between beats in second
    /// </summary>
    [SerializeField]
    private const int s_BeatInterval = 2;

    /// <summary>
    /// Period of time that IsOnBeat value stays true
    /// </summary>
    [SerializeField]
    private float BeatLenght = 0.1f;

    /// <summary>
    /// Bool to activate auto beats for debug
    /// </summary>
    [SerializeField]
    private bool IsAutoBeatOn = false;

    /// <summary>
    /// Total beats
    /// </summary>
    private const int s_BeatLimit = 8;

    /// <summary>
    /// scond to milisecond multiplier
    /// </summary>
    private const int s_MiliSecondMultiplier = 1000;

    /// <summary>
    /// Variable to increment in auto beat functionality
    /// </summary>
    private float _time;

    /// <summary>
    /// Active beat index
    /// </summary>
    private int _beatIndex;

    /// <summary>
    /// Stays true for BeatLenght second
    /// </summary>
    private bool _isOnBeat;

    private bool _isBeatsStarted = false;

    /// <summary>
    /// Actions that will be performed on specific beats
    /// </summary>
    private Dictionary<int, List<Action>> _beatListeners;

    #endregion

    #region Public & Protected Variables  

    public bool IsOnBeat => _isOnBeat;

    public int BeatIndex => _beatIndex;

    public bool AreBeatsStarted => _isBeatsStarted;
    #endregion

    #region Constructors

    private BeatManager()
    {
        _beatListeners = new Dictionary<int, List<Action>>();
    }

    #endregion

    #region Private Methods

    private void Update()
    {
        if (IsAutoBeatOn)
        {
            UpdateAutoBeat();
        }
    }

    /// <summary>
    /// Used for test purposes
    /// </summary>
    private void UpdateAutoBeat()
    {
        _time += Time.deltaTime;

        if (_time < s_BeatInterval)
        {
            return;
        }

        _time = 0;

        UpdateBeat();
    }

    /// <summary>
    /// Small time gap after the beat for not exact player moves 
    /// (Should be discussed with desing team)
    /// </summary>
    private void StartOnBeatInterval()
    {
        Task.Run(() =>
        {
            _isOnBeat = true;
            Thread.Sleep((int)(BeatLenght * s_MiliSecondMultiplier));
            _isOnBeat = false;
        });
    }

    #endregion

    #region Public & Protected Methods		

    /// <summary>
    /// Add listener to a specific beat
    /// </summary>
    /// <param name="beatIndex">Index starts from 0. So Beat for is index 3</param>
    /// <param name="beatAction"></param>
    public void AddListener(int beatIndex, Action beatAction)
    {
        var indexMod = beatIndex & s_BeatLimit;

        if (!_beatListeners.ContainsKey(indexMod))
        {
            _beatListeners.Add(indexMod, new List<Action>());
        }

        _beatListeners[indexMod].Add(beatAction);
    }

    /// <summary>
    /// Add listener to every beat
    /// </summary>
    /// <param name="beatAction"></param>
    public void AddListenerToAll(Action beatAction)
    {
        for (var index = 0; index < s_BeatLimit; index++)
        {
            AddListener(index, beatAction);
        }
    }

    /// <summary>
    /// Call this for every beat
    /// </summary>
    public void UpdateBeat()
    {
        _isBeatsStarted = true;

        StartOnBeatInterval();

        _beatIndex++;
        _beatIndex %= s_BeatLimit;

        if (!_beatListeners.ContainsKey(_beatIndex))
        {
            return;
        }

        _beatListeners[_beatIndex].ForEach(x => x?.Invoke()); ;
    }

    #endregion
}
