using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorkerPlaceDescriptionPanelUI : MonoBehaviour
{
    [SerializeField] private Text _workerPlaceNumberText;

    [SerializeField] private WorkerPlaceTicketPanelUI _workerPlaceTicketPanelUI;
    [SerializeField] private GameObject equipmentItemPrefab;

    [SerializeField] private Transform contentPosition;

    private string _workerPlaceNumberData;
    private List<Equipment> _equipmentsDataList;

    public void Initialize(string workerPlaceNumber, List<Equipment> equipments)
    {
        _workerPlaceNumberData = workerPlaceNumber;
        _equipmentsDataList = equipments;

        _workerPlaceNumberText.text = "Рабочее место №" + _workerPlaceNumberData.ToString();
        string euqip = "";

        foreach (Transform _model in contentPosition.GetComponentsInChildren<Transform>())
        {
            if (_model.gameObject.name != contentPosition.name)
                Destroy(_model.gameObject);
        }

        foreach (Equipment equipment in _equipmentsDataList)
        {
            GameObject equipmentItem = Instantiate(equipmentItemPrefab, contentPosition);
            equipmentItem.GetComponentInChildren<Text>().text = equipment.equipmentType + " " + equipment.equipmentNumber;
            //euqip = euqip + equipment.equipmentType + " " + equipment.equipmentNumber + "\n";
        }

        //_equipmentsText.text = euqip;
    }

    public void ShowProblemSendingUI()
    {
        gameObject.SetActive(false);
        _workerPlaceTicketPanelUI.gameObject.SetActive(true);
        _workerPlaceTicketPanelUI.Initialize(_workerPlaceNumberData, _equipmentsDataList);
    }
}
