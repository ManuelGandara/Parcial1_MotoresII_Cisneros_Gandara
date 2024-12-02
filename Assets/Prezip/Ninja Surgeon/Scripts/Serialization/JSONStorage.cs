using System;
using System.IO;
using UnityEngine;

public class JSONStorage<T>
{
    private string _path;
    private string _passKey;

    public JSONStorage(string fileName, string passKey = "admin1234")
    {
        string folderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/")}/{Application.productName}";

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        _path = $"{folderPath}/{fileName}.json";

        _passKey = passKey;
    }

    public void Persist(T data)
    {
        string json = JsonUtility.ToJson(data, true);

        string encryptedJson = EncryptDecrypt(json);

        File.WriteAllText(_path, encryptedJson);
    }

    public T Load(T defaultData)
    {
        if (FileDoesNotExist())
        {
            Persist(defaultData);

            return defaultData;
        }

        string encryptedJson = File.ReadAllText(_path);

        string json = EncryptDecrypt(encryptedJson);

        return JsonUtility.FromJson<T>(json);
    }

    public void Delete()
    {
        CheckIfDirectoryExists();

        File.Delete(_path);
    }

    private bool FileDoesNotExist()
    {
        return !File.Exists(_path);
    }

    private void CheckIfDirectoryExists()
    {
        if (FileDoesNotExist()) throw new Exception($"{_path} File Not Found");
    }

    private string EncryptDecrypt(string data)
    {
        string result = "";

        for (int i = 0; i < data.Length; i++)
            result += (char)(data[i] ^ _passKey[i % _passKey.Length]);

        return result;
    }
}
