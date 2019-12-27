using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDataBase : MonoBehaviour
{
    void Start()
    {
        DataBaseAPI.SetDataBase(GetComponent<DataBase>());
    }

    void Update()
    {
        
    }
}
