using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Problem
{
    public string ProblemID;
    public string Date;
    public string Auditorium;
    public string WorkplaceNumber;
    public string ProblemType;
    public string EquimpentID;
    public string EquimpentNumber;
    public string EquimpentType;
    public string Commentary;
}

public class ProblemDescriptionUI : MonoBehaviour
{
    [SerializeField] private Text _dateText;
    [SerializeField] private Text _auditoriumText;
    [SerializeField] private Text _workplaceText;
    [SerializeField] private Text _euqipmentTypeText;
    [SerializeField] private Text _problemType;
    [SerializeField] private Text _commentary;

    private Problem _problem;

    public void Initialize(Problem problem)
    {
        _problem = problem;

        _dateText.text = problem.Date;
        _auditoriumText.text = problem.Auditorium;
        _workplaceText.text = problem.WorkplaceNumber;
        _euqipmentTypeText.text = problem.EquimpentType + " №" + problem.EquimpentNumber;
        _problemType.text = problem.ProblemType;
        _commentary.text = problem.Commentary;
    }

    public void SendRequestToFixProblem()
    {
        Web.instance.TrySetProblemFixed(_problem.ProblemID);
        Debug.Log("Сообщение об исправленной проблеме отправлено!");
    }
}
