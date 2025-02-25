using Microsoft.AspNetCore.Mvc;
using SecureApiWithJwt.DTOs.Responses;

namespace SecureApiWithJwt.Configurations
{
    public class CustomValidationResponse
    {
        public static IActionResult GenerateResponse(ActionContext context)
        {
            var errors = context.ModelState
                .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                .ToDictionary(
                    e => e.Key,
                    e => e.Value?.Errors.Select(err => err.ErrorMessage).ToArray()
                );

            return new BadRequestObjectResult(ApiResponse<Dictionary<string, string[]>>.Error(errors));
        }
    }
}
