
using System.Collections;
using WebBuySource.Dto.Response;

namespace WebBuySource.Utilities
{
    public class BaseApiResponse
    {
        /// <summary>
        /// Oks.
        /// </summary>
        /// <returns>Base api OK response data</returns>
        public static BaseAPIResponse OK()
        {
            return new BaseAPIResponse
            {
                Success = true,
                StatusCode = StatusCodes.Ok,
            };
        }

        /// <summary>
        /// Oks.
        /// </summary>
        /// <param name="pageSize">The page size data for mobile.</param>
        /// <returns>Base api OK response data</returns>
        public static BaseAPIResponse OK(int pageSize)
        {
            return new BaseAPIResponse
            {
                Success = true,
                StatusCode = StatusCodes.Ok,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Oks the specified resource data.
        /// </summary>
        /// <param name="resData">The resource data</param>
        /// <returns>Base api OK response data</returns>
        public static BaseAPIResponse OK(object resData)
        {
            int? totalRecord = null;
            if (resData is ICollection col)
            {
                totalRecord = col.Count;
            }
            return new BaseAPIResponse
            {
                Success = true,
                StatusCode = StatusCodes.Ok,
                Items = resData,
                Total = totalRecord
            };
        }

        /// <summary>
        /// Oks the specified resource data.
        /// </summary>
        /// <param name="resData">The resource data</param>
        /// <param name="totalRecord">The resource data</param>
        /// <returns>Base api OK response data</returns>
        public static BaseAPIResponse OK(object resData, int totalRecord)
        {
            return new BaseAPIResponse
            {
                Success = true,
                StatusCode = StatusCodes.Ok,
                Items = resData,
                Total = totalRecord
            };
        }

        /// <summary>
        /// Oks the specified resource data.
        /// </summary>
        /// <param name="resData">The resource data</param>
        /// <param name="totalRecord">The resource data</param>
        /// <param name="pageSize">The page size data for mobile.</param>
        /// <returns>Base api OK response data</returns>
        public static BaseAPIResponse OK(object resData, int? totalRecord, int pageSize)
        {
            return new BaseAPIResponse
            {
                Success = true,
                StatusCode = StatusCodes.Ok,
                Items = resData,
                Total = totalRecord,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Errors the specified resource data.
        /// </summary>
        /// <returns>Base api ERROR response data</returns>
        public static BaseAPIResponse Error()
        {
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.BadRequest
            };
        }

        /// <summary>
        /// Errors the specified resource data.
        /// </summary>
        /// <param name="msgCode">The MSG code.</param>
        /// <returns>Base api ERROR response data</returns>
        public static BaseAPIResponse Error(string msgCode)
        {
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.BadRequest,
                MessageCode = msgCode
            };
        }

        /// <summary>
        /// Errors the specified resource data.
        /// </summary>
        /// <param name="msgCode">The MSG code.</param>
        /// <param name="msgDetail">The MSG detail.</param>
        /// <param name="resData">The resource data.</param>
        /// <returns>Base api ERROR response data</returns>
        public static BaseAPIResponse Error(string msgCode, string msgDetail, object resData)
        {
            int? totalRecord = null;
            if (resData is ICollection c)
            {
                totalRecord = c.Count;
            }
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.BadRequest,
                MessageCode = msgCode,
                MessageDetail = msgDetail,
                Items = resData,
                Total = totalRecord
            };
        }

        /// <summary>
        /// NotFound the specified resource data.
        /// </summary>
        /// <returns>Base api NotFound response data</returns>
        public static BaseAPIResponse NotFound()
        {
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.NotFound
            };
        }

        /// <summary>
        /// NotFound the specified resource data.
        /// </summary>
        /// <param name="msgCode">The MSG code.</param>
        /// <returns>Base api NotFound response data</returns>
        public static BaseAPIResponse NotFound(string msgCode)
        {
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.NotFound,
                MessageCode = msgCode
            };
        }

        /// <summary>
        /// NotFound the specified resource data.
        /// </summary>
        /// <param name="msgCode">The MSG code.</param>
        /// <param name="msgDetail">The MSG detail.</param>
        /// <returns>Base api ERROR response data</returns>
        public static BaseAPIResponse NotFound(string msgCode, string msgDetail)
        {
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.NotFound,
                MessageCode = msgCode,
                MessageDetail = msgDetail
            };
        }

        /// <summary>
        /// Validated the specified resource data.
        /// </summary>
        /// <returns>Base api validated response data.</returns>
        public static BaseAPIResponse Validated()
        {
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.BadRequest,
            };
        }

        /// <summary>
        /// Validated the specified resource data.
        /// </summary>
        /// <param name="msgCode">The message data.</param>
        /// <returns>Base api validated response data.</returns>
        public static BaseAPIResponse Validated(string msgCode)
        {
            return new BaseAPIResponse
            {
                Success = false,
                StatusCode = StatusCodes.BadRequest,
                MessageCode = msgCode
            };
        }

        /// <summary>
        /// Validated the specified resource data.
        /// </summary>
        /// <param name="resData">The resource data.</param>
        /// <returns>Base api validated response data.</returns>
        public static BaseAPIResponse Warning(object resData)
        {
            return new BaseAPIResponse
            {
                Success = false,
                Items = resData,
                IsWarning = true
            };
        }

        /// <summary>
        /// Validated the specified resource data.
        /// </summary>
        /// <param name="resData">The resource data.</param>
        /// <returns>Base api validated response data.</returns>
        public static BaseAPIResponse Warning(params string[] msgCodes)
        {
            return new BaseAPIResponse
            {
                Success = false,
                MessageCode = string.Join(",", msgCodes),
                IsWarning = true
            };
        }

        /// <summary>
        /// Validated the specified resource data.
        /// </summary>
        /// <param name="resData">The resource data.</param>
        /// <returns>Base api validated response data.</returns>
        public static BaseAPIResponse Validated(object resData)
        {
            return new BaseAPIResponse
            {
                Success = false,
                Items = resData,
            };
        }
        /// <summary>
        /// Oks the specified resource data.
        /// </summary>
        /// <param name="resData">The resource data</param>
        /// <param name="totalRecord">The resource data</param>
        /// <param name="totalUnRead">The resource data un read</param>
        /// <returns>Base api OK response data</returns>
        public static BaseAPIResponse Notification(object resData, int totalRecord, int totalUnRead)
        {
            return new BaseAPIResponse
            {
                Success = true,
                StatusCode = StatusCodes.Ok,
                Items = resData,
                Total = totalRecord,
                TotalUnRead = totalUnRead
            };
        }

        public static BaseAPIResponse OK(object resData , string message )
        {
            return new BaseAPIResponse
            {
                Success = true,
                StatusCode = StatusCodes.Ok,
                MessageCode = message,
                Items = resData
            };
        }



    }
}
