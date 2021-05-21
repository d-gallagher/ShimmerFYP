using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTestScript : MonoBehaviour
{
    string json = "{\"email\":\"testplayer @example.com\",\"firstName\":\"Test\",\"lastName\":\"Player\",\"phone\":\"5551234567\",\"age\":22,\"weight\":90.21,\"height\":1.64,\"teamKey\":null,\"teamID\":1,\"team\":null,\"trainingRecords\":null,\"createdAt\":\"2020-02-07T19:37:55.8066667\",\"modifiedAt\":\"2020-02-07T19:37:55.8066667\",\"createdBy\":null,\"modifiedBy\":\"Feb  7 2020  7:37PM\",\"id\":1}";
    string jsonArray = "[{\"email\":\"testplayer @example.com\",\"firstName\":\"Test\",\"lastName\":\"Player\",\"phone\":\"5551234567\",\"age\":22,\"weight\":90.21,\"height\":1.64,\"teamKey\":null,\"teamID\":1,\"team\":null,\"trainingRecords\":null,\"createdAt\":\"2020-02-07T19:37:55.8066667\",\"modifiedAt\":\"2020-02-07T19:37:55.8066667\",\"createdBy\":null,\"modifiedBy\":\"Feb  7 2020  7:37PM\",\"id\":1},{\"email\":\"playertwo @example.com\",\"firstName\":\"Two\",\"lastName\":\"Player Two\",\"phone\":\"5551234987\",\"age\":20,\"weight\":80,\"height\":1.24,\"teamKey\":null,\"teamID\":1,\"team\":null,\"trainingRecords\":null,\"createdAt\":\"0001-01-01T00:00:00\",\"modifiedAt\":\"0001-01-01T00:00:00\",\"createdBy\":null,\"modifiedBy\":null,\"id\":2},{\"email\":\"player3 @example.com\",\"firstName\":\"Test Three\",\"lastName\":\"Player\",\"phone\":\"98729873217\",\"age\":24,\"weight\":180,\"height\":1.7,\"teamKey\":null,\"teamID\":2,\"team\":null,\"trainingRecords\":null,\"createdAt\":\"0001-01-01T00:00:00\",\"modifiedAt\":\"0001-01-01T00:00:00\",\"createdBy\":null,\"modifiedBy\":null,\"id\":3}]";


    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        Debug.Log(jsonArray);
        //var results = JsonUtility.FromJson<UnityPlayerModel>(json);
        //var results = CustomJsonSerializer.FromJson<UnityPlayerModel>(json);
        var results = CustomJsonSerializer.FromJsonList<UnityPlayerModel>(jsonArray);

        Debug.Log("GOT: " + results.Count);

        foreach (var r in results)
        {
            Debug.Log(r);
        }

    }
}
