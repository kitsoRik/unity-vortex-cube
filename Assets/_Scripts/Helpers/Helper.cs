using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Helper {

    public static DateTime FromSave(string key, char symbol = ';')
    {
        string[] _sixString = key.Split(symbol);
        int[] _sixInt = new int[6];
        try
        {
            for (int i = 0; i < 6; i++)
                _sixInt[i] = int.Parse(_sixString[i]);
        }
        catch (Exception)
        {
            return DateTime.Now;
        }
        return new DateTime(_sixInt[0], _sixInt[1], _sixInt[2],
                             _sixInt[3], _sixInt[4], _sixInt[5]);
    }

    public static string ToSave(DateTime date, char symbol = ';')
    {
        string[] _sixString = new string[6];
        _sixString[0] = date.Year.ToString();
        _sixString[1] = date.Month.ToString();
        _sixString[2] = date.Day.ToString();
        _sixString[3] = date.Hour.ToString();
        _sixString[4] = date.Minute.ToString();
        _sixString[5] = date.Second.ToString();
        return string.Join(symbol.ToString(), _sixString);
    }

    public static string GetBeginZero(int number)
    {
        return number < 10 ? (number > -10 ? "0" + number : number.ToString()) : number.ToString();
    }

    public static string MultChar(char _char, int count)
    {
        string retStr = "";
        for (int i = 0; i < count; i++)
            retStr += _char;
        return retStr;
    }

    public static SystemLanguage ReworkLanguage(SystemLanguage language, params SystemLanguage[] args)
    {
        foreach (SystemLanguage lang in args)
        {
            if (language == lang)
                return language;
        }
        return SystemLanguage.English;
    }

    public static SystemLanguage SystemLanguageParse(string systemLanguage)
    {
        switch (systemLanguage)
        {
            case "Ukrainian": return SystemLanguage.Ukrainian;
            case "Russian": return SystemLanguage.Russian;
            default: return SystemLanguage.English;
        }
    }


    public static Coroutine InvokeDelegate(this MonoBehaviour mn, Action func, float time)
    {
        if (!mn.isActiveAndEnabled) return null;
        return mn.StartCoroutine(InvokeDelegateCor(func, time));
    }

    public static int SummFromArray(int[] array)
    {
        int sum = 0;
        foreach (int item in array)
            sum += item;
        return sum;
    }

    public static float SummFromArray(float[] array)
    {
        float sum = 0;
        foreach (float item in array)
            sum += item;
        return sum;
    }

    public static T[] ToArray<T>(params T[] args)
    {
        return args;
    }

    public static void Adds<T>(this List<T> list, params T[] items)
    {
        foreach (T item in items)
            list.Add(item);
    }

    public static T RemoveAndAt<T>(this List<T> list, int index)
    {
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }

    public static DateTime GetToDay(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public static int ArrayHasItem(int[] array, params int[] wrongItems)
    {
        int sum = 0;
        foreach (int item in array)
        {
            sum++;
            foreach (int wrongItem in wrongItems)
                if (item == wrongItem)
                {
                    sum--;
                    break;
                }
        }
        return sum;
    }

    private static IEnumerator InvokeDelegateCor(Action func, float time)
    {
        var timer = 0f;
        while (timer <= time)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        if (func != null)
            func();
    }

    public static Color ColorWithTransparent(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static void DebugArray<T>(T[] array)
    {
        foreach (T item in array)
        {
            Debug.Log(item);
        }
    }

    public static Color InverseColor(Color color)
    {
        return new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
    }

    public static T[] CombiningArrays<T>(T[] array1, T[] array2)
    {
        int arr1L = array1.Length,
            arr2L = array2.Length;
        T[] retArr = new T[arr1L + arr2L];

        int i = 0;
        for (; i < arr1L; i++)
        {
            retArr[i] = array1[i];
        }

        for (int j = 0; j < arr2L; j++)
        {
            retArr[i + j] 
                = array2[j];
        }

        return retArr;
    }

    public static Color[] InverseColor(Color[] colors)
    {
        Color[] _colors = new Color[colors.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            _colors[i] = new Color(1 - colors[i].r, 1 - colors[i].g, 1 - colors[i].b, colors[i].a);
        }
        return _colors;
    }

    public static string ToMyTimeString(TimeSpan timeSpan)
    {
        return GetBeginZero(timeSpan.Minutes) + ":" + GetBeginZero(timeSpan.Seconds);
    }
}

public enum MathOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Remainder

}

public enum Boolean
{
    True,
    False,
    Other
}
