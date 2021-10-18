using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ChangeGroundColor : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private bool active;
    Tilemap tilemap;
    private int idColor;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.color = colors[0];

    }
    public void ChangeTileMapColor()
    {
        active = !active;
        if (active)
            idColor = 0;
        else idColor = 1;

        tilemap.color = colors[idColor];
    }

    private void OnEnable()
    {
        Player.OnChangedState += ChangeTileMapColor;
    }
    private void OnDisable()
    {

        Player.OnChangedState -= ChangeTileMapColor;
    }
}
