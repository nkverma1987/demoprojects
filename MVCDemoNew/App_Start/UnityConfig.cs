using MVCDemoNew.DI;
using MVCDemoNew.Entity;
using MVCDemoNew.Models;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MVCDemoNew
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IStudent, StudentClass>();
            container.RegisterType<IApplicant, Applicant>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}