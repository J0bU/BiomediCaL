using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine;


public class FirebaseTest : MonoBehaviour
{
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DependencyStatus dependencyStatus;
    public DatabaseReference DBreference;
    [SerializeField] private PointsSystem pointsSystem;
    // Handle initialization of the necessary firebase modules:

    private void Awake()
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
    void InitializeFirebase()
    {
       
        Debug.Log("Setting up Firebase Auth");
        User = FirebaseAuth.DefaultInstance.CurrentUser;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateUserPoints(10));
        //StartCoroutine(LoadUserMenu());
    }

    private IEnumerator UpdateUserPoints(int _points)
    {
        //Set the currently logged in User Username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("points").SetValueAsync(_points);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database Username is now updated           
            //UIManager.instance.LoginScreenFromCharacters();
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

                Debug.Log("masculino");

            }
            else if (snapshot.Child("gender").Value.ToString() == "Femenino")
            {
                Debug.Log("femenino");
            }

        }

    }


}
