﻿using System.Collections;
using UnityEngine;

/// <summary>
/// The component which handles the movement and behaviour of regular Notes.
/// </summary>
public class NoteMover : MonoBehaviour
{
    public GameObject activator;
    public SongParser.NoteType noteType;

    protected NoteGenerator noteGenerator;
    protected float noteSpeed = 0.0f;
    protected KeyCode keyToPress;
    protected ScoreHandler scoreHandler;
    protected bool isDespawning;

    protected const float hitOffset = 0.075f;
    protected const float despawnTime = 1.5f;

    // Start is called before the first frame update
    protected void Start()
    {
        noteGenerator = GameObject.FindGameObjectWithTag("GameController").GetComponent<NoteGenerator>();
        scoreHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreHandler>();
        activator = GameObject.FindGameObjectWithTag("Player");

        isDespawning = false;

        switch (noteType)
        {
            case SongParser.NoteType.Fire:
                keyToPress = GameData.currentSavedData.keyBindings["FireKey"];
                break;
            case SongParser.NoteType.Air:
                keyToPress = GameData.currentSavedData.keyBindings["AirKey"];
                break;
            case SongParser.NoteType.Water:
                keyToPress = GameData.currentSavedData.keyBindings["WaterKey"];
                break;
            case SongParser.NoteType.Earth:
                keyToPress = GameData.currentSavedData.keyBindings["EarthKey"];
                break;
            //default:
            //    keyToPress = KeyCode.W;
            //    break;
        }

        // Debug.Log(keyToPress.ToString());
    }

    // Update is called once per frame
    protected void Update()
    {
        noteSpeed = noteGenerator.noteSpeed;
        Vector3 _tempPos = transform.position;
        _tempPos.x -= noteSpeed;
        transform.position = _tempPos;

        if (Input.GetKeyDown(keyToPress))
        {
            //Debug.Log("Key to Press if entered successfully.");
            CheckLocation();
        }
        // missed
        if (transform.position.x < activator.transform.position.x - hitOffset && !isDespawning)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.0f, 0.0f));
            StartCoroutine(DespawnNote());
            isDespawning = true;
        }
    }

    // checks if it can be hit
    // destroys note if it can
    protected virtual void CheckLocation()
    {
        if ((transform.position.x >= activator.transform.position.x - hitOffset && transform.position.x <= activator.transform.position.x + hitOffset) && (transform.position.y == activator.transform.position.y))
        {
            //Debug.Log("Checking location.");
            scoreHandler.SendMessage("AddScore");
            Destroy(this.gameObject);
        }
    }

    // despawns note if missed
    protected virtual IEnumerator DespawnNote()
    {
        scoreHandler.SendMessage("LoseEnergy");
        //Debug.Log("Lose energy sent.");
        scoreHandler.SendMessage("ResetStreak");
        //Debug.Log("Reset streak sent.");
        yield return new WaitForSeconds(despawnTime);
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger entered.");
        //if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(keyToPress))
        //{
        //    Debug.Log("Trigger entered successfully.");
        //    CheckLocation();
        //}
    }
}
