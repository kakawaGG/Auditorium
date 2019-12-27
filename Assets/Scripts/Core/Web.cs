using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public delegate void OnGetLoginRequestResult(User user);
    public event OnGetLoginRequestResult GetLoginRequestResult;

    public delegate void OnGetUsersRequestResult(List<User> users);
    public event OnGetUsersRequestResult GetUsersRequestResult;

    public delegate void OnGetAuditoriumsRequestResult(string[] auditoriums);
    public event OnGetAuditoriumsRequestResult GetAuditoriumsRequestResult;

    public delegate void OnGetWorkerPlaceInfo(string workplaceNumber, List<Equipment> equipments);
    public event OnGetWorkerPlaceInfo GetWorkerPlaceInfo;

    public delegate void OnGetProblemsInfo(List<Problem> problems);
    public event OnGetProblemsInfo GetProblemsInfo;

    public static Web instance;

    private const string AUTORISATION_COMMAND = "0";
    private const string GET_AUDITORIUMS_COMMAND = "1";
    private const string GET_WORPLACEINFO_COMMAND = "2";
    private const string GET_EQUIPMENT_PROBLEMSINFO_COMMAND = "3";
    private const string SET_EQUIPMENT_FIXPROBLEM_COMMAND = "4";
    private const string SET_EQUIPMENT_PROBLEM_COMMAND = "5";
    private const string GET_ALLUSERS_COMMAND = "6";
    private const string ADD_USER_COMMAND = "7";
    private const string DEL_USER_COMMAND = "8";

    public string idAdress;

    void Awake()
    {
        instance = this;
    }

    public void TryLogin(string userName, string userPassword)
    {
        instance.StartCoroutine(instance.Autorisation(userName, userPassword));
    }

    public void TryGetAuditoriumsList()
    {
        instance.StartCoroutine(instance.GettingAuditorium());
    }    

    public void TryGetWorkplaceInfo(string workplaceNumber, string auditorium)
    {
        instance.StartCoroutine(instance.GettingWorplaceInfo(workplaceNumber, auditorium));
    }

    public void TryGetProblemsInfo()
    {
        instance.StartCoroutine(instance.GettingProblemsInfo());
    }

    public void TrySetProblemFixed(string problemID)
    {
        instance.StartCoroutine(instance.SettingProblemFix(problemID));
    }

    public void TrySetProblem(Problem problem)
    {
        instance.StartCoroutine(instance.SettingProblem(problem));
    }

    public void TryGetAllUsers()
    {
        instance.StartCoroutine(instance.GettingUsers());
    }

    public void TryDeleteUser(string userId)
    {
        instance.StartCoroutine(instance.DeleteUser(userId));
    }

    public void TryAddUser(User user)
    {
        instance.StartCoroutine(instance.AddUser(user));
    }

    IEnumerator Autorisation(string userName, string userPassword)
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", AUTORISATION_COMMAND);
        form.AddField("loginUser", userName);
        form.AddField("passwordUser", userPassword);

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/authorisation.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
                User resultUser = RequestDataConverter.GetUserByBytes(results);
                GetLoginRequestResult?.Invoke(resultUser);
            }
        }
    }

    IEnumerator GettingAuditorium()
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", GET_AUDITORIUMS_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/authorisation.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
                string[] auditoriums = RequestDataConverter.GetAuditoriums(results);
                GetAuditoriumsRequestResult?.Invoke(auditoriums);
            }
        }
    }

    IEnumerator GettingWorplaceInfo(string workplaceNumber, string auditorium)
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", GET_WORPLACEINFO_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);
        form.AddField("auditorium", auditorium);
        form.AddField("workplaceNumber", workplaceNumber);

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/workplaces.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
                List<Equipment> eqipmentInfo = RequestDataConverter.GetWorkplaceInfo(results);
                foreach (Equipment eqipment in eqipmentInfo)
                    Debug.Log(eqipment.equipmentID + " " + eqipment.equipmentType + " " + eqipment.equipmentNumber);
                GetWorkerPlaceInfo?.Invoke(workplaceNumber, eqipmentInfo);
            }
        }
    }

    IEnumerator GettingProblemsInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", GET_EQUIPMENT_PROBLEMSINFO_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);
        form.AddField("problemID", "0");

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/equipmentProblems.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
                List<Problem> problems = RequestDataConverter.GetProblems(results);
                GetProblemsInfo?.Invoke(problems);
            }
        }
    }

    IEnumerator SettingProblemFix(string problemID)
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", SET_EQUIPMENT_FIXPROBLEM_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);
        form.AddField("problemID", problemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/equipmentProblems.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
        }
    }

    IEnumerator SettingProblem(Problem problem)
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", SET_EQUIPMENT_PROBLEM_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);
        form.AddField("equipmentID", problem.EquimpentID);
        form.AddField("problemTypeID", problem.ProblemType);
        form.AddField("description", problem.Commentary);
        form.AddField("date", problem.Date);

        Debug.Log(problem.EquimpentID + " " + problem.ProblemType + " " + problem.Commentary + " " + problem.Date);
        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/equipmentProblemsSet.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
                Debug.Log(System.Convert.ToString(results));
            }
        }
    }

    IEnumerator GettingUsers()
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", GET_ALLUSERS_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/users.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
                Debug.Log(System.Convert.ToString(results));

                List<User> users = RequestDataConverter.GetUsersByBytes(results);
                GetUsersRequestResult?.Invoke(users);
            }
        }
    }


    IEnumerator DeleteUser(string userId)
    {
        WWWForm form = new WWWForm();
        form.AddField("commandNumber", DEL_USER_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);
        form.AddField("userId", userId);
        form.AddField("userLoginNew", "0");
        form.AddField("userPasswordNew", "0");
        form.AddField("userRole", "0");
        form.AddField("userName", "0");
        form.AddField("userPatronymic", "0");
        form.AddField("userEmail", "0");

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/userAdd.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
        }
    }

    IEnumerator AddUser(User user)
    {
        WWWForm form = new WWWForm();
        int userType = (int)user.UserType;

        form.AddField("commandNumber", ADD_USER_COMMAND);
        form.AddField("loginUser", AutorisationUI.CurrentUser.Login);
        form.AddField("passwordUser", AutorisationUI.CurrentUser.Password);
        form.AddField("userId", "0");
        form.AddField("userLoginNew", user.Login);
        form.AddField("userPasswordNew", user.Password);
        form.AddField("userRole", userType.ToString());
        form.AddField("userName", user.Name);
        form.AddField("userSurname", user.Surname);
        form.AddField("userPatronymic", user.Patronymic);
        form.AddField("userEmail", user.Email);

        using (UnityWebRequest www = UnityWebRequest.Post("http://" + idAdress + "/Auditorium/userAdd.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
        }
    }
}
