using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandlerScript : MonoBehaviour
{
    public TextMeshProUGUI TextBox;
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("Item 1");
        items.Add("Item 2");

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        DropdowmItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdowmItemSelected(dropdown); });
    }
    void DropdowmItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBox.text = dropdown.options[index].text;
    }
}
