using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButtonUI : MonoBehaviour
{
    public delegate void OnButtonClick(string auditoriumNumber);
    public event OnButtonClick ButtonClick;

    private string _auditoriumNumber;
    private Button _button;

    public void Initialize(string auditoriumNumber)
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClickEvent);
        _auditoriumNumber = auditoriumNumber;
    }

    private void OnButtonClickEvent()
    {
        ButtonClick?.Invoke(_auditoriumNumber);
    }
}
