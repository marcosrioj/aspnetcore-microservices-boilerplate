using System.IO;
using Tool.CreateNewMicroservice.Extensions;

namespace Tool.CreateNewMicroservice.Renamer
{
    public class SharedTypeProjectRenamer : ProjectRenamer
    {
        public string ProjectItemsFileName => $"{this.ProjectName}.projitems";
        public string ProjectItemsFileNameNew => $"{this.ProjectNameNew}.projitems";

        public override void RenameAssemblyNameAndDefaultNamespace()
        {
        }

        public override void RenameAssemblyInformation()
        {
        }

        public override void CustomRenameForEachProjectType()
        {
            string projFileText = File.ReadAllText(ProjectFullNameNew);
            projFileText = projFileText.Replace(this.ProjectName, this.ProjectNameNew);
            FileManager.WriteAllText(ProjectFullNameNew, projFileText);

            var projectDirectory = ProjectFullNameNew.GetDirectoryName();
            var projItemsFilePath = Path.Combine(projectDirectory, ProjectItemsFileName);
            if (File.Exists(projItemsFilePath))
            {
                string assemblyInfoFileText = File.ReadAllText(projItemsFilePath);
                assemblyInfoFileText = assemblyInfoFileText
                    .ReplaceWithTag(ProjectName, ProjectNameNew, "Import_RootNamespace");
                FileManager.WriteAllText(projItemsFilePath, assemblyInfoFileText);
            }

            var fullNameWithRenamedFolder =
                Path.Combine(ProjectFullNameNew.GetDirectoryName(), ProjectItemsFileName);
            var fullNameWithRenamedFolderNew =
                Path.Combine(ProjectFullNameNew.GetDirectoryName(), ProjectItemsFileNameNew);
            FileManager.Move(fullNameWithRenamedFolder, fullNameWithRenamedFolderNew);
        }
    }
}