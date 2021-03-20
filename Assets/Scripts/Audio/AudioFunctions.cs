using UnityEngine;

public class AudioFunctions : MonoBehaviour
{
    /// <summary>
    /// Converts Decibel Logarithmic scale float value into linear scale value from 0 to 1 for to feed game engine functions.
    /// </summary>
    /// <param name="dB"></param>
    /// <returns>float value of the conversion</returns>
    public static float DbToLinear(float dB)
    {
        if (dB > -80)
        {
            return Mathf.Clamp01(Mathf.Pow(10.0f, dB / 20.0f));
        }
        else
            return 0;
    }

    /// <summary>
    /// Converts linear scale slider value from .01 to 1 into corresponding Decibel log value for the mixer.
    /// </summary>
    /// <param name="linear"></param>
    /// <returns>float value of the conversion</returns>
    public static float LinearToDb(float linear)
    {
        if (linear > 0)
        {
            return Mathf.Clamp(20.0f * Mathf.Log10(linear), -80f, 0f);
        }
        else
            return -80.0f;
    }

    /// <summary>
    /// Static value representing one semi-tone
    /// </summary>
    private static float twelfthRootOfTwo = Mathf.Pow(2, 1.0f / 12);

    /// <summary>
    /// Converts semitones into pitchshift float value
    /// </summary>
    /// <param name="st"></param>
    /// <returns>float value of the conversion</returns>
    public static float St2pitch(float st)
    {
        return Mathf.Clamp(Mathf.Pow(twelfthRootOfTwo, st), 0f, 4f);
    }

    /// <summary>
    /// Converts pitchshift float value into semitones
    /// </summary>
    /// <param name="pitch"></param>
    /// <returns>float value of the conversion</returns>
    public static float Pitch2st(float pitch)
    {
        return Mathf.Log(pitch, twelfthRootOfTwo);
    }

}
