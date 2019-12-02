using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreNotes.AutoFac.Common.Helper;
using CoreNotes.AutoFac.CoreApi.AuthHelper.Policy;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly PermissionRequirement _requirement;
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="requirement"></param>
        public LoginController(IUserService userService, PermissionRequirement requirement)
        {
            _userService = userService;
            _requirement = requirement;
        }

        [HttpPost]
        public async Task<object> GetJwtToken(string username = "", string password = "")
        {
            string jwtStr = string.Empty;
            var data = new MessageModel<object>();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                data.Success = false;
                data.Msg = "用户名或密码不能为空";
                return data;
            }

            password = Md5Helper.Md5Encrypt32(password);

            var user = await _userService.Query(d => d.LoginName == username && d.Pwd == password).ConfigureAwait(false);
            if (user.Count > 0)
            {
                var userRoles = await _userService.GetUserRoleNameStr(username, password).ConfigureAwait(false);
                // 如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(_requirement.Expiration.TotalMinutes).ToString())
                };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                // 用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                data.Success = true;
                data.Response = token;
                data.Msg = "登录成功！";
                return data;
            }

            data.Success = false;
            data.Msg = "登录失败！";
            return data;
        }

        [HttpGet]
        public MessageModel<string> LoginOut()
        {
            var data = new MessageModel<string>
            {
                Msg = "退出成功！",
                Success = true
            };
            return data;
        }
    }
}