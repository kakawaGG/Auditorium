using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerPlaceTicketPanelUI : MonoBehaviour
{
    [SerializeField] private Text _workerPlaceText;
    [SerializeField] private Dropdown _workerEquipmentDropdown;
    [SerializeField] private Dropdown _problemTypeDropdown;
    [SerializeField] private InputField _commentaryinputField;

    private List<Equipment> _equipmentsList = new List<Equipment>();

    public void Initialize(string workerPlaceNumber, List<Equipment> eqipments)
    {
        _equipmentsList = eqipments;

        _workerPlaceText.text = "Рабочее место №" + workerPlaceNumber;
        _workerEquipmentDropdown.ClearOptions();
        _commentaryinputField.text = "";

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (Equipment equipment in eqipments)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = equipment.equipmentType + " " + equipment.equipmentNumber;
            options.Add(option);
        }
        _workerEquipmentDropdown.AddOptions(options);
    }

    public void SendWorkplaceTiket()
    {
        Equipment equipment = FindEuqipmentByName(_workerEquipmentDropdown.captionText.text);

        Problem problem = new Problem();
        problem.EquimpentID = equipment.equipmentID;
        problem.ProblemType = (_problemTypeDropdown.value + 1).ToString();
        problem.Commentary = _commentaryinputField.text;
        problem.Date = System.DateTime.Now.ToString("yyyy/MM/dd");

        Web.instance.TrySetProblem(problem);
        Debug.Log("Сообщение об неисправности на рабочем месте отправлено!" + " Номер проблемы: " + (_problemTypeDropdown.value + 1).ToString());
    }

    private Equipment FindEuqipmentByName(string name)
    {
        Equipment equipment = new Equipment();
        foreach(Equipment eq in _equipmentsList)
        {
            if (eq.equipmentType + " " + eq.equipmentNumber == name)
                return eq;
        }
        return null;
    }
}
