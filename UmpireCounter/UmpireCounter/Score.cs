 using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Timers;

namespace UmpireCounter
{
    public static class Score
    {
        public static int Total { get; set; }
        public static int Wickets { get; set; }
        public static int ValidDeliveriesInOver { get; set; }
        public static int OversCompleted { get; set; }
        public static int BallsInOver { get; set; }
        public static bool InningsInPlay { get; set; }
        public static DateTime InningsStartTime { get; set; }
        public static string InningsTime { get; set; }
        public static TimeSpan TimeSinceTimerUpdate { get; set; }
        public static DateTime TimeLastUpdated { get; set; }
        public static bool AnyButtonPressed { get; set; }
        public static bool TimeChanged { get; set; }
        public static Timer inningsTimerCheckerTimer;

        /*public static void CheckingTimer()
        {
            inningsTimerCheckerTimer = new System.Timers.Timer();
            inningsTimerCheckerTimer.Interval = 30000;
            inningsTimerCheckerTimer.Elapsed += Score.CheckUpdateTimer;
            inningsTimerCheckerTimer.AutoReset = true;
            inningsTimerCheckerTimer.Enabled = true;
        }
        */
        public static void IncreaseBalls()
        {
            Score.ValidDeliveriesInOver++;

            if (Score.ValidDeliveriesInOver == Score.BallsInOver)
            {
                Score.OversCompleted++;
                Score.ValidDeliveriesInOver = 0;
            }

            if (!Score.InningsInPlay)
            {
                Score.InningsStartTime = DateTime.Now;
                Score.InningsInPlay = true;
            }

            TextFileStorage.WriteScore();
        }

        public static void DecreaseBalls()
        {
            if (Score.ValidDeliveriesInOver == 0)
            {
                Score.ValidDeliveriesInOver = (Score.BallsInOver - 1);
                Score.OversCompleted--;
            }
            else
            {
                Score.ValidDeliveriesInOver--;
            }

            if (Score.OversCompleted < 0)
            {
                Score.OversCompleted = 0;
                Score.ValidDeliveriesInOver = 0;
            }

            TextFileStorage.WriteScore();
        }

        public static void IncreaseTotal()
        {
            Score.Total++;
            TextFileStorage.WriteScore();

            if (!Score.InningsInPlay)
            {
                Score.InningsStartTime = DateTime.Now;
                Score.InningsInPlay = true;
            }
        }

        public static void DecreaseTotal()
        {
            Score.Total--;
            if (Score.Total < 0)
            {
                Score.Total = 0;
            }

            TextFileStorage.WriteScore();
        }

        public static void IncreaseWickets()
        {
            Score.Wickets++;
            TextFileStorage.WriteScore();

            if (!Score.InningsInPlay)
            {
                Score.InningsStartTime = DateTime.Now;
                Score.InningsInPlay = true;
            }
        }

        public static void DecreaseWickets()
        {
            Score.Wickets--;
            if (Score.Wickets < 0)
            {
                Score.Wickets = 0;
            }

            TextFileStorage.WriteScore();
        }

        public static void ResetScore()
        {
            Score.Total = 0;
            Score.Wickets = 0;
            Score.OversCompleted = 0;
            Score.ValidDeliveriesInOver = 0;
            Score.InningsTime = "Innings hasn't started";
            Score.InningsInPlay = false;
        }

        public static void UpdateInningsTimer()
        {
            Score.InningsTime = DateTime.Now.Subtract(Score.InningsStartTime).Minutes.ToString() + " minute(s)";
            Score.TimeLastUpdated = DateTime.Now;
        }

        public static void CheckUpdateTimer()
        {
            if (Score.InningsTime != "Innings hasn't started")
            {
                int minutesSinceUpdate = DateTime.Now.Subtract(Score.TimeLastUpdated).Minutes;
                if (minutesSinceUpdate >= 1)
                {
                    Score.TimeChanged = true;
                    Score.UpdateInningsTimer();
                }
            }
        }
    }
}
