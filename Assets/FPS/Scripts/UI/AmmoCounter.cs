using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class AmmoCounter : MonoBehaviour
    {
        public TextMeshProUGUI m_textdroit;
        public TextMeshProUGUI m_textoblique;
        public GameObject _weapon;
        WeaponController _weaponController;

        private void Start()
        {
            m_textdroit.text = "0";
            m_textoblique.text = "0";
            _weaponController = FindObjectOfType<PlayerWeaponsManager>().Weapon;
        }

        void Update()
        {
           // m_textdroit.text = _weaponController.GetCurrentAmmoDirect().ToString();
           // m_textoblique.text = _weaponController.GetCurrentAmmoOblique().ToString();
        }

    }
}