
using Azure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMSBlogMaster_BL.Domain.Common
{
    [ExcludeFromCodeCoverageAttribute]

    public class ValidationMessages
    {
        public static string DUPLICATE_RECORD = "Duplicate record cannot be added";
        public static string RECORD_ADDED_SUCESSFULLY = "Record added successfully";
        public static string RECORD_UPDATED_SUCESSFULLY = "Record modified successfully";
        public static string RECORD_NOT_FOUND = "Record not found";
        public static string RECORD_ALREADY_EXIST = "Record already exist";
        public static string DESCRIPTION_MANDATORY = "Description should not be empty";
        public static string CODE_MANDATORY = "Code should not be empty";
        public static string NO_DATA_FOUND = "No Data Found";
        public static string OBSOLETE = "obsolete";
        public static string ACTIVE = "active";
        public static string TRUE = "true";
        public static string FALSE = "false";
        public static string DESCRIPTION = "description";
        public static string DISPLAY = "display";
        public static string ISDELETED = "IsDeleted";
        public static string PAGE_NUMBER_CANNOT_BE_NEGATIVE = "Page Number Cannot be negative";
        public static string PAGINATION_ASCENDING = "asc";
        public static string PAGINATION_DESCENDING = "desc";



        public static string StringValidation(string userValue)
        {
            return !string.IsNullOrEmpty(userValue) ? userValue.Trim() : "";
        }

        public static string StringIgnoreCase(string userValue)
        {
            return StringValidation(userValue).ToLower();
        }
    }
}
