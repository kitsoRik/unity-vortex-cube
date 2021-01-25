﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class s_code {

    #region RAM
    public static byte[] Key = Guid.NewGuid().ToByteArray();

    public static string Encode(string value)
    {
        return Convert.ToBase64String(Encode(Encoding.UTF8.GetBytes(value), Key));
    }

    public static string Decode(string value)
    {
        return Encoding.UTF8.GetString(Encode(Convert.FromBase64String(value), Key));
    }

    private static byte[] Encode(byte[] bytes, byte[] key)
    {
        var j = 0;

        for (var i = 0; i < bytes.Length; i++)
        {
            bytes[i] ^= key[j];

            if (++j == key.Length)
            {
                j = 0;
            }
        }

        return bytes;
    }
    #endregion

    #region ROM

    public static int KeyLength = 128;
    private const string SaltKey = "ShMG8hLyZ7k~Ge5@";
    private const string VIKey = "~6YUi0Sv5@|{aOZO"; // TODO: Generate random VI each encryption and store it with encrypted value

    public static string Encrypt(byte[] value, string password)// save
    {
        var keyBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(SaltKey)).GetBytes(KeyLength / 8);
        var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.UTF8.GetBytes(VIKey));

        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(value, 0, value.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
                memoryStream.Close();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }

    public static string Encrypt(string value, string password)// save
    {
        return Encrypt(Encoding.UTF8.GetBytes(value), password);
    }

    public static string Decrypt(string value, string password)// save
    {
        var cipherTextBytes = Convert.FromBase64String(value);
        var keyBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(SaltKey)).GetBytes(KeyLength / 8);
        var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.None };
        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.UTF8.GetBytes(VIKey));

        using (var memoryStream = new MemoryStream(cipherTextBytes))
        {
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                var plainTextBytes = new byte[cipherTextBytes.Length];
                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
        }
    }

    public static void ToSave<T>(T obj, string key, string password)
    {
        UnityEngine.PlayerPrefs.SetString(Encrypt(key, password), Encrypt(UnityEngine.JsonUtility.ToJson(obj), password));
    }

    public static void ToSave(string obj, string key, string password)
    {
        UnityEngine.PlayerPrefs.SetString(Encrypt(key, password), Encrypt(obj, password));
    }

    public static T FromSave<T>(string key, string password, T defaultvalue)
    {
        try
        {
            string _key = Encrypt(key, password);
            if (UnityEngine.PlayerPrefs.HasKey(_key))
                return UnityEngine.JsonUtility.FromJson<T>(Decrypt(UnityEngine.PlayerPrefs.GetString(_key), password));
        }
        catch (Exception)
        {

        }
        return defaultvalue;
    }

    public static string FromSave(string key, string password, string defaultvalue)
    {
        try
        {
            string _key = Encrypt(key, password);
            if (UnityEngine.PlayerPrefs.HasKey(_key))
                return Decrypt(UnityEngine.PlayerPrefs.GetString(_key), password);
        }
        catch (Exception) { }
       return defaultvalue;
    }

    #endregion
}
