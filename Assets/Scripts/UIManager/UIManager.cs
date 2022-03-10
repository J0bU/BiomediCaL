using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Firebase variables
    [Header("Firebase")]
    // User display
    public FirebaseUser User;

    //Screen variables
    [Header("Screens")]
    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject menuUI;
    public GameObject startUI;
    public GameObject userUI;
    public GameObject levelsUI;
    public GameObject tutorialUI;
    public GameObject characterUI;
    public GameObject userOptionsUI;

    //Backgrounds variables
    [Header("Backgrounds")]
    public GameObject initialBackground;
    public GameObject menuBackground;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI


    /*
     * @name MainMenuScreen
     * @function: Move the user from start game to principal menu (login, quit, register)
     * Use: yes - 1
     */

    public void MainMenuScreen()
    {
        startUI.SetActive(false);
        menuUI.SetActive(true);
    }
    /*
     * @name LoginScreen
     * @function: Move the user from principal menu to login (login, quit, register)
     * Use: yes - 2
     */
    public void LoginScreen()
    {
        menuUI.SetActive(false);
        loginUI.SetActive(true);
    }

    /*
     * @name StartScreenLogin
     * @function: Move the user from login form to principal menu (login, quit, register)
     * Use: yes - 3
     */
    public void MenuScreenFromLogin()
    {
        loginUI.SetActive(false);
        menuUI.SetActive(true);
    }

    /*
     * @name RegisterScreen
     * @function: Move the user from principal menu to register (login, quit, register)
     * Use: yes - 4
     */
    public void RegisterScreen()
    {
        menuUI.SetActive(false);
        registerUI.SetActive(true);
    }

    /*
     * @name MenuScreenFromRegister
     * @function: Move the user from register to principal menu (login, quit, register)
     * Use: yes - 5
     */
    public void MenuScreenFromRegister()
    {
        registerUI.SetActive(false);
        menuUI.SetActive(true);
    }

    /*
    * @name OptionsScreen
    * @function: Move the user from login to options (cases, forum, settings)
    * Use: yes - 6
    */
    public void OptionsScreen()
    {
        loginUI.SetActive(false);
        userOptionsUI.SetActive(true);
    }

    /*
    * @name UserScreen
    * @function: Move the user from login to user menu (case 1)
    * Use: yes - 7
    */
    public void UserScreen()
    {
        userOptionsUI.SetActive(false);
        userUI.SetActive(true);
    }

    /*
    * @name OptionsScreenFromUser
    * @function: Move the user from user menu to options (cases, forum, settings)
    * Use: yes - 8
    */
    public void OptionsScreenFromUser()
    {
        userUI.SetActive(false);
        userOptionsUI.SetActive(true);
    }

    /*
    * @name LoginScreenFromRegister
    * @function: Move the user from characters to login (when the user is created)
    * Use: yes - 9
    */
    public void LoginScreenFromCharacters() //Back button 
    {
        characterUI.SetActive(false);
        loginUI.SetActive(true);
    }

    /*
    * @name LevelsToUserScreen
    * @function: Move the user from levels to user menu (case 1)
    * Use: yes - 10
    */
    public void LevelsToUserScreen()
    {
        levelsUI.SetActive(false);
        userUI.SetActive(true);
    }

    /*
    * @name TutorialScreen
    * @function: Move the user from levels to the tutorial ()
    * Use: yes - 11
    */

    public void TutorialScreen()
    {
        levelsUI.SetActive(false);
        tutorialUI.SetActive(true);
    }

    /*
    * @name TutorialToLevelsScreen
    * @function: Move the user from tutorial to the levels ()
    * Use: yes - 12
    */

    public void TutorialToLevelsScreen()
    {
        tutorialUI.SetActive(false);
        levelsUI.SetActive(true);
    }


    /*
    * @name LevelsScreen
    * @function: Move the user from select case to the levels ()
    * Use: yes - 13
    */

    public void LevelsScreen()
    {
        userUI.SetActive(false);
        levelsUI.SetActive(true);
    }


    /*
   * @name SelectCharacterScreen
   * @function: Move the user from register to select the character()
   * Use: yes - 14
   */
    public void SelectCharacterScreen()
    {
        characterUI.SetActive(true);
        registerUI.SetActive(false);
    }

    /*
  * @name BackgroundChanger
  * @function: Change the initial background to the second
  * Use: yes - 15
  */
    public void BackgroundChanger()
    {
        initialBackground.SetActive(false);
        menuBackground.SetActive(true);

    }

    /*
 * @name BackgroundChangerInitial
 * @function: Change the second for the initial backgroundå
 * Use: yes - 15
 */

    public void BackgroundChangerInitial()
    {
        initialBackground.SetActive(true);
        menuBackground.SetActive(false);
    }


}
