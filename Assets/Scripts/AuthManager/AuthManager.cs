using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;



public class AuthManager : MonoBehaviour
{
    // Data about screens
    [Header("Screens")]
    public GameObject userUI;
    public GameObject loginUI;
    public GameObject userUI2;
    public GameObject userOptionUI;

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
    public TextMeshProUGUI buttonMale;
    public TextMeshProUGUI buttonFemale;
    public TMP_Text femaleValue;
    public TMP_Text maleValue;
    public TMP_Text usernameText;
    public TMP_Text usernameText2;
    public TMP_Text userNameTextOptions;

    //User Data Images
    [Header("GenderUserImages")]
    public GameObject femaleImage;
    public GameObject maleImage;
    public GameObject femaleImageUserMenu;
    public GameObject maleImageUserMenu;

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
            string message = "Ingreso fallido";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    //message = "Missing Email";
                    message = "Ingrese el correo";
                    break;
                case AuthError.MissingPassword:
                    //message = "Missing Password";
                    message = "Ingrese la contraseña";
                    break;
                case AuthError.WrongPassword:
                    //message = "Wrong Password";
                    message = "Contraseña incorrecta";
                    break;
                case AuthError.InvalidEmail:
                    //message = "Invalid Email";
                    message = "Correo invalido";
                    break;
                case AuthError.UserNotFound:
                    //message = "Account does not exist";
                    message = "Este usuario no se encuentra registrado";
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
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "¡Sesión Iniciada!";

            // setting usernametext
            usernameText.text = User.DisplayName;
            usernameText2.text = User.DisplayName;
            userNameTextOptions.text = User.DisplayName;
            StartCoroutine(LoadUserMenu());
            yield return new WaitForSeconds(2);
            UIManager.instance.OptionsScreen();


        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            //warningRegisterText.text = "Missing Username";
            warningRegisterText.text = "Ingrese un nombre de usuario";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            //warningRegisterText.text = "Password Does Not Match!";
            warningRegisterText.text = "las contraseñas no coinciden";
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
                string message = "Registro fallido";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        //message = "Missing Email";
                        message = "Ingrese el correo";
                        break;
                    case AuthError.MissingPassword:
                        //message = "Missing Password";
                        message = "Ingrese una contraseña";
                        break;
                    case AuthError.WeakPassword:
                        //message = "Weak Password";
                        message = "Contraseña débil";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        //message = "Email Already In Use";
                        message = "Este correo ya se encuentra registrado";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                confirmRegisterText.text = "¡Se registró exitosamente!";
                User = RegisterTask.Result;
                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth updatUserOptionsScreene user profile function passing the profile with the username
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
                        usernameText.text = User.DisplayName;
                        usernameText2.text = User.DisplayName;
                        userNameTextOptions.text = User.DisplayName;
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
                maleImageUserMenu.SetActive(true);
                maleImage.SetActive(true);

            }
            else if(snapshot.Child("gender").Value.ToString() == "Femenino")
            {
                femaleImageUserMenu.SetActive(true);
                femaleImage.SetActive(true);
                maleImage.SetActive(false);
                maleImageUserMenu.SetActive(false);
            }

        }

    }

}
