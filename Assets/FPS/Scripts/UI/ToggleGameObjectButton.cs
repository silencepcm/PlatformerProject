using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

namespace Unity.FPS.UI
{
    public class ToggleGameObjectButton : MonoBehaviour
    {
        public GameObject ObjectToToggle;
        public VideoPlayer video;
        public bool ResetSelectionAfterClick;

        void Update()
        {
            if (ObjectToToggle.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel))
            {
                SetGameObjectActive(false);
            }
        }

        public void SetGameObjectActive(bool active)
        {
            ObjectToToggle.SetActive(active);

            if (ResetSelectionAfterClick)
                EventSystem.current.SetSelectedGameObject(null);
        }

        public void SetVideoActive()
        {
            video.Play();
            
        }
    }
}