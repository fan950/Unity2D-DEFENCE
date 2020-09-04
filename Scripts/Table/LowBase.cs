﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowBase
{
    private Dictionary<int, Dictionary<string, string>> m_data =
        new Dictionary<int, Dictionary<string, string>>();

    public void Load(string text)
    {
        string[] rows = text.Split('\n');

        for (int i = 0; i < rows.Length; ++i)
        {
            if (rows[i].Contains("\r"))
            {
                rows[i] = rows[i].Remove(rows[i].Length - 1);
            }
        }

        int rowCount = rows.Length;

        if (string.IsNullOrEmpty(rows[rows.Length - 1]))
        {
            rowCount--;
        }

        string[] subject = rows[0].Split(',');

        for (int row = 1; row < rowCount; ++row)
        {
            string[] values = rows[row].Split(',');

            int val = -1;
            if (int.TryParse(values[0], out val))
            {
                if (m_data.ContainsKey(val) == false)
                {
                    m_data.Add(val, new Dictionary<string, string>());
                }
            }

            for (int col = 1; col < subject.Length; ++col)
            {
                if (m_data[val].ContainsKey(subject[col]) == false)
                {

                    m_data[val].Add(subject[col], values[col]);


                }

            }
        }
    }

    public int ToI(int mainKey, string subKey)
    {
        if (m_data.ContainsKey(mainKey))
        {
            if (m_data[mainKey].ContainsKey(subKey))
            {
                string val = m_data[mainKey][subKey];
                int iVal = 0;
                if (int.TryParse(val, out iVal))
                    return iVal;

                return -1;
            }
        }

        return -1;
    }

    public float ToF(int mainKey, string subKey)
    {
        if (m_data.ContainsKey(mainKey))
        {
            if (m_data[mainKey].ContainsKey(subKey))
            {
                string val = m_data[mainKey][subKey];
                float iVal = 0;
                if (float.TryParse(val, out iVal))
                    return iVal;

                return -1;
            }
        }

        return -1;
    }

    public char ToC(int mainKey, string subKey)
    {
        if (m_data.ContainsKey(mainKey))
        {
            if (m_data[mainKey].ContainsKey(subKey))
            {
                string val = m_data[mainKey][subKey];
                char iVal = 'F';
                if (char.TryParse(val, out iVal))
                    return iVal;

                return ' ';
            }
        }

        return ' ';
    }
    public string ToS(int mainKey, string subKey)
    {
        if (m_data.ContainsKey(mainKey))
        {
            if (m_data[mainKey].ContainsKey(subKey))
            {
                string val = m_data[mainKey][subKey];
                return val;
            }
        }
        return string.Empty;
    }
}
