using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsersListUI : MonoBehaviour
{
    [SerializeField] private GameObject _problemButton;
    [SerializeField] private Transform _scrollContentParent;

    [SerializeField] private UserDescriptionUI _userDescriptionUI;
    [SerializeField] private UserDescriptionUI _userAddingUI;

    public List<User> UsersList;

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
        Web.instance.GetUsersRequestResult += GetUsersInfo;
        Web.instance.TryGetAllUsers();
    }

    private void GetUsersInfo(List<User> users)
    {
        Web.instance.GetUsersRequestResult -= GetUsersInfo;
        UsersList = users;
        if (users != null)
            foreach (User user in users)
            {
                GameObject auditoriumInstance = Instantiate(_problemButton, _scrollContentParent);
                auditoriumInstance.GetComponent<UserButtonUI>().Initialize(user);
                auditoriumInstance.GetComponent<UserButtonUI>().ButtonClick += ButtonClick;
            }
    }

    private void ButtonClick(User user)
    {
        _userDescriptionUI.Initialize(user);
        _userDescriptionUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ClearScrollContent()
    {
        foreach (RectTransform problemsTransform in _scrollContentParent.GetComponentInChildren<RectTransform>())
        {
            Destroy(problemsTransform.gameObject);
        }
    }

    public void OpenAddingUserPanel()
    {

    }
}
