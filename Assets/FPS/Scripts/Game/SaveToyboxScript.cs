using UnityEngine;
using System.IO;
namespace Unity.FPS.Game
{
    public static class SaveToyboxScript
    {
        public static string file_path() { return Application.dataPath + "/Toyboxdata.mine"; }

        public static SaveData LoadData()
        {
            if (File.Exists(file_path()))
            {
                string loaded_data = File.ReadAllText(file_path());
                SaveData lod = JsonUtility.FromJson<SaveData>(loaded_data);
                return lod;
            }
            else
            {
                Debug.Log("File doesn't exist");
                return null;
            }
        }

        public static void save_game()
        {
            SaveData data = new SaveData();
            data.FallDamage = GameManager.Instance.FallDamage;
            data.MovementSpeedOnGround = GameManager.Instance.MaxSpeedOnGround;
            data.MovementSpeedInAir = GameManager.Instance.MaxSpeedInAir;
            data.JumpForce = GameManager.Instance.JumpForce;
            data.GravityForce = GameManager.Instance.GravityForce;
            data.MaxChargeDuration = GameManager.Instance.MaxChargeDuration;
            data.MaxAmmo = GameManager.Instance.MaxAmmo;
            data.BulletSpreadAngle = GameManager.Instance.BulletSpreadAngle;
            data.TrampoplanteForce = GameManager.Instance.TrampoplanteForce;
            data.MinSpeedFallDamage = GameManager.Instance.MinSpeedFallDamage;
            data.FallDamageValeurAtMinSpeed = GameManager.Instance.FallDamageValeurAtMinSpeed;
            data.MaxSpeedFallDamage = GameManager.Instance.MaxSpeedFallDamage;
            data.FallDamageValeurAtMaxSpeed = GameManager.Instance.FallDamageValeurAtMaxSpeed;

            data.Vie = GameManager.Instance.Vie;
            data.MaxVie = GameManager.Instance.MaxVie;
            data.maxGourde = GameManager.Instance.MaxGourde;
            data.maxNourriture = GameManager.Instance.MaxNourriture;
            data.maxThirst = GameManager.Instance.MaxSoif;


            data.NbIngredientA = GameManager.Instance.NbIngredientA;
            data.NbIngredientB = GameManager.Instance.NbIngredientB;
            data.NbIngredientC = GameManager.Instance.NbIngredientC;
            data.NbIngredientD = GameManager.Instance.NbIngredientD;
            data.NbIngredientE = GameManager.Instance.NbIngredientE;
            data.NbIngredientF = GameManager.Instance.NbIngredientF;
            data.NbIngredientG = GameManager.Instance.NbIngredientG;

            data.NbRecette1 = GameManager.Instance.NbRecette1;
            data.NbRecette2 = GameManager.Instance.NbRecette2;
            data.NbRecette3 = GameManager.Instance.NbRecette3;
            data.NbRecette4 = GameManager.Instance.NbRecette4;
            data.NbRecette5 = GameManager.Instance.NbRecette5;

            data.BulletGravity = GameManager.Instance.BulletGravity;
            data.BulletSpeed = GameManager.Instance.BulletSpeed;


            data.BruteWalkSpeed = GameManager.Instance.BruteWalkSpeed;
            data.BruteRunSpeed = GameManager.Instance.BruteRunSpeed;
            data.BruteAngleSpeed = GameManager.Instance.BruteAngleSpeed;
            data.BruteAttackDistance = GameManager.Instance.BruteAttackDistance;
            data.BruteAttackStopDistance = GameManager.Instance.BruteAttackStopDistance;
            data.BruteDetectDistance = GameManager.Instance.BruteDetectDistance;

            data.TourelleWalkSpeed = GameManager.Instance.TourelleWalkSpeed;
            data.TourelleRunSpeed = GameManager.Instance.TourelleRunSpeed;
            data.TourelleAngleSpeed = GameManager.Instance.TourelleAngleSpeed;
            data.TourelleAttackDistance = GameManager.Instance.TourelleAttackDistance;
            data.TourelleAttackStopDistance = GameManager.Instance.TourelleAttackStopDistance;
            data.TourelleDetectDistance = GameManager.Instance.TourelleDetectDistance;

            data.FrondeWalkSpeed = GameManager.Instance.FrondeWalkSpeed;
            data.FrondeRunSpeed = GameManager.Instance.FrondeRunSpeed;
            data.FrondeAngleSpeed = GameManager.Instance.FrondeAngleSpeed;
            data.FrondeAttackDistance = GameManager.Instance.FrondeAttackDistance;
            data.FrondeAttackStopDistance = GameManager.Instance.FrondeAttackStopDistance;
            data.FrondeDetectDistance = GameManager.Instance.FrondeDetectDistance;


            string json_data = JsonUtility.ToJson(data);
            File.WriteAllText(file_path(), json_data);
        }
    }
}
