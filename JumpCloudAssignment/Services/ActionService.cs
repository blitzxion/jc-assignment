using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JumpCloudAssignment.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace JumpCloudAssignment.Services
{
    public class ActionService
    {
        private string[] _validActions = new string[2] { "jump", "run" };

        private const string _errorEmptyAction = @"Action cannot be empty.";
        private const string _errorInvalidAction = @"Action provided is invalid.";
        private const string _successAddedAction = ""; // Expected to be empty;

        private JsonSerializerSettings _jsonSettings
            = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error,
            };

        private ConcurrentBag<ActionModel> Actions { get; set; }
            = new ConcurrentBag<ActionModel>();

        /// <summary>
        /// Adds an action to the service.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>An empty result if successful, otherwise an error.</returns>
        public string AddAction(string action)
        {
            // If an action was provided, remove lead/training whitespace.
            action = action?.Trim();

            // Make sure there is an action at all.
            if (string.IsNullOrEmpty(action)) return _errorEmptyAction;

            // Validate the action, make sure the structure is right
            // Additional properties will be ignored.
            // Missing properties will cause an error.
            ActionModel actionObj = null;
            if (false == TryValidateActionJson(action, out actionObj))
                return _errorInvalidAction;

            // The action is good to go, lets add it to our queue
            Actions.Add(actionObj);

            // Return an empty string, this is expected for a successful out
            return _successAddedAction;
        }


        /// <summary>
        /// Generate a string representation of action stats.
        /// </summary>
        /// <returns>If actions are present, returns a string representation of unique action stats, otherwise empty string.</returns>
        public string GetStats()
        {
            // If we don't have any actions at all
            // return an empty string.
            if (null == Actions || Actions.Any() == false)
                return string.Empty;

            // Grab the actions, group them into a stats model with the average
            var stats = Actions
                .GroupBy(a => a.Action)
                .Select(a => new ActionStatsModel()
                {
                    Action = a.Key,
                    Avg = a.Average(x => x.Time)
                });

            // Return a serialized object of the stats
            return JsonConvert.SerializeObject(stats);
        }


        /// <summary>
        /// Validates and converts the string representation of an action to an ActionModel equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="actionJson">The action represented as a json string.</param>
        /// <param name="results">When the validation succeeds, this parameter will be filled with the results of parsing the json string, otherwise null if an error occured.</param>
        /// <returns>true if the json string was validated and converted successfully; otherwise false.</returns>
        private bool TryValidateActionJson(string actionJson, out ActionModel results)
        {
            // Default results will be a null value upon failure to parse.
            results = null;

            try
            {
                // Convert the string into the expected object
                // Deserialization settings dictate that if there are missing
                // properties to fail.
                var test = JsonConvert.DeserializeObject<ActionModel>(actionJson, _jsonSettings);

                // The object requires an action and that the action must be a expected type
                if (string.IsNullOrEmpty(test.Action) || false == _validActions.Contains(test.Action.ToLower()))
                    return false;

                // Time shouldn't be negative
                if (test.Time < 0)
                    return false;

                // Pass all validation
                results = test;
                return true;
            }
            catch
            {
                // Unable to parse the string, lets fail.
                return false;
            }
        }


    }
}
