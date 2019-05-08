using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component which controls Beatmap Editor Activator behaviour.
/// </summary>
public class EditorActivatorController : ActivatorController
{
    public GameObject fireNotePrefab;
    public GameObject airNotePrefab;
    public GameObject waterNotePrefab;
    public GameObject earthNotePrefab;
    public GameObject empoweredFireNotePrefab;
    public GameObject empoweredAirNotePrefab;
    public GameObject empoweredWaterNotePrefab;
    public GameObject empoweredEarthNotePrefab;

    private Vector3 instantiatePos;

    // Start is called before the first frame update
    protected override void Start()
    {
        fireKey = KeyCode.W;
        airKey = KeyCode.A;
        waterKey = KeyCode.D;
        earthKey = KeyCode.S;

        isPaused = false;

        // 0.55f is hit offset
        instantiatePos = new Vector3(transform.position.x - 0.55f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    protected override void Update()
    {
        instantiatePos.y = transform.position.y;

        if (!isPaused)
        {
            // moves activator up and down
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (transform.position.y + 1.5f < 2f)
                {
                    transform.Translate(new Vector3(0f, 1.5f, 0f));
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (transform.position.y - 1.5f >= -1.5f)
                {
                    transform.Translate(new Vector3(0f, -1.5f, 0f));
                }
            }

            // determines instantiated note
            if (Input.GetKeyDown(fireKey))
            {
                InstantiateNote(SongParser.NoteType.Fire, IsOnEmpoweredLane());
            }
            else if (Input.GetKeyDown(airKey))
            {
                InstantiateNote(SongParser.NoteType.Air, IsOnEmpoweredLane());
            }
            else if (Input.GetKeyDown(waterKey))
            {
                InstantiateNote(SongParser.NoteType.Water, IsOnEmpoweredLane());
            }
            else if (Input.GetKeyDown(earthKey))
            {
                InstantiateNote(SongParser.NoteType.Earth, IsOnEmpoweredLane());
            }
        }
    }

    /// <summary>
    /// Instantiates a note according to parameter settings.
    /// </summary>
    /// <param name="_noteType">The type of the note to be instantiated.</param>
    /// <param name="_isEmpowered">Determines whether the note is empowered or not.</param>
    private void InstantiateNote(SongParser.NoteType _noteType, bool _isEmpowered)
    {
        GameObject _notePrefab = null;

        switch (_noteType)
        {
            case SongParser.NoteType.Fire:
                if (_isEmpowered)
                {
                    _notePrefab = fireNotePrefab;
                }
                else
                {
                    _notePrefab = empoweredFireNotePrefab;
                }
                break;
            case SongParser.NoteType.Air:
                if (_isEmpowered)
                {
                    _notePrefab = airNotePrefab;
                }
                else
                {
                    _notePrefab = empoweredAirNotePrefab;
                }
                break;
            case SongParser.NoteType.Water:
                if (_isEmpowered)
                {
                    _notePrefab = waterNotePrefab;
                }
                else
                {
                    _notePrefab = empoweredWaterNotePrefab;
                }
                break;
            case SongParser.NoteType.Earth:
                if (_isEmpowered)
                {
                    _notePrefab = earthNotePrefab;
                }
                else
                {
                    _notePrefab = empoweredEarthNotePrefab;
                }
                break;
        }

        if (_notePrefab != null)
        {
            Instantiate(_notePrefab, instantiatePos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Checks whether the Activator is on an empowered lane or not.
    /// </summary>
    /// <returns>Return a boolean of whether the Activator is on an empowered lane or not.</returns>
    private bool IsOnEmpoweredLane()
    {
        if (transform.position.y >= -0.5f && transform.position.y <= 0.5f)
        {
            return true;
        }
        return false;
    }
}
