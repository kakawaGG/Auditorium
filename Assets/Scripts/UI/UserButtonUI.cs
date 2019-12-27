using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserButtonUI : MonoBehaviour
{
    public delegate void OnButtonClick(User user);
    public event OnButtonClick ButtonClick;

    public Text leftText;
    public Text rightText;

    User _user;
    Button _button;

    public void Initialize(User user)
    {
        leftText.text = user.Surname + " " + user.Name + " " + user.Patronymic;
        UserType type = (UserType)System.Enum.Parse(typeof(UserType), user.UserType.ToString());
        rightText.text = type.ToString();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClickEvent);
        _user = user;
    }

    private void OnButtonClickEvent()
    {
        ButtonClick?.Invoke(_user);
    }
}
