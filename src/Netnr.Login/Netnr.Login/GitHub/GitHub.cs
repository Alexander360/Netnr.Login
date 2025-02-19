﻿using System.Collections.Generic;

namespace Netnr.Login
{
    public class GitHub
    {
        /// <summary>
        /// 请求授权地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string AuthorizeHref(GitHub_Authorize_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            return string.Concat(new string[] {
                GitHubConfig.API_Authorize,
                "?client_id=",
                entity.client_id,
                "&scope=",
                LoginBase.EncodeUri(entity.scope),
                "&state=",
                entity.state,
                "&redirect_uri=",
                LoginBase.EncodeUri(entity.redirect_uri)});
        }

        /// <summary>
        /// 获取 access token
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static GitHub_AccessToken_ResultEntity AccessToken(GitHub_AccessToken_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            string pars = LoginBase.EntityToPars(entity);

            var hwr = LoginBase.HttpTo.HWRequest(GitHubConfig.API_AccessToken, "POST", pars);
            hwr.Accept = "application/json";//application/xml
            string result = LoginBase.HttpTo.Url(hwr);

            var outmo = LoginBase.ResultOutput<GitHub_AccessToken_ResultEntity>(result);

            return outmo;
        }

        /// <summary>
        /// 获取 用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static GitHub_User_ResultEntity User(GitHub_User_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }
            
            string pars = LoginBase.EntityToPars(entity);

            var hwr = LoginBase.HttpTo.HWRequest(GitHubConfig.API_User + "?" + pars);
            hwr.UserAgent = entity.ApplicationName;
            string result = LoginBase.HttpTo.Url(hwr);

            var outmo = LoginBase.ResultOutput<GitHub_User_ResultEntity>(result, new List<string> { "plan" });

            return outmo;            
        }
    }
}
