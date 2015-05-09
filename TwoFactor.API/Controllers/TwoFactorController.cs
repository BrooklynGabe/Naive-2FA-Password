using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwoFactor.API.Models;
using TwoFactor.API.Models.ViewModels;

namespace TwoFactor.API.Controllers
{
    public class TwoFactorController : ApiController
    {
        public TwoFactorController()
        {
            _service = new TwoFactorService();
        }

        public HttpResponseMessage Get(string UserLoginName)
        {
            if(string.IsNullOrWhiteSpace(UserLoginName))
                return Request.CreateResponse(HttpStatusCode.NotImplemented);

            return Request.CreateResponse(HttpStatusCode.OK, _service.DoesSecretExistForUser(UserLoginName.Trim()));
        }

        public HttpResponseMessage Put([FromBody]TwoFactorPut model)
        {
            if (!string.IsNullOrWhiteSpace(model.UserLoginName) && !string.IsNullOrWhiteSpace(model.OneTimePassword))
                return Request.CreateResponse(HttpStatusCode.OK, _service.VerifyPasswordForUser(model.UserLoginName.Trim(), model.OneTimePassword.Trim()));

            return Request.CreateResponse(HttpStatusCode.OK, false);
        }

        public HttpResponseMessage Post([FromBody] TwoFactorPost model)
        {
            if (!string.IsNullOrWhiteSpace(model.UserLoginName))
            {
                _service.SetSecretForUser(model.UserLoginName.Trim());
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        public HttpResponseMessage Delete([FromBody]TwoFactorDelete model)
        {
            if (!string.IsNullOrWhiteSpace(model.UserLoginName))
            {
                _service.DeleteSecretForUser(model.UserLoginName.Trim());
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        private TwoFactorService _service;
    }
}
