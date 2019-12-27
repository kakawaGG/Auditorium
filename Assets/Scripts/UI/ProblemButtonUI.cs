using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemButtonUI : MonoBehaviour
{
    public delegate void OnButtonClick(Problem problem);
    public event OnButtonClick ButtonClick;

    [SerializeField] private Text _leftText;
    [SerializeField] private Text _rightText;

    private Problem _problem;
    private Button _button;

    public void Initialize(Problem problem)
    {
        _rightText.text = "Аудитория №" + problem.Auditorium;
        _leftText.text = problem.Date;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClickEvent);
        _problem = problem;
    }

    private void OnButtonClickEvent()
    {
        ButtonClick?.Invoke(_problem);
    }
}
