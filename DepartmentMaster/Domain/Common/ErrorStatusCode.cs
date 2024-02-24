using System.Diagnostics.CodeAnalysis;

namespace CMSBlogMaster_BL.Domain.Common
{
  [ExcludeFromCodeCoverageAttribute]

  public class ErrorStatusCode
  {
    public static string STATUS_SUCCESS = "200";
    public static string STATUS_BadRequest = "400";
    public static string STATUS_Forbidden = "403";
    public static string STATUS_NotFound = "404";
    public static string STATUS_Unauthorized = "401";
  }
}
