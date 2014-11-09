namespace VoiceWall.Web.Infrastructure.Filters
{
    using System.Reflection;
    using System.Web.Mvc;

    public class AjaxPostAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest() 
                && controllerContext.HttpContext.Request.HttpMethod == "POST";
        }
    }
}
