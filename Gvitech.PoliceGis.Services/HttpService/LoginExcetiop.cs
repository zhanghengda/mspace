using System;

namespace Mmc.Mspace.Services.HttpService
{
    public class LoginExcetiop : Exception
    {
        public delegate void ExcetionDelegate();

        public static event ExcetionDelegate Logout;

        public LoginExcetiop(string message) : base(message)
        {
            switch (message)
            {
                case "1011":
                    Logout();
                    break;

                default:
                    break;
            }
        }

        private void ReLogin()
        {
        }
    }
}