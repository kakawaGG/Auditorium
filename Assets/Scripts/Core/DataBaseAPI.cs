using System.Collections;
using System.Collections.Generic;

public static class DataBaseAPI
{
    private static DataBase dataBase;

    public static void SetDataBase(DataBase _dataBase)
    {
        dataBase = _dataBase;
    }

    public static List<AuditoriumID> GetAuditoriums()
    {
        return dataBase.AuditoriumsList;
    }

    public static AuditoriumID GetAuditorium(string number)
    {
        foreach (AuditoriumID auditorium in dataBase.AuditoriumsList)
        {
            if (auditorium.Number == number)
            {
                return auditorium;
            }
        }
        return null;
    }
}
