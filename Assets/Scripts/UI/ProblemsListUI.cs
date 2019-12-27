using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemsListUI : MonoBehaviour
{
    [SerializeField] private GameObject problemButton;
    [SerializeField] private Transform scrollContentParent;

    [SerializeField] private ProblemDescriptionUI problemDescriptionUI;

    private void OnEnable()
    {
        FillScrollContent();
    }

    private void OnDisable()
    {
        ClearScrollContent();
    }

    private void FillScrollContent()
    {
        Web.instance.GetProblemsInfo += GetProblemsInfo;
        Web.instance.TryGetProblemsInfo();
    }

    private void GetProblemsInfo(List<Problem> problems)
    {
        Web.instance.GetProblemsInfo -= GetProblemsInfo;
        if(problems != null)
        foreach (Problem problem in problems)
        {
            GameObject auditoriumInstance = Instantiate(problemButton, scrollContentParent);
            auditoriumInstance.GetComponent<ProblemButtonUI>().Initialize(problem);
            auditoriumInstance.GetComponent<ProblemButtonUI>().ButtonClick += ButtonClick;
        }
    }

    private void ButtonClick(Problem problem)
    {
        problemDescriptionUI.Initialize(problem);
        problemDescriptionUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ClearScrollContent()
    {
        foreach (RectTransform problemsTransform in scrollContentParent.GetComponentInChildren<RectTransform>())
        {
            Destroy(problemsTransform.gameObject);
        }
    }
}
