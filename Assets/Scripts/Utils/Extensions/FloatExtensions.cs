using System.Collections.Generic;

public static class FloatExtensions
{
	public static List<float> RandomAngles(float _angleForward, float _angleIncrement, float _angleRandom)
	{
		List<float> angles = new List<float>();
		int sign = 1;
		if (UnityEngine.Random.Range(0, 2) == 0)
			sign = -1;
		float angle = UnityEngine.Random.Range(-_angleRandom, _angleRandom);
		angles.Add(angle);
		angle += _angleIncrement + UnityEngine.Random.Range(-_angleRandom, _angleRandom);
		while (angle < _angleForward)
		{
			angles.Add(angle * sign);
			angles.Add(angle * -sign);
			angle += _angleIncrement + UnityEngine.Random.Range(-_angleRandom, _angleRandom);
		}
		return angles;
	}

	public static float Sqr(this float _value)
	{
		return _value * _value;
	}
}