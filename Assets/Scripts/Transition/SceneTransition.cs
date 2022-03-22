using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    [Header("Transition")]
    public GameObject finalHeart;
    public GameObject lastStep;

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(LoadScene());
        //}

    }
    IEnumerator LoadScene(){
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other){
        switch (other.tag){
            case "LastStep":
                Debug.Log("LastStep");
                Destroy(finalHeart.gameObject);
                Destroy(other.gameObject);
                StartCoroutine(LoadScene());
                break;
        }
    }
}
