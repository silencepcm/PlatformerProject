using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{



    public Slider ThirstSlider;
    public Slider GourdSlider;
    public Slider NourritureSlide;
    public float vie = 0f;

    public bool verif = true;
    public bool actif = false;

    public float DelaiGourde = 1.0f;
    public float timer = 0f;

    PlayerStatsScript player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsScript>();
        ThirstSlider.value = player.Thirst;
        GourdSlider.value = player.Gourde;
        NourritureSlide.value = player.Nourriture;
    }

    void Update()
    {
        ThirstSlider.value = player.Thirst;
        GourdSlider.value = player.Gourde;
        NourritureSlide.value = player.Nourriture;
        vie = player.Vie;
    }
}



