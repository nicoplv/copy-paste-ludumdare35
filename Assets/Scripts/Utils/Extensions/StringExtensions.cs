using System;

public static class StringExtensions
{
    public static string Clean(this string _value)
    {
        string output = "";
        bool nextUpper = true;

        for (int i = 0; i < _value.Length; i++)
        {
            bool nextUpperBuffer = nextUpper;
            nextUpper = false;

            if (_value[i] == ' ')
            {
                nextUpper = true;
                continue;
            }

            if (nextUpperBuffer)
                output += Char.ToUpper(_value[i]);
            else
                output += _value[i];
        }

        return output;
    }
}