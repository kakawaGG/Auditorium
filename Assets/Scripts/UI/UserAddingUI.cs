using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAddingUI : MonoBehaviour
{
    [SerializeField] private InputField _userLogin;
    [SerializeField] private InputField _userPassword;
    [SerializeField] private Dropdown _userRole;
    [SerializeField] private InputField _userName;
    [SerializeField] private InputField _userSurname;
    [SerializeField] private InputField _userPatronimyc;
    [SerializeField] private InputField _userEmail;

    [SerializeField] private GameObject _falseImage;
    [SerializeField] private GameObject _shortPass;
    [SerializeField] private GameObject _loginFail;
    [SerializeField] private UsersListUI _userListPanel;

    private void OnEnable()
    {
        _falseImage.SetActive(false);
        _shortPass.SetActive(false);
        _loginFail.SetActive(false);
        ClearFields();
    }

    private void ClearFields()
    {
        _userLogin.text = "";
        _userPassword.text = "";
        _userName.text = "";
        _userSurname.text = "";
        _userPatronimyc.text = "";
    }

    public void TryAddUser()
    {
        User user = new User();
        user.Login = _userLogin.text;
        user.Password = _userLogin.text;
        user.UserType = (UserType)System.Enum.Parse(typeof(UserType), _userRole.captionText.text);
        user.Name = _userName.text;
        user.Surname = _userSurname.text;
        user.Patronymic = _userPatronimyc.text;
        user.Email = _userEmail.text;

        if (_userLogin.text == "" || _userPassword.text == "" || _userName.text == "" || _userSurname.text == "")
        {
            _falseImage.SetActive(true);
            StopCoroutine("HideFailPanel");
            StartCoroutine("HideFailPanel");
        } else if (_userPassword.text.Length < 5)
        {
            _shortPass.SetActive(true);
            StopCoroutine("HideFailPanel");
            StartCoroutine("HideFailPanel");
        } else if (!CheckAnotherUser())
        {
            _loginFail.SetActive(true);
            StopCoroutine("HideFailPanel");
            StartCoroutine("HideFailPanel");
        }
        else
        {
            Web.instance.TryAddUser(user);
            _userListPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private bool CheckAnotherUser()
    {
        foreach (User user in _userListPanel.UsersList)
            if (user.Login == _userLogin.text)
                return false;
        return true;
    }

    private IEnumerator HideFailPanel()
    {
        yield return new WaitForSeconds(2f);
        _falseImage.SetActive(false);
        _shortPass.SetActive(false);
        _loginFail.SetActive(false);
    }
}
