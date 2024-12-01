using System;
using System.IO;
using UnityEngine;

public class JSONStorage<T>
{
    private string _path;
    private string _passKey;
    private T _defaultData;

    public JSONStorage(string fileName, T defaultData, string passKey = "admin1234")
    {
        string folderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/")}/{Application.productName}";

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        _path = $"{folderPath}/{fileName}.json";

        _passKey = passKey;

        _defaultData = defaultData;
    }

    public void Persist(T data)
    {
        string json = JsonUtility.ToJson(data, true);

        string encryptedJson = EncryptDecrypt(json);

        File.WriteAllText(_path, encryptedJson);
    }

    public T Load()
    {
        if (!File.Exists(_path)) throw new Exception($"{_path} File Not Found");

        string encryptedJson = File.ReadAllText(_path);

        string json = EncryptDecrypt(encryptedJson);

        return JsonUtility.FromJson<T>(json);
    }

    public void Delete(bool hardDelete = false)
    {
        if (hardDelete && File.Exists(_path))
        {
            File.Delete(_path);
        }
        else
        {
            Persist(_defaultData);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string result = "";

        for (int i = 0; i < data.Length; i++)
            result += (char)(data[i] ^ _passKey[i % _passKey.Length]);

        return result;
    }
}
