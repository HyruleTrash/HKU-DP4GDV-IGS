using UnityEngine;

public static class ColorExtensions
{
    public static string ToHex(this Color color)
    {
        int r = (int)(color.r * 255);
        int g = (int)(color.g * 255);
        int b = (int)(color.b * 255);
        int a = (int)(color.a * 255);
        
        return $"#{r:X2}{g:X2}{b:X2}{a:X2}";
    }
}