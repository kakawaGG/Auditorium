using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutorisationUI : MonoBehaviour
{
    [SerializeField] private InputField _loginInputField;
    [SerializeField] private InputField _passwordInputField;
    [SerializeField] private InputField _ipField;

    [SerializeField] private GameObject[] _allPanels;
    [SerializeField] private GameObject _userPanel;
    [SerializeField] private GameObject[] _workerPanel;
    [SerializeField] private GameObject[] _administratorPanel;
    [SerializeField] private GameObject[] _technikalPanel;
    [SerializeField] private GameObject[] _auditoruimMemberPanel;

    public static User CurrentUser;

    public void TryLogin()
    {
        Web.instance.idAdress = _ipField.text;
        Web.instance.GetLoginRequestResult += GetLoginRequestResult;
        Web.instance.TryLogin(_loginInputField.text, _passwordInputField.text);
    }

    public void TryLoginByCurrentUser()
    {
        Web.instance.idAdress = _ipField.text;
        Web.instance.GetLoginRequestResult += GetLoginRequestResult;
        Web.instance.TryLogin(CurrentUser.Login, CurrentUser.Password);
    }

    private void GetLoginRequestResult(User user)
    {
        if (user != null)
        {
            ClearInputFields();
            DeactivatePanels();
            ShowPanelByUserType(user);

            _userPanel.SetActive(true);
            Debug.Log("Авторизация пользователя " + user.Name + " прошла успешно!");
        }
        else
        {
            ClearInputFields();
            Debug.Log("Такого пользователя не существует!");
        }
        Web.instance.GetLoginRequestResult -= GetLoginRequestResult;
    }

    private void ClearInputFields()
    {
        _loginInputField.text = "";
        _passwordInputField.text = "";
    }

    private void DeactivatePanels()
    {
        foreach (GameObject ob in _allPanels)
            ob.SetActive(false);
    }

    private void ShowPanelByUserType(User user)
    {
        CurrentUser = user;
        switch (user.UserType)
        {
            case UserType.Администратор:
                ActivatePanelParts(_administratorPanel);
                break;
            case UserType.Заведующийлабораториями:
                ActivatePanelParts(_auditoruimMemberPanel);
                break;
            case UserType.Техник:
                ActivatePanelParts(_technikalPanel);
                break;
            case UserType.Сотрудник:
                ActivatePanelParts(_workerPanel);
                break;
        }
        gameObject.SetActive(false);
    }

    private void ActivatePanelParts(GameObject[] parts)
    {
        foreach (GameObject part in parts)
            part.SetActive(true);
    }

    #region Test logins
    public void EnterLikeABoss()
    {
        Web.instance.idAdress = _ipField.text;
        Web.instance.GetLoginRequestResult += GetLoginRequestResult;
        Web.instance.TryLogin("admin", "admin");
    }

    public void EnterLikeATechnik()
    {
        Web.instance.idAdress = _ipField.text;
        Web.instance.GetLoginRequestResult += GetLoginRequestResult;
        Web.instance.TryLogin("katya", "katya123");
    }

    public void EnterLikeAWorker()
    {
        Web.instance.idAdress = _ipField.text;
        Web.instance.GetLoginRequestResult += GetLoginRequestResult;
        Web.instance.TryLogin("vasya", "vasya123");
    }
    #endregion
}
