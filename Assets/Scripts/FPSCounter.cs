using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display;

    public enum DisplayMode { FPS, MS }

    [SerializeField] private DisplayMode displayMode = DisplayMode.FPS;
    [SerializeField, Range(.1f, 2f)] private float sampleDuration = 1f;
    int frames;
    float duration, bestDuration = float.MaxValue, worstDuration;


    private void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames++;
        duration += frameDuration;

        if (frameDuration < bestDuration) bestDuration = frameDuration;
        if (frameDuration > worstDuration) worstDuration = frameDuration;

        if (duration >= sampleDuration)
        {
            if (displayMode == DisplayMode.FPS)
            {
                display.SetText(
                    "FPS\n{0.0}\n{1.0}\n{2.0}",
                    1f / bestDuration,
                    frames / duration,
                    1f / worstDuration
                );
            }
            else
            {
                display.SetText(
                    "MS\n{0:1}\n{1:1}\n{2:1}",
                    1000f * bestDuration,
                    1000f * duration / frames,
                    1000f * worstDuration
                );
            
            }
            frames = 0;
            duration = 0;
            bestDuration = float.MaxValue;
            worstDuration = 0;
        }


    }
}
