using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperClass
{
    public static Color playerRed = new Color(0.35f, 0.15f, 0.15f, 1);
    public static Color playerGreen = new Color(0.1f, 0.2f, 0.1f, 1);
    public static Color playerBlue = new Color(0.15f, 0.15f, 0.35f, 1);
    public static List<Color> playerColors = new List<Color> { playerRed, playerGreen, playerBlue };

    public static Color particleRed = new Color(1f, 0.15f, 0.15f, 1);
    public static Color particleGreen = new Color(0.15f, 1f, 0.15f, 1);
    public static Color particleBlue = new Color(0.15f, 0.15f, 1f, 1);
    public static List<Color> particleColors = new List<Color> { particleRed, particleGreen, particleBlue };
}
