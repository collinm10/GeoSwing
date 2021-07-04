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
}
