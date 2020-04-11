using System.Collections.Generic;
using Tool.CreateNewMicroservice.Renamer;

namespace Tool.CreateNewMicroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectRenamer = new ProjectRenamer()
            {
                SolutionFullName = @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\All.sln",
                ProjectFullName = @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct\test\MainProduct.Tests\MainProduct.Tests.csproj",
                ProjectName = "MainProduct",
                ProjectUniqueName = @"MainProduct\MainProduct.csproj",
                ProjectNameNew = "MicroserviceBaseProject",
                SolutionProjects = new List<string>
                {
                    @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct\src\MainProduct.Application\MainProduct.Application.csproj",
                    @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct\src\MainProduct.Core\MainProduct.Core.csproj",
                    @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct\src\MainProduct.EntityFrameworkCore\MainProduct.EntityFrameworkCore.csproj",
                    @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct\src\MainProduct.Migrator\MainProduct.Migrator.csproj",
                    @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct\src\MainProduct.Web.Core\MainProduct.Web.Core.csproj",
                    @"C:\work\marcoslima\aspnetcore-microservices-boilerplate\backend\MainProduct\src\MainProduct.Web.Host\MainProduct.Web.Host.csproj",
                }
            };

            projectRenamer.FullRename();
        }
    }
}
