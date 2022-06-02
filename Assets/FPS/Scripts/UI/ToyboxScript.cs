using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.FPS.Game;

using UnityEngine.Events;


namespace Unity.FPS.Gameplay
{
    public class ToyboxScript : MonoBehaviour
    {
        private PlayerInputHandler playerInput;
        private PlayerCharacterController playerCharacterController;
        public List<GameObject> jumpUIObjects;
        public List<GameObject> tirUIObjects;
        public List<GameObject> trampolantes;
        public List<GameObject> trampolanteUIParams;
        public GameObject MinSpeedFallDamage;
        public GameObject MaxSpeedFallDamage;
        public GameObject FallDamageVALEURatMinSpeed;
        public GameObject FallDamageVALEURatMaxSpeed;
        public GameObject InventaireUIFeedBack;
        public GameObject UIPanel;
        public GameObject InnerPanel;
        WeaponController playerWeaponsController;
        public List<Transform> toggleUI;
        int activeSetting = 0;
        public GameObject TirSettingsUI;
        public GameObject PlayerSettingsUI;
        public GameObject BruteSettingsUI;
        public GameObject TourelleSettingsUI;
        public GameObject FrondeSettingsUI;
        public UnityAction setEnemyParamsUpdate;
        
        void Start()
        {
            playerInput = FindObjectOfType<PlayerInputHandler>();
            playerCharacterController = FindObjectOfType<PlayerCharacterController>();
            playerWeaponsController = playerCharacterController.GetComponent<PlayerWeaponsManager>().Weapon;
        }
        public void LoadValues()
        {
            if (PlayerSettingsUI.activeSelf)
            {
                foreach (Transform element in toggleUI)
                {
                    Toggle toggle = element.GetComponent<Toggle>();
                    if (element.name == "FallDamage")
                    {
                        toggle.isOn = GameManager.Instance.FallDamage;
                        playerCharacterController.RecievesFallDamage = GameManager.Instance.FallDamage;
                        MinSpeedFallDamage.SetActive(GameManager.Instance.FallDamage);
                        MaxSpeedFallDamage.SetActive(GameManager.Instance.FallDamage);
                        FallDamageVALEURatMinSpeed.SetActive(GameManager.Instance.FallDamage);
                        FallDamageVALEURatMaxSpeed.SetActive(GameManager.Instance.FallDamage);
                    }
                }
                foreach (Transform element in PlayerSettingsUI.transform)
                {
                    TMP_InputField text = element.GetComponent<TMP_InputField>();
                    switch (element.name)
                    {
                        case "MovementSpeedOnGround":
                            text.text = GameManager.Instance.MaxSpeedOnGround.ToString();
                            break;
                        case "MovementSpeedInAir":
                            text.text = GameManager.Instance.MaxSpeedInAir.ToString();
                            break;
                        case "JumpForce":
                            text.text = GameManager.Instance.JumpForce.ToString();
                            break;
                        case "GravityForce":
                            text.text = GameManager.Instance.GravityForce.ToString();
                            break;
                        case "TrampoplanteForce":
                            text.text = GameManager.Instance.TrampoplanteForce.ToString();
                            break;
                        case "MinSpeedFallDamage":
                            text.text = GameManager.Instance.MinSpeedFallDamage.ToString();
                            break;
                        case "FallDamageValeurAtMinSpeed":
                            text.text = GameManager.Instance.FallDamageValeurAtMinSpeed.ToString();
                            break;
                        case "MaxSpeedFallDamage":
                            text.text = GameManager.Instance.MaxSpeedFallDamage.ToString();
                            break;
                        case "FallDamageValeurAtMaxSpeed":
                            text.text = GameManager.Instance.FallDamageValeurAtMaxSpeed.ToString();
                            break;
                    }
                }

            }
            else if (TirSettingsUI.activeSelf)
            {
                foreach (Transform element in TirSettingsUI.transform)
                {
                    TMP_InputField text = element.GetComponent<TMP_InputField>();
                    switch (element.name)
                    {
                        case "MaxChargeDuration":
                            text.text = GameManager.Instance.MaxChargeDuration.ToString();
                            break;
                        case "MaxAmmo":
                            text.text = GameManager.Instance.MaxAmmo.ToString();
                            break;
                        case "BulletSpreadAngle":
                            text.text = GameManager.Instance.BulletSpreadAngle.ToString();
                            break;
                        case "BulletGravity":
                            text.text = GameManager.Instance.BulletGravity.ToString();
                            break;
                        case "BulletSpeed":
                            text.text = GameManager.Instance.BulletSpeed.ToString();
                            break;
                    }
                }
            }
            else if (BruteSettingsUI.activeSelf)
            {
                foreach (Transform element in BruteSettingsUI.transform)
                {
                    TMP_InputField text = element.GetComponent<TMP_InputField>();
                    switch (element.name)
                    {
                        case "WalkSpeed":
                            text.text = GameManager.Instance.BruteWalkSpeed.ToString();
                            break;
                        case "RunSpeed":
                            text.text = GameManager.Instance.BruteRunSpeed.ToString();
                            break;
                        case "AttackDistance":
                            text.text = GameManager.Instance.BruteAttackDistance.ToString();
                            break;
                        case "DetectDistance":
                            text.text = GameManager.Instance.BruteDetectDistance.ToString();
                            break;
                        case "AttackStopDistance":
                            text.text = GameManager.Instance.BruteAttackStopDistance.ToString();
                            break;
                        case "AngleSpeed":
                            text.text = GameManager.Instance.BruteAngleSpeed.ToString();
                            break;
                    }
                }
            }
            else if (BruteSettingsUI.activeSelf)
            {
                foreach (Transform element in BruteSettingsUI.transform)
                {
                    TMP_InputField text = element.GetComponent<TMP_InputField>();
                    switch (element.name)
                    {
                        case "WalkSpeed":
                            text.text = GameManager.Instance.TourelleWalkSpeed.ToString();
                            break;
                        case "RunSpeed":
                            text.text = GameManager.Instance.TourelleRunSpeed.ToString();
                            break;
                        case "AttackDistance":
                            text.text = GameManager.Instance.TourelleAttackDistance.ToString();
                            break;
                        case "DetectDistance":
                            text.text = GameManager.Instance.TourelleDetectDistance.ToString();
                            break;
                        case "AttackStopDistance":
                            text.text = GameManager.Instance.TourelleAttackStopDistance.ToString();
                            break;
                        case "AngleSpeed":
                            text.text = GameManager.Instance.TourelleAngleSpeed.ToString();
                            break;
                    }
                }
            }
            else if (FrondeSettingsUI.activeSelf)
            {
                foreach (Transform element in FrondeSettingsUI.transform)
                {
                    TMP_InputField text = element.GetComponent<TMP_InputField>();
                    switch (element.name)
                    {
                        case "WalkSpeed":
                            text.text = GameManager.Instance.FrondeWalkSpeed.ToString();
                            break;
                        case "RunSpeed":
                            text.text = GameManager.Instance.FrondeRunSpeed.ToString();
                            break;
                        case "AttackDistance":
                            text.text = GameManager.Instance.FrondeAttackDistance.ToString();
                            break;
                        case "DetectDistance":
                            text.text = GameManager.Instance.FrondeDetectDistance.ToString();
                            break;
                        case "AttackStopDistance":
                            text.text = GameManager.Instance.FrondeAttackStopDistance.ToString();
                            break;
                        case "AngleSpeed":
                            text.text = GameManager.Instance.FrondeAngleSpeed.ToString();
                            break;
                    }
                }
            }
        }
        public void RightSettingButton()
        {
            switch(activeSetting)
            {
                case 0:
                    PlayerSettingsUI.SetActive(false);
                    TirSettingsUI.SetActive(true);
                    break;
                case 1:
                    TirSettingsUI.SetActive(false);
                    BruteSettingsUI.SetActive(true);
                    break;
                case 2:
                    BruteSettingsUI.SetActive(false);
                    TourelleSettingsUI.SetActive(true);
                    break;
                case 3:
                    TourelleSettingsUI.SetActive(false);
                    FrondeSettingsUI.SetActive(true);
                    break;
                case 4:
                    FrondeSettingsUI.SetActive(false);
                    PlayerSettingsUI.SetActive(true);
                    break;
            }
            activeSetting = (activeSetting + 1) % 5;
            LoadValues();
        }
        public void LeftSettingButton()
        {
            activeSetting = (activeSetting - 1) % 5;

            switch (activeSetting)
            {
                case 0:
                    PlayerSettingsUI.SetActive(true);
                    TirSettingsUI.SetActive(false);
                    break;
                case 1:
                    TirSettingsUI.SetActive(true);
                    BruteSettingsUI.SetActive(false);
                    break;
                case 2:
                    BruteSettingsUI.SetActive(true);
                    TourelleSettingsUI.SetActive(false);
                    break;
                case 3:
                    TourelleSettingsUI.SetActive(true);
                    FrondeSettingsUI.SetActive(false);
                    break;
                case 4:
                    FrondeSettingsUI.SetActive(true);
                    PlayerSettingsUI.SetActive(false);
                    break;
            }
            LoadValues();
        }
        public void FallDamageActivate(Toggle toggle)
        {
            playerCharacterController.RecievesFallDamage = toggle.isOn;
            MinSpeedFallDamage.SetActive(toggle.isOn);
            MaxSpeedFallDamage.SetActive(toggle.isOn);
            FallDamageVALEURatMinSpeed.SetActive(toggle.isOn);
            FallDamageVALEURatMaxSpeed.SetActive(toggle.isOn);
            GameManager.Instance.FallDamage = toggle.isOn;
        }

        public void SetGravityForce(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.GravityDownForce = f;
                GameManager.Instance.GravityForce = f;
            }
        }
        public void SetBulletGravity(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BulletGravity = f;
            }
        }
        public void SetBulletSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BulletSpeed = f;
            }
        }
        public void SetMovementSpeedOnGround(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.MaxSpeedOnGround = f;
                GameManager.Instance.MaxSpeedOnGround = f;
            }
        }
        public void SetVie(TMP_InputField textObj)
        {
            if (int.TryParse(textObj.text, out int f))
            {
                GameManager.Instance.Vie = f;
            }
        }
        public void SetMaxVie(TMP_InputField textObj)
        {
            if (int.TryParse(textObj.text, out int f))
            {
                GameManager.Instance.MaxVie = f;
            }
        }
        public void SetMovementSpeedInAir(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.MaxSpeedInAir = f;
                GameManager.Instance.MaxSpeedInAir = f;
            }
        }

        public void SetJumpForce(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.JumpForce = f;
                GameManager.Instance.JumpForce = f;

            }
        }
        public void SetMaxAmmo(TMP_InputField textObj)
        {
            if (int.TryParse(textObj.text, out int f) && playerWeaponsController != null)
            {
                playerWeaponsController.MaxAmmo = f;
                GameManager.Instance.MaxAmmo = f;

            }
        }
        public void SetMaxChargeDuration(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f) && playerWeaponsController != null)
            {
                playerWeaponsController.MaxChargeDuration = f;
                GameManager.Instance.MaxChargeDuration = f;

            }
        }
        public void SetSpreadAngle(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f) && playerWeaponsController != null)
            {
                playerWeaponsController.BulletSpreadAngle = f;
                GameManager.Instance.BulletSpreadAngle = f;

            }
        }
        public void SetWeaponsManager()
        {
            playerWeaponsController = GameObject.Find("Player").GetComponent<WeaponController>();
        }
        public void SetTrampoplanteForce(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.TrampoplanteForce = f;
                playerCharacterController.TrampoplanteForce = f;
            }
        }


        public void SetMinSpeedFallDamage(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.MinSpeedForFallDamage = f;
                GameManager.Instance.MinSpeedFallDamage = f;
            }
        }
        public void SetMaxSpeedFallDamage(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.MaxSpeedForFallDamage = f;
                GameManager.Instance.MaxSpeedFallDamage = f;
            }
        }
        public void SetMinFallDamage(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.FallDamageAtMinSpeed = f;
                GameManager.Instance.FallDamageValeurAtMinSpeed = f;
            }
        }
        public void SetMaxFallDamage(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                playerCharacterController.FallDamageAtMaxSpeed = f;
                GameManager.Instance.FallDamageValeurAtMaxSpeed = f;
            }
        }






        public void SetBruteWalkSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BruteWalkSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetBruteRunSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BruteRunSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetBruteAngularSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BruteAngleSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetBruteDetectDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BruteDetectDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetBruteAttackDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BruteAttackDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetBruteAttackStopDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.BruteAttackStopDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }



        public void SetTourelleWalkSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.TourelleWalkSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetTourelleRunSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.TourelleRunSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetTourelleAngularSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.TourelleAngleSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetTourelleDetectDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.TourelleDetectDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetTourelleAttackDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.TourelleAttackDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetTourelleAttackStopDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.TourelleAttackStopDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }



        public void SetFrondeWalkSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.FrondeWalkSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetFrondeRunSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.FrondeRunSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetFrondeAngularSpeed(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.FrondeAngleSpeed = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetFrondeDetectDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.FrondeDetectDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetFrondeAttackDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.FrondeAttackDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SetFrondeAttackStopDistance(TMP_InputField textObj)
        {
            if (float.TryParse(textObj.text, out float f))
            {
                GameManager.Instance.FrondeAttackStopDistance = f;
                if (setEnemyParamsUpdate != null)
                {
                    setEnemyParamsUpdate.Invoke();
                }
            }
        }
        public void SaveChanges()
        {
            SaveToyboxScript.save_game();
        }
    }
}
