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

    private GameObject note;

    private SpriteRenderer sprRend;

    private bool active;

    // Start is called before the first frame update
    private void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();

        sprRend.sprite = idleSprite;

        active = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // determines if activator hits note or not
        if (active)
        {
            if (Input.GetKeyDown(fireKey))
            {
                if (note.GetComponent<Note>().shape == Note.Shape.FIRE)
                {
                    Destroy(note);
                }
            }
            else if (Input.GetKeyDown(airKey))
            {
                if (note.GetComponent<Note>().shape == Note.Shape.AIR)
                {
                    Destroy(note);
                }
            }
            else if (Input.GetKeyDown(waterKey))
            {
                if (note.GetComponent<Note>().shape == Note.Shape.WATER)
                {
                    Destroy(note);
                }
            }
            else if (Input.GetKeyDown(earthKey))
            {
                if (note.GetComponent<Note>().shape == Note.Shape.EARTH)
                {
                    Destroy(note);
                }
            }
        }

        // determines activator shape
        if (Input.GetKey(fireKey))
        {
            sprRend.sprite = fireSprite;
            sprRend.color = Color.red;
        }
        else if (Input.GetKey(airKey))
        {
            sprRend.sprite = airSprite;
            sprRend.color = Color.grey;
        }
        else if (Input.GetKey(waterKey))
        {
            sprRend.sprite = waterSprite;
            sprRend.color = Color.blue;
        }
        else if (Input.GetKey(earthKey))
        {
            sprRend.sprite = earthSprite;
            sprRend.color = Color.green;
        }
        else
        {
            sprRend.sprite = idleSprite;
            sprRend.color = Color.white;
        }
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
