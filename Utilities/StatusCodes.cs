using System.Net;

namespace WebBuySource.Utilities
{
    public static class StatusCodes
    {
        /// <summary>
        /// Ok
        /// </summary>
        public const int Ok = (int)HttpStatusCode.OK;

        /// <summary>
        /// Not Found
        /// </summary>
        public const int NotFound = (int)HttpStatusCode.NotFound;

        /// <summary>
        /// Bad request
        /// </summary>
        public const int BadRequest = (int)HttpStatusCode.BadRequest;

        /// <summary>
        /// Unauthorized
        /// </summary>
        public const int Unauthorized = (int)HttpStatusCode.Unauthorized;

        /// <summary>
        /// Internal Server Error
        /// </summary>
        public const int InternalServerError = (int)HttpStatusCode.InternalServerError;

        /// <summary>
        /// Forbidden
        /// </summary>
        public const int Forbidden = (int)HttpStatusCode.Forbidden;
    }
}
