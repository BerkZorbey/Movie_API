using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Movie_API.Models.DTOs;
using Movie_API.Services;

namespace Movie_API.Filter
{
    
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _validationService = context.HttpContext.RequestServices.GetService<ValidationService>();

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestResult();    
            }
            else
            {
                var user = context.ActionArguments["user"];
                var checkUserName = _validationService.IsUserNameValid((RegisterDTO)user);
                var checkEmail = _validationService.IsEmailValid((RegisterDTO)user);
                var checkPassword = _validationService.IsPasswordValid((RegisterDTO)user);
                var checkConditions = _validationService.IsConditionsValid(checkEmail, checkUserName, checkPassword);
                if(checkConditions != true)
                {
                    context.Result = new BadRequestResult();
                }
                
            }
               
        }
        
    }
}
