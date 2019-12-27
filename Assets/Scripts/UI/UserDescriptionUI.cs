using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDescriptionUI : MonoBehaviour
{
    [SerializeField] private Text _userLogin;
    [SerializeField] private Text _userPassword;
    [SerializeField] private Text _userRole;
    [SerializeField] private Text _userName;
    [SerializeField] private Text _userSurname;
    [SerializeField] private Text _userPatronimyc;
    [SerializeField] private Text _userEmail;

    [SerializeField] private GameObject _deleteButton;
    private User _user;

    private void OnEnable()
    {
        if(AutorisationUI.CurrentUser.Login == _userLogin.text && AutorisationUI.CurrentUser.Password == _userPassword.text)
        {
            _deleteButton.SetActive(false);
        }
        else
        {
            _deleteButton.SetActive(true);
        }
    }

    public void Initialize(User user)
    {
        _user = user;

        UserType type = (UserType)System.Enum.Parse(typeof(UserType), user.UserType.ToString());

        _userLogin.text = user.Login;
        _userPassword.text = user.Password;
        _userRole.text = type.ToString();
        _userName.text = user.Name;
        _userSurname.text = user.Surname;
        _userPatronimyc.text = user.Patronymic;
        _userEmail.text = user.Email;
    }

    public void TryDeleteUser()
    {
        Web.instance.TryDeleteUser(_user.UserId);
    }
}
