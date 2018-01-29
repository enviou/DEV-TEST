using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MuralDeRecados.App_Start.SessionManager
{
    class ResearchAutenticadoModelBinder : System.Web.Mvc.IModelBinder
    {
        public object GetValue(ControllerContext controllerContext)//, string modelName, Type modelType, ModelStateDictionary modelState)
        {
            var modelo = new ResearchAutenticadoBindModel();
            modelo.Usuario = SessionManager.ReturnSessionObject(SessionKeys.Usuario, controllerContext.HttpContext).ToString();
            return modelo;
        }

        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext", "controllerContext is null.");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext", "bindingContext is null.");

            return GetValue(controllerContext);
        }
    }
}