using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAnswer : MonoBehaviour
{
    [SerializeField] private GameObject mainPlatform;
    [SerializeField] private string doorNumber;
    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        if (gameObject.GetComponent<DoorState>().getAnswerState())
        {
            ScoreController.instance.IncreaseScore();

            if (mainPlatform.GetComponent<QuestionHandler>().lastPlatform == true)
            {
                ScoreController.instance.endGame();
            }
            else
            {
                LoadLevel.instance.SpawnNextPlatform(mainPlatform, doorNumber);
            }
        }
        else
        {
            Debug.Log("Incorrect!");
            if (mainPlatform.GetComponent<QuestionHandler>().lastPlatform == true)
            {
                ScoreController.instance.endGame();
            }
            else
            { 
                LoadLevel.instance.SpawnNextPlatform(mainPlatform, doorNumber); 
            }
        }
        StartCoroutine(MoveDoor());
        LoadLevel.instance.ClearPlatforms();
    }

    private IEnumerator MoveDoor()
    {
        GameObject transitionDoor = null;
        switch (doorNumber)
        {
            case "1":
                transitionDoor = mainPlatform.GetComponent<QuestionHandler>().doorTransition1;
                break;
            case "2":
                transitionDoor = mainPlatform.GetComponent<QuestionHandler>().doorTransition2;
                break;
            case "3":
                transitionDoor = mainPlatform.GetComponent<QuestionHandler>().doorTransition3;
                break;
            case "4":
                transitionDoor = mainPlatform.GetComponent<QuestionHandler>().doorTransition4;
                break;

        }
        Vector3 startPosition = transitionDoor.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y - 2, startPosition.z);
        float duration = 3f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transitionDoor.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Za osiguravanje da vrata tačno završe na krajnjoj poziciji
        transitionDoor.transform.position = endPosition;
    }
}
