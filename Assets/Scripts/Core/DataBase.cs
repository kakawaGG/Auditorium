using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum UserType
{
    None,
    Администратор,
    Техник,
    Сотрудник,
    Заведующийлабораториями
}

[Serializable]
public class User
{
    public string UserId;
    public string Login;
    public string Password;
    public string Name;
    public string Surname;
    public string Patronymic;
    public Sprite UserPhoto;
    public UserType UserType;
    public string UserTypeStr;
    public string Email;
}

[Serializable]
public class AuditoriumID
{
    public string Number;
    public GameObject Model;
}

public class DataBase : MonoBehaviour
{
    public List<AuditoriumID> AuditoriumsList = new List<AuditoriumID>();
}

