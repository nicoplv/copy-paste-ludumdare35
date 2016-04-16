using System.Collections.Generic;

public static class FloatExtensions
{
	public static float Sqr(this float _value)
	{
		return _value * _value;
    }

    public static float NormalizeAngle(this float _value)
    {
        // Normalize between -360 and 360
        _value = _value % 360f;

        // Normalize between 0 and 360
        if (_value < 0)
            _value += 360;

        // Normalize between -180 and 180
        if (_value > 180)
            _value -= 360;

        return _value;
    }
}