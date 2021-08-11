using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Direction direction;
    Vector2 targetPos = Vector2.zero;

    public void SetDirection(Direction dir)
    {
        direction = dir;
        switch (direction)
        {
            case Direction.Left:
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                break;
            case Direction.Up:
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                break;
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.05f);
    }
}
