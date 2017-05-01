namespace vvarscNET.Model.Enums
{
    /// <summary>
    /// The StatusEnum used for return the status of a request
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum StatusEnum
    {
        undefined = 0,
        /// <summary>
        /// Ok meaning the request was processed
        /// </summary>
        OK = 200,

        /// <summary>
        /// Bad Request meaning the requested action cannot be performed
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Unauthorized meaning the request is not an authorized action for the user
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Not found meaning the object requested was not found
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Method Not Allowed meaning the requested method is not allowed to be performed
        /// </summary>
        MethodNotAllowed = 405,

        /// <summary>
        /// Failure meaning the individual object/objects failed to process
        /// </summary>
        Failure = 409,

        /// <summary>
        /// Internal Server Error meaning there was an error that should be logged in the logging
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// Not Implemented meaning the requested method has not been implemented
        /// </summary>
        NotImplemented = 501,
    }
}
