using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorController : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite fireSprite;
    public Sprite airSprite;
    public Sprite waterSprite;
    public Sprite earthSprite;

    private SpriteRenderer sprRend;
    private KeyCode fireKey;
    private KeyCode airKey;
    private KeyCode waterKey;
    private KeyCode earthKey;
    private bool isPaused;

    // Start is called before the first frame update
    private void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();

        sprRend.sprite = idleSprite;

        fireKey = GameData.currentSavedData.keyBindings["FireKey"];
        airKey = GameData.currentSavedData.keyBindings["AirKey"];
        waterKey = GameData.currentSavedData.keyBindings["WaterKey"];
        earthKey = GameData.currentSavedData.keyBindings["EarthKey"];

        isPaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isPaused)
        {
            // moves activator up and down
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (transform.position.y + 1.5f < 2f)
                {
                    transform.Translate(new Vector3(0f, 1.5f, 0f));
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (transform.position.y - 1.5f >= -1.5f)
                {
                    transform.Translate(new Vector3(0f, -1.5f, 0f));
                }
            }
            else
            {
                transform.position = new Vector3(-4f, 0f, 0f);
            }

            // determines activator shape
            if (Input.GetKey(fireKey))
            {
                sprRend.sprite = fireSprite;
                // sprRend.color = Color.red;
            }
            else if (Input.GetKey(airKey))
            {
                sprRend.sprite = airSprite;
                // sprRend.color = Color.grey;
            }
            else if (Input.GetKey(waterKey))
            {
                sprRend.sprite = waterSprite;
                // sprRend.color = Color.blue;
            }
            else if (Input.GetKey(earthKey))
            {
                sprRend.sprite = earthSprite;
                // sprRend.color = Color.green;
            }
            else
            {
                sprRend.sprite = idleSprite;
                // sprRend.color = Color.white;
            }
        }
    }

    /// <summary>
    /// Sets the value of isPaused to the _isPaused parameter.
    /// </summary>
    /// <param name="_isPaused"></param>
    public void PauseActivator(bool _isPaused)
    {
        isPaused = _isPaused;
    }
}
