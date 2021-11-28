using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperClass
{
    public static Color playerOrange = new Color(0.5f, 0.2f, 0f, 1);
    public static Color playerGreen = new Color(0.1f, 0.3f, 0.1f, 1);
    public static Color playerPurple = new Color(0.3f, 0f, 0.4f, 1);
    public static List<Color> playerColors = new List<Color> { playerOrange, playerGreen, playerPurple };

    public static Color particleOrange = new Color(0.5f, 0.2f, 0f, 1);
    public static Color particleGreen = new Color(0.15f, 1f, 0.15f, 1);
    public static Color particlePurple = new Color(0.3f, 0f, 0.4f, 1);
    public static List<Color> particleColors = new List<Color> { particleOrange, particleGreen, particlePurple };
}
