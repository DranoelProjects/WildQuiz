using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DailyRewardSystem
{
    public enum RewardType
    {
        Hearts,
        Coins,
    }

    [Serializable] public struct Reward
    {
        public RewardType Type;
        public int Amount;
    }

    public class DailyRewards : MonoBehaviour
    {
        [Header("Reward UI")]
        [SerializeField] GameObject rewardsCanvas;
        [SerializeField] Button openButton;
        [SerializeField] Button closeButton;
        [SerializeField] Image rewardImage;
        [SerializeField] Text rewardAmountText;
        [SerializeField] Button claimButton;
        [SerializeField] GameObject rewardsNotification;
        [SerializeField] GameObject noMoreRewardsPanel, rewardsInfo;

        [Space]
        [Header("Rewards Images")]
        [SerializeField] Sprite iconHearts;
        [SerializeField] Sprite iconCoins;

        [Space]
        [Header("Rewards Database")]
        [SerializeField] RewardsDatabase rewardsDB;

        [Space]
        [Header("Timing")]
        [SerializeField] ParticleSystem fx;

        [Space]
        [Header("Timing")]
        //next reward wait delay
        [SerializeField] double nextRewardDelay = 24f;
        [SerializeField] float checkForRewardDelay = 60f;

        //Used in order to update user hearts and coins 
        PanelUserInfo panelUserInfo;

        private int nextRewardIndex;
        private bool isRewardReady = false;

        private void Awake()
        {
            panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>();
        }
        void Start()
        {
            //PlayerPrefs.DeleteAll();
            initialize();
            //Check for rewards
            StopAllCoroutines();
            StartCoroutine(CheckForRewards());
        }

        void initialize()
        {
            nextRewardIndex = PlayerPrefs.GetInt("NextRewardIndex", 0);

            //Clicks events
            openButton.onClick.RemoveAllListeners();
            openButton.onClick.AddListener(onOpenButtonClick);

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(onCloseButtonClick);

            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(onClaimButtonClick);

            //game is opened for the first time
            if (string.IsNullOrEmpty(PlayerPrefs.GetString("RewardClaimDatetime")))
            {
                PlayerPrefs.SetString("RewardClaimDatetime", DateTime.Now.ToString());
                PlayerPrefs.SetInt("NextLevel", 85);
                PlayerPrefs.SetInt("HeartsNumber", 20);
                PlayerPrefs.SetInt("CoinsNumber", 1000);
                PlayerPrefs.SetInt("NumberLostLevels", 0);
                PlayerPrefs.SetInt("NumberWonLevels", 0);
                PlayerPrefs.SetInt("mute", 0);
            }
        }

        IEnumerator CheckForRewards()
        {
            while (true)
            {
                if (!isRewardReady)
                {
                    DateTime currentDatetime = DateTime.Now;
                    DateTime rewardClaimDatetime = DateTime.Parse(PlayerPrefs.GetString("RewardClaimDatetime", currentDatetime.ToString()));

                    double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalHours;

                    if (elapsedHours >= nextRewardDelay)
                        activateReward();
                    else
                        desactivateReward();
                }
                yield return new WaitForSeconds(checkForRewardDelay);
            }

        }

        void activateReward()
        {
            isRewardReady = true;
            noMoreRewardsPanel.SetActive(false);
            rewardsNotification.SetActive(true);
            rewardsInfo.SetActive(true);

            //Update reward UI
            Reward reward = rewardsDB.GetReward(nextRewardIndex);
            if (reward.Type == RewardType.Hearts)
                rewardImage.sprite = iconHearts;
            else if (reward.Type == RewardType.Coins)
                rewardImage.sprite = iconCoins;
            rewardAmountText.text = string.Format("+{0}", reward.Amount);
        }

        void desactivateReward()
        {
            isRewardReady = false;
            noMoreRewardsPanel.SetActive(true);
            rewardsNotification.SetActive(false);
            rewardsInfo.SetActive(false);
        }

        void onClaimButtonClick()
        {
            Reward reward = rewardsDB.GetReward(nextRewardIndex);

            //check reward type
            if(reward.Type == RewardType.Hearts)
            {
                GameData.Hearts += reward.Amount;
                panelUserInfo.UpdateHearts();
            }
            else if (reward.Type == RewardType.Coins)
            {
                GameData.Coins += reward.Amount;
                panelUserInfo.UpdateCoins();
            }
            fx.Play();
            isRewardReady = false;

            //increment reward index
            nextRewardIndex++;
            if (nextRewardIndex >= rewardsDB.rewardsCount)
                nextRewardIndex = 0;
            PlayerPrefs.SetInt("NextRewardIndex", nextRewardIndex);

            PlayerPrefs.SetString("RewardClaimDatetime", DateTime.Now.ToString());
            desactivateReward();
        }

        //Open / Close rewards panel
        void onOpenButtonClick()
        {
            rewardsCanvas.SetActive(true);
        }
        void onCloseButtonClick()
        {
            rewardsCanvas.SetActive(false);
        }
    }
}

