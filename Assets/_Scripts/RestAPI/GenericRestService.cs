using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace RestAPI
{
    public static class GenericRestService
    {
        private static string _rootApiUrl = "https://blazor-sports.azurewebsites.net/api";
        //private static string _rootApiUrl = "https://localhost:44393/api";

        #region GET
        /// <summary>
        /// Generic GET method to return Objects from an API node.
        /// </summary>
        /// <typeparam name="T">The type of objects expected from this endpoint.</typeparam>
        /// <param name="node">Url extenstion to send request to, e.g. /players, /teams, etc</param>
        /// <param name="onSuccess">Callback if request AND deserialisation are successful.</param>
        /// <param name="onError">Callback if request OR deserialisation are NOT successful.</param>
        /// <returns></returns>
        public static IEnumerator Get<T>(string node, Action<List<T>> onSuccess, Action<string> onError)
        {
            // Create a default generic variable.
            List<T> result = default;
            using (UnityWebRequest req = UnityWebRequest.Get(_rootApiUrl + node))
            {
                // Send the request
                yield return req.SendWebRequest();

                // Handle request errors.
                if (req.isNetworkError || req.isHttpError)
                {
                    onError(req.error);
                }
                else
                {
                    if (req.isDone)
                    {
                        // Get the JSON string from the response.
                        string json = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                        try
                        {
                            // Deserialize Json.
                            //result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                            result = CustomJsonSerializer.FromJsonList<T>(json);
                            onSuccess(result);
                        }
                        catch (Exception e)
                        {
                            onError(e.Message);
                        }
                    }
                }
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// Generic POST method to post a Json string to the relevant API node.
        /// </summary>
        /// <param name="node">Url extenstion to send request to, e.g. /players, /teams, etc</param>
        /// <param name="json">Json String to upload</param>
        /// <param name="onSuccess">Callback if request succeeds.</param>
        /// <param name="onError">Callback if request fails for any reason.</param>
        /// <returns></returns>
        public static IEnumerator PostJson(string node, string json, Action<string> onSuccess, Action<string> onError)
        {
            using (UnityWebRequest req = UnityWebRequest.Post(_rootApiUrl + node, json))
            {
                // Convert the Json string to bytes and add to request body.
                byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
                req.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                // Set the content type.
                req.SetRequestHeader("Content-Type", "application/json");
                // Send the request.
                yield return req.SendWebRequest();

                // Handle request errors.
                if (req.isNetworkError || req.isHttpError)
                {
                    onError(req.error);
                }
                else
                {
                    if (req.isDone)
                    {
                        onSuccess("Completed JSON upload");
                    }
                }
            }
        }
        #endregion

    }
}