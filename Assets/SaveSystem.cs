using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveCustomizer(Customizer customizer)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/customizer1.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, customizer);

        stream.Close();
    }

    public static Customizer LoadCustomizer()
    {
        string path = Application.persistentDataPath + "/customizer1.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Customizer customizer = formatter.Deserialize(stream) as Customizer;

            stream.Close();

            return customizer;
        }
        else
        {
            Debug.Log("Save file not found.");
            return null;
        }
    }

    public static CoinProgress LoadCoinProgress()
    {
        string path = Application.persistentDataPath + "/CoinProgress.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CoinProgress cp = formatter.Deserialize(stream) as CoinProgress;

            stream.Close();

            return cp;
        }
        else
        {
            Debug.Log("Save file not found.");
            return null;
        }
    }

    public static void SaveCoinProgress(CoinProgress cp)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/CoinProgress.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, cp);

        stream.Close();
    }
}
