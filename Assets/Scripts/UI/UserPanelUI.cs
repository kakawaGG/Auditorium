using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanelUI : MonoBehaviour
{
    [SerializeField] private Text _userName;
    [SerializeField] private Text _userSurname;
    [SerializeField] private Text _userPatronymic;
    [SerializeField] private Text _userRole;

    private void OnEnable()
    {
        _userName.text = AutorisationUI.CurrentUser.Name;
        _userSurname.text = AutorisationUI.CurrentUser.Surname;
        _userPatronymic.text = AutorisationUI.CurrentUser.Patronymic;
        _userRole.text = AutorisationUI.CurrentUser.UserTypeStr;
    }
}
