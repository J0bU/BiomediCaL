using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

 //Tutorial
 [Header("Tutorial")]
 public GameObject tutorialFirstStep;
 public GameObject tutorialSecondStep;
 public GameObject tutorialThirdStep;
 public GameObject tutorialFourthStep;
 public GameObject tutorialFifthStep;
 public GameObject tutorialSixthStep;
 public GameObject tutorialSeventhStep;


 //Achievements
 [Header("Achievements")]
 public GameObject pinkAchievement1;
 public GameObject pinkAchievement2;
 public GameObject pinkAchievement3;
 public GameObject pinkAchievement4;
 public GameObject pinkAchievement5;
 public GameObject pinkAchievement6;
 public GameObject pinkAchievement7;
 public GameObject greenAchievement1;
 public GameObject greenAchievement2;
 public GameObject greenAchievement3;
 public GameObject greenAchievement4;
 public GameObject greenAchievement5;
 public GameObject greenAchievement6;
 public GameObject greenAchievement7;


 /*
* @name StartTutorial
* @function: Move the user from step 1 to step 2
* Use: yes - 18
*/

 public void StartTutorial()
 {
  tutorialFirstStep.SetActive(false);
  tutorialSecondStep.SetActive(true);
 }

 /*
* @name SecondStep
* @function: Move the user from step 2 to step 3
* Use: yes - 19
*/
 public void SecondStep()
 {
  tutorialSecondStep.SetActive(false);
  tutorialThirdStep.SetActive(true);
 }

 /*
* @name ThirdStep
* @function: Move the user from step 3 to step 4
* Use: yes - 20
*/
 public void ThirdStep()
 {
  tutorialThirdStep.SetActive(false);
  tutorialFourthStep.SetActive(true);
 }

 /*
* @name FourthStep
* @function: Move the user from step 3 to step 4
* Use: yes - 21
*/

 public void FourthStep()
 {
  tutorialFourthStep.SetActive(false);
  tutorialFifthStep.SetActive(true);
 }

 /*
* @name FifthStep
* @function: Move the user from step 4 to step 5
* Use: yes - 22
*/

 public void FifthStep()
 {
  tutorialFifthStep.SetActive(false);
  tutorialSixthStep.SetActive(true);
 }


 /*
* @name SixthStep
* @function: Move the user from step 5 to step 6
* Use: yes - 23
*/

 public void SixthStep()
 {
  tutorialSixthStep.SetActive(true);
  tutorialSeventhStep.SetActive(true);
 }
}
