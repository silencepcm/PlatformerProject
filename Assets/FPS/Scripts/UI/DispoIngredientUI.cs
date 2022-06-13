using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DispoIngredientUI : MonoBehaviour
{
    public GameObject player;
    public Image image;
    public TextMeshProUGUI _text;
    public enum Ingredient
    {
        Munitite,
        Directite,
        Clochite,
        Baie,
        Fruit,
        Poussite,
        Plontite,
    }
    public Ingredient ingredient;

    void Update()
    {
        switch (ingredient)
        {
            case Ingredient.Munitite :
                if (player.GetComponent<InventaireScript>().Munitite < int.Parse(_text.text))
                {
                    image.color = Color.grey;
                }
                else
                {
                    image.color = Color.white;
                }
                break;

            case Ingredient.Directite:
                if (player.GetComponent<InventaireScript>().Directite < int.Parse(_text.text))
                {
                    image.color = Color.grey;
                }
                else
                {
                    image.color = Color.white;
                }
                break;

            case Ingredient.Clochite:
                if (player.GetComponent<InventaireScript>().Clochite < int.Parse(_text.text))
                {
                    image.color = Color.grey;
                }
                else
                {
                    image.color = Color.white;
                }
                break;

            case Ingredient.Baie:
                if (player.GetComponent<InventaireScript>().Baie < int.Parse(_text.text))
                {
                    image.color = Color.grey;
                }
                else
                {
                    image.color = Color.white;
                }
                break;

            case Ingredient.Fruit:
                if (player.GetComponent<InventaireScript>().Fruit < int.Parse(_text.text))
                {
                    image.color = Color.grey;
                }
                else
                {
                    image.color = Color.white;
                }
                break;

            case Ingredient.Poussite:
                if (player.GetComponent<InventaireScript>().Poussite < int.Parse(_text.text))
                {
                    image.color = Color.grey;
                }
                else
                {
                    image.color = Color.white;
                }
                break;

            case Ingredient.Plontite:
                if (player.GetComponent<InventaireScript>().Plontite < int.Parse(_text.text))
                {
                    image.color = Color.grey;
                }
                else
                {
                    image.color = Color.white;
                }
                break;

            default:
                Debug.Log("No type");
                break;
        }

        
    }
}
