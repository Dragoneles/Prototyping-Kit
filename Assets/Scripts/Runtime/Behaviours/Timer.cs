// Author:  Joseph Crump
// Date:    05/22/22

using UnityEngine;
using UnityEngine.Events;

namespace JC.Prototyping
{
    /// <summary>
    /// A Timer object that can be used to coordinate game events.
    /// </summary>
    public class Timer : MonoBehaviour
    {
        [Header("Properties")]
        [Min(0f)]
        [SerializeField]
        [Tooltip("How frequently the timer will raise its OnTick event. Will never tick more than once per frame.")]
        private float tickRate = 0.1f;

        [Min(0f)]
        [SerializeField]
        [Tooltip("How long the timer will run before stopping.")]
        private float duration = 5f;

        [SerializeField]
        [Tooltip("If false, the timer won't begin until explicitly started using Run().")]
        private bool runOnStart = true;

        [SerializeField]
        [Tooltip("If true, the timer will run again when it finishes.")]
        private bool loop = false;

        [Header("Events")]
        [SerializeField] private UnityEvent onTimerStarted = new();
        [SerializeField] private UnityEvent onTimerResumed = new();
        [SerializeField] private UnityEvent onTimerPaused = new();
        [SerializeField] private UnityEvent onTimerStopped = new();
        [SerializeField] private UnityEvent onTimerFinished = new();
        [SerializeField] private UnityEvent onTick = new();

        public float TickRate { get => tickRate; set => tickRate = value; }
        public float Duration { get => duration; set => duration = value; }
        public bool IsRunning { get; private set; } = false;
        public bool IsPaused { get; private set; } = false;
        public float TimeElapsed { get; private set; } = 0f;
        public float TimeElapsedRatio { get => TimeElapsed / Duration; }
        public float TimeRemaining { get => duration - TimeElapsed; }
        public float TimeRemainingRatio { get => 1f - TimeElapsedRatio; }

        private float tickTimeElapsed = 0f;

        private void Start()
        {
            if (runOnStart)
                Run();
        }

        private void Update()
        {
            if (IsPaused && !IsRunning)
                return;

            TimeElapsed += Time.deltaTime;
            tickTimeElapsed += Time.deltaTime;

            if (tickTimeElapsed >= TickRate)
            {
                tickTimeElapsed = 0f;
                Tick();
            }

            if (TimeRemaining <= 0f)
            {
                Finish();
            }
        }

        /// <summary>
        /// Start the timer from 0. Does nothing if the timer is already
        /// running. If it's paused, this will cause the timer to resume.
        /// If you want to restart the timer instead, use the Restart() 
        /// method.
        /// </summary>
        public void Run()
        {
            if (IsPaused)
            {
                Resume();
                return;
            }

            if (IsRunning)
                return;

            IsRunning = true;
            IsPaused = false;

            onTimerStarted?.Invoke();
        }

        /// <summary>
        /// Halt the timer at its current time.
        /// </summary>
        public void Pause()
        {
            if (IsPaused)
                return;

            IsPaused = true;

            onTimerPaused?.Invoke();
        }

        /// <summary>
        /// Stop the timer, resetting its elapsed time to 0.
        /// </summary>
        public void Stop()
        {
            if (!IsRunning)
                return;

            TimeElapsed = 0f;
            tickTimeElapsed = 0f;
            IsRunning = false;
            IsPaused = false;

            onTimerStopped?.Invoke();
        }

        /// <summary>
        /// Resume the timer from its paused state. If the timer is not paused,
        /// this method does nothing.
        /// </summary>
        public void Resume()
        {
            if (!IsPaused)
                return;

            IsPaused = false;

            onTimerResumed?.Invoke();
        }

        /// <summary>
        /// Stop the timer and then start it over again.
        /// </summary>
        public void Restart()
        {
            Stop();
            Run();
        }

        private void Tick()
        {
            onTick?.Invoke();
        }

        private void Finish()
        {
            onTimerFinished?.Invoke();

            Stop();

            if (loop)
                Run();
        }
    }
}
