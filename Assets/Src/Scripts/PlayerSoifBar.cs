using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine.UI;

public class PlayerSoifBar : MonoBehaviour
{
        [Tooltip("Image component dispplaying current health")]
        public Image HealthFillImage;

    PlayerStatsScript player;
        void Start()
        {
        player = FindObjectOfType<PlayerStatsScript>();
        }

        void Update()
        {
            // update health bar value
            HealthFillImage.fillAmount = player.Vie / player.MaxVie;
        }
}
