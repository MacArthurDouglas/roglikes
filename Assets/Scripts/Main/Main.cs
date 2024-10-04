using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 这里面放全局游戏数据或者方法
/// </summary>
public class Main : MonoBehaviour
{
    public static int MaxHealth=200;
    public static int Damage=10;
    public static Vector3 GetEulerAnglesByDirection(Vector2 currentDirection)
    {
        Vector3 eulerAngles = new Vector3(0, 0, 0);
        if (currentDirection.x == 0 && currentDirection.y == 1)
        {
            eulerAngles = new Vector3(0, 0, 0);
        }
        else if (currentDirection.x == 0 && currentDirection.y == -1)
        {
            eulerAngles = new Vector3(0, 0, 180);
        }
        else if (currentDirection.x == 1 && currentDirection.y == 0)
        {
            eulerAngles = new Vector3(0, 0, 270);
        }
        else if (currentDirection.x == -1 && currentDirection.y == 0)
        {
            eulerAngles = new Vector3(0, 0, 90);
        }
        else if (currentDirection.x == 1 && currentDirection.y == 1)
        {
            eulerAngles = new Vector3(0, 0, 315);
        }
        else if (currentDirection.x == -1 && currentDirection.y == -1)
        {
            eulerAngles = new Vector3(0, 0, 135);
        }
        else if (currentDirection.x == -1 && currentDirection.y == 1)
        {
            eulerAngles = new Vector3(0, 0, 45);
        }
        else if (currentDirection.x == 1 && currentDirection.y == -1)
        {
            eulerAngles = new Vector3(0, 0, 225);
        }
        return eulerAngles;
    }
}
