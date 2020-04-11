using System;
using System.Collections.Generic;
using System.Text;

namespace Tool.CreateNewMicroservice.Renamer
{
    public class NamespaceRenamer
    {
        public bool IsNecessaryToRename { get; set; }
        public IEnumerable<string> ProjectFiles { get; set; }
        public IEnumerable<string> SolutionFiles { get; set; }
    }
}
