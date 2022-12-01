using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class WebHandler : MonoBehaviour
{
    public readonly string BaseURL = "https://gdapdev-web-api.herokuapp.com/api/";

    private void Start()
    {
        //CreateGroup();
    }

    private void CreateGroup()
    {
        StartCoroutine(PostCreateGroupRequest());
    }

    IEnumerator PostCreateGroupRequest()
    {
        Dictionary<string, string> groupParams = new Dictionary<string, string>();

        //TODO: add in actual group parameters
        groupParams.Add("group_num", "9999");
        groupParams.Add("group_name", "TEST GROUP");
        groupParams.Add("game_name", "TEST GAME");
        groupParams.Add("secret", "pass");

        //turns the dictionary into a JSON string
        string requestString = JsonConvert.SerializeObject(groupParams);
        //convert string into bytes
        byte[] requestData = Encoding.UTF8.GetBytes(requestString);

        //create POST request directed to /groups route
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "groups", "POST"))
        {
            //send what data type is in the request
            request.SetRequestHeader("Content-Type", "application/JSON");
            //add request data
            request.uploadHandler = new UploadHandlerRaw(requestData);
            //create reciever for response
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            Debug.Log($"response code: {request.responseCode}");

            //check if no error
            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"message: {request.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"error: {request.error}");
            }
        }
    }
}
