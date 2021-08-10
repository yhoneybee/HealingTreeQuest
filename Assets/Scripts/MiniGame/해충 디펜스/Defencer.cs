using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defencer : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    Direction direction;

    void Start()
    {
        direction = Direction.Up;
    }

    void Update()
    {
    }

    public void SetDirection(GameObject obj)
    {
        switch (obj.name)
        {
            case "Left":
                direction = Direction.Left;
                break;
            case "Right":
                direction = Direction.Right;
                break;
            case "Up":
                direction = Direction.Up;
                break;
            case "Down":
                direction = Direction.Down;
                break;
        }
    }
}
