using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;



public class AuthManager : MonoBehaviour
{

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public TMP_Text confirmRegisterText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;


    //User Data variables
    [Header("UserData")]
    public TMP_Text femaleValue;
    public TMP_Text maleValue;
    public TMP_Text userNameCase;
    public TMP_Text userNameLevels;
    public TMP_Text userNameCaseOptions;
    public Transform scoreboardContent;
    public GameObject scoreElement;


    //User Data Images
    [Header("GenderUserImages")]
    public GameObject femaleImage;
    public GameObject maleImage;
    public GameObject femaleImageUserMenu;
    public GameObject maleImageUserMenu;
    public GameObject femaleImageCase;
    public GameObject maleImageCase;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    //Function for the save button
    public void SaveDataButton()
    {

    }

    public void SetGenderMaleDataButton()
    {
        StartCoroutine(UpdateGenderDatabase(maleValue.text));
    }

    public void SetGenderFemaleDataButton()
    {
        StartCoroutine(UpdateGenderDatabase(femaleValue.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            //string message = "Login Failed!";
            string message = "INGRESO FALLIDO";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    //message = "Missing Email";
                    message = "INGRESE UN CORREO";
                    break;
                case AuthError.MissingPassword:
                    //message = "Missing Password";
                    message = "INGRESE LA CONTRASEÑA";
                    break;
                case AuthError.WrongPassword:
                    //message = "Wrong Password";
                    message = "CONTRASEÑA INCORRECTA";
                    break;
                case AuthError.InvalidEmail:
                    //message = "Invalid Email";
                    message = "CORREO NO VÁLIDO";
                    break;
                case AuthError.UserNotFound:
                    //message = "Account does not exist";
                    message = "ESTE USUARIO NO SE ENCUENTRA REGISTRADO";
                    break;
            }
            confirmLoginText.text = "";
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            if (User.Email == "profesor@gmail.com")
            {
                Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
                yield return new WaitForSeconds(2);
                UIManager.instance.ProfessorOptions();
            }
            else
            {

                Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
                warningLoginText.text = "";
                confirmLoginText.text = "¡SESIÓN INICIADA!";

                // setting userNameCase
                userNameCase.text = User.DisplayName;
                userNameLevels.text = User.DisplayName;
                userNameCaseOptions.text = User.DisplayName;
                StartCoroutine(LoadUserMenu());
                yield return new WaitForSeconds(2);
                UIManager.instance.OptionsScreen();
            }


        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            //warningRegisterText.text = "Missing Username";
            warningRegisterText.text = "INGRESE UN NOMBRE DE USUARIO";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            //warningRegisterText.text = "Password Does Not Match!";
            warningRegisterText.text = "LAS CONTRASEÑAS NO COINCIDEN";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                //string message = "Register Failed!";
                string message = "REGISTRO FALLIDO";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        //message = "Missing Email";
                        message = "INGRESE EL CORREO";
                        break;
                    case AuthError.MissingPassword:
                        //message = "Missing Password";
                        message = "INGRESE UNA CONTRASEÑA";
                        break;
                    case AuthError.WeakPassword:
                        //message = "Weak Password";
                        message = "CONTRASEÑA DÉBIL - MÍNIMO 6 CARACTERES";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        //message = "Email Already In Use";
                        message = "ESTE CORREO YA SE ENCUENTRA REGISTRADO";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                confirmRegisterText.text = "¡SE REGISTRÓ EXITOSAMENTE!";
                User = RegisterTask.Result;
                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        userNameCase.text = User.DisplayName;
                        userNameLevels.text = User.DisplayName;
                        userNameCaseOptions.text = User.DisplayName;
                        UIManager.instance.SelectCharacterScreen();
                    }
                }
            }
        }
    }

    private IEnumerator UpdateGenderDatabase(string _gender)
    {

        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("gender").SetValueAsync(_gender);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated           
            UIManager.instance.LoginScreenFromCharacters();
        }
    }

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("No hay información");
        }
        else
        {
        }
    }

    private IEnumerator LoadUserMenu()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        else if (DBTask.Result.Value == null)
        {
            Debug.Log("No hay información");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            if (snapshot.Child("gender").Value.ToString() == "Masculino")
            {

                femaleImageUserMenu.SetActive(false);
                femaleImage.SetActive(false);
                femaleImageCase.SetActive(false);
                maleImageUserMenu.SetActive(true);
                maleImage.SetActive(true);
                maleImageCase.SetActive(true);

            }
            else if (snapshot.Child("gender").Value.ToString() == "Femenino")
            {
                femaleImageUserMenu.SetActive(true);
                femaleImage.SetActive(true);
                femaleImageCase.SetActive(true);
                maleImage.SetActive(false);
                maleImageUserMenu.SetActive(false);
                maleImageCase.SetActive(false);
            }

        }

    }

    /* Scoreboard */
    //Function for the scoreboard button
    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    private IEnumerator LoadScoreboardData()
    {
        //Get all the usersCollection data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("kills").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every usersCollection UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int kills = int.Parse(childSnapshot.Child("kills").Value.ToString());
                int deaths = int.Parse(childSnapshot.Child("deaths").Value.ToString());
                int xp = int.Parse(childSnapshot.Child("xp").Value.ToString());

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, kills, deaths, xp);
            }

            //Go to scoareboard screen
            UIManager.instance.ScoreboardScreen();
        }
    }

}
