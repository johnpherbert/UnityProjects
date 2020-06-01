using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float ScreenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    // Start is called before the first frame update
    Ball theBall;
    GameSession session;
    void Start()
    {
        theBall = FindObjectOfType<Ball>();
        session = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"{Input.mousePosition.x / Screen.width * ScreenWidthInUnits}");
        float currentpos = GetXPos();
        Vector2 paddlePos = new Vector2(Mathf.Clamp(currentpos, minX, maxX), .25f);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (session.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * ScreenWidthInUnits;
        }
    }
}
