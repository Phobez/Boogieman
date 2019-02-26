using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public KeyCode fireKey;
    public KeyCode airKey;
    public KeyCode waterKey;
    public KeyCode earthKey;

    public Sprite idleSprite;
    public Sprite fireSprite;
    public Sprite airSprite;
    public Sprite waterSprite;
    public Sprite earthSprite;

    private Note.Shape currentShape;

    private GameObject note;

    private SpriteRenderer sprRend;

    private bool active;

    // Start is called before the first frame update
    private void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();

        sprRend.sprite = idleSprite;

        currentShape = Note.Shape.IDLE;

        active = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // determines activator shape
        if (Input.GetKey(fireKey))
        {
            currentShape = Note.Shape.FIRE;
            sprRend.sprite = fireSprite;
            sprRend.color = Color.red;
        }
        else if (Input.GetKey(airKey))
        {
            currentShape = Note.Shape.AIR;
            sprRend.sprite = airSprite;
            sprRend.color = Color.grey;
        }
        else if (Input.GetKey(waterKey))
        {
            currentShape = Note.Shape.WATER;
            sprRend.sprite = waterSprite;
            sprRend.color = Color.blue;
        }
        else if (Input.GetKey(earthKey))
        {
            currentShape = Note.Shape.EARTH;
            sprRend.sprite = earthSprite;
            sprRend.color = Color.green;
        }
        else
        {
            currentShape = Note.Shape.IDLE;
            sprRend.sprite = idleSprite;
            sprRend.color = Color.white;
        }

        // determines if activator hits note or not
        if (active)
        {
            if (note.GetComponent<Note>().shape == currentShape)
            {
                Destroy(note);
                AddScore();
                active = false;
            }
        }
    }

    private void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            active = true;
            note = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }
}
