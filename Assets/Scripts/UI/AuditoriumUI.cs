using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AuditoriumUI : MonoBehaviour
{
    [SerializeField] private Text _auditoriumNumberText;
    [SerializeField] private Transform _auditoriumModelParent;

    [SerializeField] private WorkerPlaceDescriptionPanelUI _workerPlaceDescriptionPanel;

    public float _rotationSpeed;
    private float _mouseXpos;
    private float _mouseYpos;

    private string _auditoriumNumber;
    private bool _secondModeActive;

    public void InitializeAuditorium(string auditoriumNumber)
    {
        _auditoriumNumber = auditoriumNumber;
        _auditoriumNumberText.text = "План аудитории №" + auditoriumNumber;
        foreach(Transform _model in _auditoriumModelParent.GetComponentsInChildren<Transform>())
        {
            if(_model.gameObject.name != _auditoriumModelParent.name)
                Destroy(_model.gameObject);
        }
        Auditorium curentAuditorium = Instantiate(DataBaseAPI.GetAuditorium(auditoriumNumber).Model, _auditoriumModelParent).GetComponent<Auditorium>();
        foreach(WorkerPlace workerPlace in curentAuditorium.WorkerPlaces)
            workerPlace.WorkerPlaceClick += WorkerPlaceClick;
    }

    private void WorkerPlaceClick(string workerPlaceNumber)
    {
        Web.instance.GetWorkerPlaceInfo += GetWorkerPlaceInfo;
        Web.instance.TryGetWorkplaceInfo(workerPlaceNumber, _auditoriumNumber.ToString());

        Debug.Log("Рабочее место №" + workerPlaceNumber + " аудитории " + _auditoriumNumber + " поймало нажатие");
    }

    private void GetWorkerPlaceInfo(string workplaceNumber, List<Equipment> equipments)
    {
        _workerPlaceDescriptionPanel.Initialize(workplaceNumber, equipments);
        _workerPlaceDescriptionPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Set2DVision()
    {
        _secondModeActive = true;
        _auditoriumModelParent.localPosition = new Vector3(0, 0, -12);
        _auditoriumModelParent.localScale = new Vector3(80, 80, 80);
    }

    public void Set3DVision()
    {
        _secondModeActive = false;
        _auditoriumModelParent.localPosition = new Vector3(0, 0, -500);
        _auditoriumModelParent.localScale = new Vector3(38, 38, 38);
    }

    private void Update()
    {
        if (!_secondModeActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseXpos = Input.mousePosition.x;
                _mouseYpos = Input.mousePosition.y;
            }

            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > _mouseXpos)
                {
                    _auditoriumModelParent.eulerAngles += new Vector3(0f, _rotationSpeed * (Input.mousePosition.x - _mouseXpos) * Time.deltaTime, 0f);
                    _mouseXpos = Input.mousePosition.x;
                }

                if (Input.mousePosition.x < _mouseXpos)
                {
                    _auditoriumModelParent.eulerAngles -= new Vector3(0f, _rotationSpeed * (_mouseXpos - Input.mousePosition.x) * Time.deltaTime, 0f);
                    _mouseXpos = Input.mousePosition.x;
                }

                if (Input.mousePosition.y > _mouseYpos)
                {
                    _auditoriumModelParent.localEulerAngles += new Vector3(_rotationSpeed * (Input.mousePosition.y - _mouseYpos) * Time.deltaTime, 0f, 0f);
                    _mouseYpos = Input.mousePosition.y;
                }

                if (Input.mousePosition.y < _mouseYpos)
                {
                    _auditoriumModelParent.localEulerAngles -= new Vector3(_rotationSpeed * (_mouseYpos - Input.mousePosition.y) * Time.deltaTime, 0f, 0f);
                    _mouseYpos = Input.mousePosition.y;
                }
            }
        }
    }
}
