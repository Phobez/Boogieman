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

    public GameObject firePrefab;
    public GameObject airPrefab;
    public GameObject waterPrefab;
    public GameObject earthPrefab;

    public bool createMode;

    private Note.Shape currentShape;

    private GameObject note;
    private GameObject gameManager;

    private SpriteRenderer sprRend;

    private bool active;
    private bool isPressing;
    private bool isLongPressing;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");

        sprRend = GetComponent<SpriteRenderer>();

        sprRend.sprite = idleSprite;

        currentShape = Note.Shape.IDLE;

        active = false;
        isPressing = false;
        isLongPressing = false;
    }

    // Update is called once per frame
    private void Update()
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

        if (createMode)
        {
            if (Input.GetKeyDown(fireKey))
            {
                Instantiate(firePrefab, transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown(airKey))
            {
                Instantiate(airPrefab, transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown(waterKey))
            {
                Instantiate(waterPrefab, transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown(earthKey))
            {
                Instantiate(earthPrefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            // determines activator shape
            if (Input.GetKey(fireKey))
            {
                currentShape = Note.Shape.FIRE;
                sprRend.sprite = fireSprite;
                // sprRend.color = Color.red;
                isPressing = true;
            }
            else if (Input.GetKey(airKey))
            {
                currentShape = Note.Shape.AIR;
                sprRend.sprite = airSprite;
                // sprRend.color = Color.grey;
                isPressing = true;
            }
            else if (Input.GetKey(waterKey))
            {
                currentShape = Note.Shape.WATER;
                sprRend.sprite = waterSprite;
                // sprRend.color = Color.blue;
                isPressing = true;
            }
            else if (Input.GetKey(earthKey))
            {
                currentShape = Note.Shape.EARTH;
                sprRend.sprite = earthSprite;
                // sprRend.color = Color.green;
                isPressing = true;
            }
            else
            {
                currentShape = Note.Shape.IDLE;
                sprRend.sprite = idleSprite;
                // sprRend.color = Color.white;
                isPressing = false;
            }

            // Debug.Log(active);
            // determines if activator hits note or not
            if (active)
            {
                if (note != null && note.GetComponent<Note>().shape == currentShape)
                {
                    if (note.CompareTag("Note"))
                    {
                        Destroy(note);
                        gameManager.GetComponent<GameManager>().AddStreak();
                        AddScore();
                        active = false;
                    }
                    else if (note.CompareTag("Long Note"))
                    {
                        if (!isLongPressing)
                        {
                            isLongPressing = true;
                        }
                        gameManager.GetComponent<GameManager>().AddStreak();
                        AddScore();
                    }
                    else if (note.CompareTag("Empowered Note"))
                    {
                        Destroy(note);
                        gameManager.GetComponent<GameManager>().AddStreak();
                        AddScore();
                        AddPower();
                        active = false;
                    }
                }
            }
        }
    }

    // a method to add to the score
    private void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gameManager.GetComponent<GameManager>().GetScore());
    }

    // a method to add to the power
    // NOTE: FIND A WAY TO MOVE TO GAMEMANAGER
    private void AddPower()
    {
        if (PlayerPrefs.GetInt("Power") + 50 < 100)
        {
            PlayerPrefs.SetInt("Power", PlayerPrefs.GetInt("Power") + gameManager.GetComponent<GameManager>().GetPower());
        }
        else
        {
            PlayerPrefs.SetInt("Multiplier", PlayerPrefs.GetInt("Multiplier") * 2);
            PlayerPrefs.SetInt("Power", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Victory Note"))
        {
            gameManager.GetComponent<GameManager>().Win();
        }

        if (collision.gameObject.CompareTag("Note") || collision.gameObject.CompareTag("Long Note") || collision.gameObject.CompareTag("Empowered Note"))
        {
            if (!isPressing)
            {
                active = true;
                note = collision.gameObject;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Long Note"))
        {
            if (isLongPressing && note.GetComponent<Note>().shape != currentShape)
            {
                // Destroy(note);
                gameManager.GetComponent<GameManager>().ResetStreak();
                active = false;
                isLongPressing = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }
}
