using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vvarscNET.Model.Result;

namespace vvarscNET.Web.Client.Helper
{
    public static class ControllerExtensions
    {
        public static ToastMessage AddToastMessage(this Controller controller, Result result)
        {
            ToastType type = ToastType.Info;
            switch (result.Status)
            {
                case System.Net.HttpStatusCode.OK:
                {
                    type = ToastType.Success;
                    break;
                }
                default:
                {
                    type = ToastType.Error;
                    break;
                }
            }

            Toastr toastr = controller.TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(type.ToString(), result.StatusDescription, type);
            controller.TempData["Toastr"] = toastr;
            return toastMessage;
        }
    }
}