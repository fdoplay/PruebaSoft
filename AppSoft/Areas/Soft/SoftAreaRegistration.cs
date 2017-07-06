using System.Web.Mvc;

namespace AppSoft.Areas.Soft
{
    public class SoftAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Soft";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Soft_default",
                "Soft/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}