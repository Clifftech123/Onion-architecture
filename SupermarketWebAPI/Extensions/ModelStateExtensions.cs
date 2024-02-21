using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SupermarketWebAPI.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
            => dictionary
            .SelectMany(m => m.Value!.Errors)
            .Select(m => m.ErrorMessage)
            .ToList();
    }
}
