using System;
using System.Reflection;

namespace NetVulkanoPruebasAutomatizadas_Back.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}