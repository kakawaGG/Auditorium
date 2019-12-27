using System.Collections;
using System.Collections.Generic;
using System;

public static class RequestDataConverter
{
    public static User GetUserByBytes(byte[] bytes)
    {
        string bytesStr = System.Text.Encoding.UTF8.GetString(bytes);
        if (bytesStr == "0") return null;

        string[] rows = bytesStr.Split('*');
        User user = new User();

        user.Login = rows[0];
        user.Password = rows[1];
        user.UserType = (UserType)Convert.ToInt32(rows[2]);
        user.UserTypeStr = rows[6];
        user.Name = rows[3];
        user.Surname = rows[4];
        user.Patronymic = rows[5];

        return user;
    }

    public static List<User> GetUsersByBytes(byte[] bytes)
    {
        string bytesStr = System.Text.Encoding.UTF8.GetString(bytes);
        if (bytesStr == "0") return null;

        string[] rows = bytesStr.Split('*');

        int usersAmount = rows.Length;
        List<User> userList = new List<User>();

        for (int i = 0; i < usersAmount - 1; i += 8)
        {
            User user = new User();
            user.UserId = rows[i];
            user.Login = rows[i+1];
            user.Password = rows[i+2];
            user.UserType = (UserType)Convert.ToInt32(rows[i+3]);
            user.UserTypeStr = rows[i+3];
            user.Name = rows[i+4];
            user.Surname = rows[i+5];
            user.Patronymic = rows[i+6];
            user.Email = rows[i + 7];

            userList.Add(user);
        }

        return userList;
    }

    public static string[] GetAuditoriums(byte[] bytes)
    {
        string bytesStr = System.Text.Encoding.UTF8.GetString(bytes);
        if (bytesStr == "0") return null;

        string[] rows = bytesStr.Split('*');

        return rows;
    }

    public static List<Equipment> GetWorkplaceInfo(byte[] bytes)
    {
        string bytesStr = System.Text.Encoding.UTF8.GetString(bytes);
        if (bytesStr == "0") return null;

        string[] rows = bytesStr.Split('*');

        int eqipmentsAmount = rows.Length;
        List<Equipment> eqipmentsList = new List<Equipment>();

        for (int i = 0; i < eqipmentsAmount-1; i += 3)
        {
            Equipment equipment = new Equipment();
            equipment.equipmentID = rows[i];
            equipment.equipmentType = rows[i + 1];
            equipment.equipmentNumber = rows[i + 2];

            eqipmentsList.Add(equipment);
        }

        return eqipmentsList;
    }

    public static List<Problem> GetProblems(byte[] bytes)
    {
        string bytesStr = System.Text.Encoding.UTF8.GetString(bytes);
        if (bytesStr == "0") return null;

        string[] rows = bytesStr.Split('*');

        int problemsAmount = rows.Length;
        List<Problem> problemsList = new List<Problem>();

        for(int i = 0; i < problemsAmount-1; i += 9)
        {
            Problem problem = new Problem();
            problem.ProblemID = rows[i];
            problem.EquimpentID = rows[i + 1];
            problem.EquimpentType = rows[i + 2];
            problem.EquimpentNumber = rows[i + 3];
            problem.Commentary = rows[i + 4];
            problem.Date = rows[i + 5];
            problem.Auditorium = rows[i + 6];
            problem.WorkplaceNumber = rows[i + 7];
            problem.ProblemType = rows[i + 8];

            problemsList.Add(problem);
        }

        return problemsList;
    }
}
