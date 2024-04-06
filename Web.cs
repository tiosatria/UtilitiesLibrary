using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace UtilitiesLibrary
{
    public class Web
    {
        public static async Task<T?> DecodeFormDataUrlEncodedAsync<T>(HttpContext ctx)
        {
            try
            {
                using var reader = new StreamReader(ctx.Request.Body);
                var requestBody = await reader.ReadToEndAsync();
                var formData = QueryHelpers.ParseQuery(requestBody);
                var obj = Activator.CreateInstance<T>();
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    if (!formData.TryGetValue(property.Name, out var stringValue))
                        continue;
                    var isNullable = Nullable.GetUnderlyingType(property.PropertyType) != null;
                    var conversionResult = TryConvert(stringValue, property.PropertyType, isNullable, out var convertedValue);
                    property.SetValue(obj, conversionResult ? convertedValue : GetDefaultValue(property.PropertyType));
                }
                return obj;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        #region Decode Helper

        private static bool TryConvert(string stringValue, Type targetType, bool isNullable, out object? result)
        {
            try
            {
                // Handle nullable types
                if (isNullable)
                {
                    if (string.IsNullOrEmpty(stringValue))
                    {
                        result = null;
                        return true;
                    }
                    // non-empty strings
                    result = Convert.ChangeType(stringValue, Nullable.GetUnderlyingType(targetType));
                    return true;
                }
                else
                {
                    // non-nullable types
                    result = Convert.ChangeType(stringValue, targetType);
                    return true;
                }
            }
            catch
            {
                result = null;
                return false;
            }
        }

        private static object? GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        #endregion

    }



}
