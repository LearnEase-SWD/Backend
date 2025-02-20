using LearnEase.Core.Utils;

namespace LearnEase.Core.Enum
{
    public enum StatusCodeHelper
    {
        [ResponseName("Success")]
        OK = 200,

        [ResponseName("Bad Request")]
        BadRequest = 400,

        [ResponseName("Unauthorized")]
        Unauthorized = 401,

        [ResponseName("Internal Server Error")]
        ServerError = 500
    }
}