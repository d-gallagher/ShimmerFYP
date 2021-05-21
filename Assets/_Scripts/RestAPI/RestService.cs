using Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RestAPI
{
    public class RestService
    {
        #region GET
        /// <summary>
        /// Get all Players
        /// </summary>
        /// <param name="onSuccess">Callback if no errors.</param>
        /// <param name="onError">Callback if any errors.</param>
        /// <returns></returns>
        public static IEnumerator GetPlayers(Action<List<UnityPlayerModel>> onSuccess, Action<string> onError)
        {
            return GenericRestService.Get("/players", onSuccess, onError);
        }

        /// <summary>
        /// Get a list of all TrainingRecords related to this Player.
        /// </summary>
        /// <param name="playerId">Id of the Player.</param>
        /// <param name="onSuccess">Callback if no errors.</param>
        /// <param name="onError">Callback if any errors.</param>
        /// <returns></returns>
        public static IEnumerator GetPlayerTrainingRecords(int playerId, Action<List<UnityTrainingRecord>> onSuccess, Action<string> onError)
        {
            string node = $"/players/{playerId}/training-records";
            return GenericRestService.Get(node, onSuccess, onError);
        }

        /// <summary>
        /// Get a list of all TrainingRecords related to this Player.
        /// </summary>
        /// <param name="playerId">Id of the Player.</param>
        /// <param name="onSuccess">Callback if no errors.</param>
        /// <param name="onError">Callback if any errors.</param>
        /// <returns></returns>
        public static IEnumerator GetTrainingRecordData(int trainingRecordId, Action<List<UnityShimmerDataModel>> onSuccess, Action<string> onError)
        {
            string node = $"/training-records/{trainingRecordId}/data";
            return GenericRestService.Get(node, onSuccess, onError);
        }


        #endregion

        #region POST
        /// <summary>
        /// Creates a new TrainingRecord for the Player with playerId, adding the data in 'json' to the Training Record
        /// </summary>
        /// <param name="playerId">Id of the Player to create the TrainingRecord for.</param>
        /// <param name="json">A Json string representation of the ShimmerDataModels to add to the TrainingRecord</param>
        /// <param name="onSuccess">Callback if no errors.</param>
        /// <param name="onError">Callback if any errors.</param>
        /// <returns></returns>
        public static IEnumerator CreatePlayerTrainingRecord(int playerId, string json, Action<string> onSuccess, Action<string> onError)
        {
            string node = $"/players/{playerId}/training-records";
            return GenericRestService.PostJson(node, json, onSuccess, onError);
        }
        #endregion
    }
}
