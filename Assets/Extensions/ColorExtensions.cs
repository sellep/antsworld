using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ColorExtensions
{

    public static void SetAlpha(this Color c, float alpha)
    {
        c.a = alpha;
    }
}