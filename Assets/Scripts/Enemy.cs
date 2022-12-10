using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum typefff
{
    type1,
    type2,
    type3
}

public class Enemy : MonoBehaviour
{
    public int weight;
    public typefff type;


    public void OnEnable()
    {
        Invoke("Death", 3f);
    }

    public void SetPosition(Vector3 position)
    {

    }

    public void Death()
    {
        EventManager.SendEnemyKilled(this);
    }

}
