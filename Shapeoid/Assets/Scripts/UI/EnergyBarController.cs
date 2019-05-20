using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    public ScoreHandler scoreHandler;
    public Sprite energyBar1;
    public Sprite energyBar2;
    public Sprite energyBar3;
    public Sprite energyBar4;

    private Image image;

    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponent<Image>();

        image.sprite = energyBar2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (scoreHandler.energy > 37)
        {
            image.sprite = energyBar1;
        }
        else if (scoreHandler.energy > 24)
        {
            image.sprite = energyBar2;
        }
        else if (scoreHandler.energy > 12)
        {
            image.sprite = energyBar3;
        }
        else
        {
            image.sprite = energyBar4;
        }
    }
}
