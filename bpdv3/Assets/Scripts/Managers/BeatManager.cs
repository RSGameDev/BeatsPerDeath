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

    /// <summary>
    /// Actions that will be performed on specific beats
    /// </summary>
    private Dictionary<int, List<Action>> _beatListeners;

    #endregion

    #region Public & Protected Variables  

    public bool IsOnBeat => _isOnBeat;

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

    public void AddListener(int beatIndex, Action beatAction)
    {
        if (beatIndex >= s_BeatLimit)
        {
            throw new Exception($"Given beat index can not be bigger than the beat limit. BeatLimit: {s_BeatLimit}");
        }

        if (!_beatListeners.ContainsKey(beatIndex))
        {
            _beatListeners.Add(beatIndex, new List<Action>());
        }

        _beatListeners[beatIndex].Add(beatAction);
    }

    public void AddListenerToAll(Action beatAction)
    {
        for (var index = 0; index < s_BeatLimit; index++)
        {
            AddListener(index, beatAction);
        }
    }

    public void UpdateBeat()
    {
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
