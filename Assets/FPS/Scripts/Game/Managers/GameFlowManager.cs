﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
namespace Unity.FPS.Game
{
    public class GameFlowManager : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("Duration of the fade-to-black at the end of the game")]
        public float EndSceneLoadDelay = 3f;
        private GameObject Player;

        public Vector3 PtSauvegarde;
        public Quaternion Rotation;

       

        [Header("Win")]
        [Tooltip("This string has to be the name of the scene you want to load when winning")]
        public string WinSceneName = "WinScene";

        [Tooltip("Duration of delay before the fade-to-black, if winning")]
        public float DelayBeforeFadeToBlack = 4f;

        [Tooltip("Win game message")]
        public string WinGameMessage;
        [Tooltip("Duration of delay before the win message")]
        public float DelayBeforeWinMessage = 2f;

        [Tooltip("Sound played on win")] public AudioClip VictorySound;

        [Header("Lose")]
        [Tooltip("This string has to be the name of the scene you want to load when losing")]
        public string LoseSceneName = "LoseScene";

        public bool GameIsEnding { get; private set; }

        float m_TimeLoadEndGameScene;
        string m_SceneToLoad;

        void Awake()
        {
            EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        }
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PtSauvegarde = Player.transform.position;
            AudioUtility.SetMasterVolume(1);
            StartGame();
            
        }
        void Update()
        {
            if (GameIsEnding)
            {
                Camera.main.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / EndSceneLoadDelay;
                GetComponent<HUDManager>().canvasGroup.alpha = timeRatio;

                AudioUtility.SetMasterVolume(1 - timeRatio);

                // See if it's time to load the end scene (after the delay)
                if (timeRatio >= 1)
                {
                    //SceneManager.LoadScene("ToyBox");
                    // respawn du personnage au  dernier point de sauvegarde
                    GetComponent<HUDManager>().canvasGroup.alpha = 0;
                    GetComponent<HUDManager>().canvasGroup.gameObject.SetActive(false);
                    GameIsEnding = false;
                    Player.GetComponent<Gameplay.PlayerCharacterController>().OnRespawn();
                    Player.GetComponent<PlayerStatsScript>().Respawn();
                    GameManager.Instance.GetComponent<SurvieScript>().Vie.value = 100;
                    Player.GetComponent<CharacterController>().enabled = false;
                    Player.transform.SetPositionAndRotation(PtSauvegarde, Rotation);
                    Player.GetComponent<CharacterController>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                }
            }
        }

        void OnPlayerDeath(PlayerDeathEvent evt) => EndGame(false);

        void EndGame(bool win)
        {
            // unlocks the cursor before leaving the scene, to be able to click buttons
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Remember that we need to load the appropriate end scene after a delay
            GameIsEnding = true;
            FindObjectOfType<HUDManager>().canvasGroup.gameObject.SetActive(true);
            if (win)
            {
                m_SceneToLoad = WinSceneName;
                m_TimeLoadEndGameScene = Time.time + EndSceneLoadDelay + DelayBeforeFadeToBlack;

                // play a sound on win
                var audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = VictorySound;
                audioSource.playOnAwake = false;
                audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
                audioSource.PlayScheduled(AudioSettings.dspTime + DelayBeforeWinMessage);

                // create a game message
                //var message = Instantiate(WinGameMessagePrefab).GetComponent<DisplayMessage>();
                //if (message)
                //{
                //    message.delayBeforeShowing = delayBeforeWinMessage;
                //    message.GetComponent<Transform>().SetAsLastSibling();
                //}

            }
            else
            {
                m_SceneToLoad = LoseSceneName;
                m_TimeLoadEndGameScene = Time.time + EndSceneLoadDelay;
            }
        }

        void StartGame()
        {
            PtSauvegarde = Player.GetComponent<Transform>().position;
            Rotation = Player.GetComponent<Transform>().rotation;
        }


    }
}