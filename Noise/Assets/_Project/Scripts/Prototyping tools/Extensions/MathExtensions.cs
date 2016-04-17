using UnityEngine;
using System.Collections;

public class MathExtensions : MonoBehaviour
{

	public static float Ramp01SmoothOut(float t, float rampScale)
	{
		// - the "smooth out" curve ramps up quickly at the start and then flattens out at the end
		// - the higher the value of rampScale the more extreme the bend in the curve
		//   (zero rampScale means linear relationship)
		
		t = Mathf.Clamp01(t);	// expect t in range 0.0 -> 1.0
		
		if (rampScale <= 0.0f)
			return t;
		
		return EvalRamp01(t, 1.0f / (rampScale + 1.0f));	// output range 0.0 -> 1.0
	}
	
	
	public static float Ramp01SmoothInOut(float t)
	{
		// currently the shape of this curve is not adjustable
		// this curve is flat at the start and also at the end (symmetrical)
		
		// 3t^2 - 2t^3
		t = Mathf.Clamp01(t);	// expect t in range 0.0 -> 1.0
		float sq = t * t;
		return (3*sq) - (2*sq*t);	// output range 0.0 -> 1.0
	}

	static float EvalRamp01(float t, float factor)
	{
		// factor needs to be a positive value
		// - values above 1.0 create "smooth in" curve
		// - values below 1.0 create "smooth out" curve
		
		if (factor <= 0.0f || factor == 1.0f)
			return t;
		
		t = (1.0f / (1.0f - t + (t / factor))) - 1.0f;
		return t * (1.0f / (factor - 1.0f));
	}

	public static float AreaOfTriangle( Vector3 p0, Vector3 p1, Vector3 p2 )
	{
		float area;
		float edge0 = (p0 - p1).magnitude;
		float edge1 = (p1 - p2).magnitude;
		float edge2 = (p2 - p0).magnitude;
		
		float halfPerimeter = (edge0 + edge1 + edge2) / 2f;
		
		area = Mathf.Sqrt( halfPerimeter * (halfPerimeter - edge0) * (halfPerimeter - edge1) * (halfPerimeter - edge2) );
		
		return area;
	}

}
