using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AuditoriumSchemesUI : MonoBehaviour
{
    [SerializeField] private GameObject _auditoriumButton;
    [SerializeField] private Transform _scrollContentParent;

    [SerializeField] private AuditoriumUI _auditoriumUI;

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
        Web.instance.GetAuditoriumsRequestResult += GetAuditoriumsRequestResult;
        Web.instance.TryGetAuditoriumsList(); 
    }

    private void GetAuditoriumsRequestResult(string[] auditoriums)
    {
        Web.instance.GetAuditoriumsRequestResult -= GetAuditoriumsRequestResult;
        foreach (string auditorium in auditoriums)
        {
            if (auditorium != "")
            {
                GameObject auditoriumInstance = Instantiate(_auditoriumButton, _scrollContentParent);
                auditoriumInstance.GetComponentInChildren<Text>().text = "Аудитория №" + auditorium;
                auditoriumInstance.GetComponent<RoomButtonUI>().Initialize(auditorium);
                auditoriumInstance.GetComponent<RoomButtonUI>().ButtonClick += ButtonClick;
            }
        }
    }

    private void ButtonClick(string auditoriumNumber)
    {
        _auditoriumUI.InitializeAuditorium(auditoriumNumber);
        _auditoriumUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ClearScrollContent()
    {
        foreach (RectTransform auditoriumTransform in _scrollContentParent.GetComponentInChildren<RectTransform>())
        {
            Destroy(auditoriumTransform.gameObject);
        }
    }
}
