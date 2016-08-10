using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Service;

namespace common
{
    public class CookiesUtil
    {
        private MemberService memberService = new MemberService();
        public int getCookie(string cookieName) { 
        
            string cookieValue="";
            int userId = 0;
            try
            {
                if (HttpContext.Current.Request.Cookies[cookieName] != null)
                {
                    cookieValue = HttpContext.Current.Request.Cookies[cookieName].Value;
                }
                if (!"".Equals(cookieValue))
                {
                    var userArray = cookieValue.Split('|');
                    userId = memberService.GetMembericid(userArray[0]);
                }
                else
                {

                    HttpContext.Current.Response.Redirect("http://www.au-express.com/");
                    HttpContext.Current.Response.End();
                }
            }
            catch { 
            
            }
            return userId;
        }
         
    public string getCookieUserName(string cookieName) { 
        
            string cookieValue="";
            string userName ="";
            try
            {
                if (HttpContext.Current.Request.Cookies[cookieName] != null)
                {
                    cookieValue = HttpContext.Current.Request.Cookies[cookieName].Value;
                }
                if (!"".Equals(cookieValue))
                {
                    var userArray = cookieValue.Split('|');
                    userName = userArray[0];
                }
                else
                {

                    HttpContext.Current.Response.Redirect("http://www.au-express.com/");
                    HttpContext.Current.Response.End();
                }
            }
            catch { 
            
            }
            return userName;
        }

    }
}
