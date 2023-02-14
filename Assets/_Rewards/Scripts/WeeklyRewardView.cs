using System;
using UnityEngine;
namespace Rewards
{
    internal class WeeklyRewardView : DailyRewardView
    {
        private const string WeeklyCurrentSlotInActiveKey = nameof(WeeklyCurrentSlotInActiveKey);
        private const string WeeklyTimeGetRewardKey = nameof(WeeklyTimeGetRewardKey);

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(WeeklyCurrentSlotInActiveKey);
            set => PlayerPrefs.SetInt(WeeklyCurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(WeeklyTimeGetRewardKey);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(WeeklyTimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(WeeklyTimeGetRewardKey);
            }
        }
    }
}
