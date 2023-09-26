using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject Parametre;
    //fonction du bouton jouer lance la scene EnterScene en chargeant la scene
    public void chargerScene(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
    //fonction du bouton option active le canvas paramètre
    public void OptionButton()
    {
        Parametre.SetActive(true);
    }
    //fonction du bouton quitter option désactive le canvas paramètre
    public void QuitterReglageMenu()
    {
        Parametre.SetActive(false);
    }
    //fonction quitter, quitte le jeu
    public void Exit()
    {
        if (Application.isEditor)//jeu tourne dans l'editeur
        {
#if UNITY_EDITOR //Build = Directive de compilation(transformation langage de haut niveau en langage machine)  = si editor =/ compilation ignoré
            UnityEditor.EditorApplication.isPlaying = false;
#endif


        }
        else
        {
            Application.Quit();
        }
    }
}
