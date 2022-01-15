using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeDico
{
    public static string GetThemeInBaseFromTheme(string theme)
    {
        string themeInBase = "";
        switch (theme)
        {
            case "Environnement":
                themeInBase = "environment";
                break;
            case "Sciences":
                themeInBase = "science";
                break;
            case "Histoire":
                themeInBase = "history";
                break;
            case "G�ographie":
                themeInBase = "geography";
                break;
            case "Sport":
                themeInBase = "sport";
                break;
            case "Maths":
                themeInBase = "mathematics";
                break;
            case "Cin�ma":
                themeInBase = "movie";
                break;
            case "Multim�dia":
                themeInBase = "multimedia";
                break;
            case "Culture G.":
                themeInBase = "general_culture";
                break;
            case "Animaux":
                themeInBase = "animals";
                break;
            case "Art":
                themeInBase = "art";
                break;
            case "Logique":
                themeInBase = "logic";
                break;
            case "Devinettes":
                themeInBase = "riddles";
                break;
            case "Orthographe":
                themeInBase = "spelling";
                break;
            case "R�bus":
                themeInBase = "rebus";
                break;
            default:
                throw new System.Exception("Pas de th�me correspondant en base pour le th�me : " + theme);
        }
        return themeInBase;
    }
}
