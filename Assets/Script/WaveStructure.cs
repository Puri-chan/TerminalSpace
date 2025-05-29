using UnityEngine;

public class WaveStructure
{
    // Number of wave
    public int Wave { get; set; }

    // false if over / not started yet
    public bool IsWaveActive { get; set; }

    public WaveStructure()
    {
        // default value when created
        Wave = 1;
        IsWaveActive = false;
    }
}
