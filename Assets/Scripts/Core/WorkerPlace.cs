using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public string equipmentID;
    public string equipmentType;
    public string equipmentNumber;
}

public class WorkerPlace : MonoBehaviour
{
    public delegate void OnWorkerPlaceClick(string workerPlaceNumber);
    public event OnWorkerPlaceClick WorkerPlaceClick;

    public string workerPlaceNumber;
    public List<Equipment> equipments = new List<Equipment>();


    private void OnMouseDown()
    {
        WorkerPlaceClick?.Invoke(workerPlaceNumber);
    }
}
