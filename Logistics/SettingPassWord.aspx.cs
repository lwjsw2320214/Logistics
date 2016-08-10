using common;
using Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Logistics
{
    public partial class SettingPassWord : System.Web.UI.Page
    {

        protected string username;

        ClientUser cu = new ClientUser();

        private MemberService memberService = new MemberService();
        protected CookiesUtil cookiesUtil = new CookiesUtil();

        protected void Page_Load(object sender, EventArgs e)
        {

            var icid = cookiesUtil.getCookie("GInfo_999_Vali");
            if (icid != 10)
            {

                Response.Write("该用户没有查看此页的权限！");
                Response.End();
            }


            if (Request.Form.Count > 0) {

                cu.username = Request.Form["username"];
                cu.password = Request.Form["password"];

                if (string.IsNullOrEmpty(cu.username) || string.IsNullOrEmpty(cu.password)) {

                    Response.Write("<script>alert('用户名或者密码不能为空！');history.back();</script>");
                    Response.End();
                }

                cu.password = Md5Basece64(cu.password); 
                int count = memberService.add(cu);
                if (count > 0) {
                    Response.Write("<script>alert('设置成功！');location.href='ClienPassWord.aspx';</script>");
                    Response.End();
                }
            }

            if (!IsPostBack) { 
            
                username=Request.QueryString["username"];
            }
        }

        /// <summary>
        /// 把字符串用MD5方式加密转换为Base64位字符串
        /// </summary>
        /// <param name="objec">要转换的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string Md5Basece64(string objec)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] MD5Source = System.Text.Encoding.UTF8.GetBytes(objec);
            byte[] MD5Out = MD5CSP.ComputeHash(MD5Source);
            return Convert.ToBase64String(MD5Out);
        }
    }
}