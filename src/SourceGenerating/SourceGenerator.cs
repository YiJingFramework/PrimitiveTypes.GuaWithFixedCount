using Microsoft.CodeAnalysis;
using System.IO;
using System.Text;

namespace SourceGenerating
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        private readonly string template;

        public SourceGenerator()
        {
            var templateProjectPath = TemplatePath.Path;
            // If cannot find 'TemplatePath':
            // 1. Add a TemplatePath.cs file (this file will be ignored by .gitignore)
            // 2. Copy the codes from TemplatePath.txt
            // 3. Set TemplatePath.Path as the path of TemplateForSourceGeneration project

            this.template = File.ReadAllText(Path.Combine(templateProjectPath, "GuaWith999Lines.cs"));
        }

        private string GenerateCode(int lineCount, string className)
        {
            var templateClassName = $"GuaWith{lineCount}Lines";
            if (className == null)
                className = templateClassName;

            var builder = new StringBuilder(this.template);
            _ = builder.Replace("999", lineCount.ToString());
            _ = builder.Replace(templateClassName, className);

            _ = builder.AppendLine($"partial class {className}");
            _ = builder.AppendLine("{");

            _ = builder.AppendLine(@"    /// <summary>
    /// 创建新实例。
    /// Initializes a new instance.
    /// </summary>");

            for (int i = 0; i < lineCount; i++)
            {
                _ = builder.AppendLine($@"    /// <param name=""line{i}"">
    /// 第 <c>{i}</c> 爻。
    /// Line <c>{i}</c>.
    /// </param>");
            }

            _ = builder.Append($"    public {className}(");
            for (int i = 0; i < lineCount; i++)
            {
                _ = builder.Append($@"Yinyang line{i}, ");
            }
            if (lineCount != 0)
                _ = builder.Remove(builder.Length - 2, 2);

            _ = builder.AppendLine(")");
            _ = builder.Append(@"    {
        this.innerGua = new Gua(");
            for (int i = 0; i < lineCount; i++)
            {
                _ = builder.Append($@"line{i}, ");
            }
            if (lineCount != 0)
                _ = builder.Remove(builder.Length - 2, 2);

            _ = builder.AppendLine(");");
            _ = builder.AppendLine("    }");
            _ = builder.AppendLine("}");
            return builder.ToString();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource($"GuaEmpty.g.cs", this.GenerateCode(0, "GuaEmpty"));
            context.AddSource($"GuaWith1Line.g.cs", this.GenerateCode(1, "GuaWith1Line"));
            context.AddSource($"GuaWith2Lines.g.cs", this.GenerateCode(2, null));
            context.AddSource($"GuaTrigram.g.cs", this.GenerateCode(3, "GuaTrigram"));
            context.AddSource($"GuaWith4Lines.g.cs", this.GenerateCode(4, null));
            context.AddSource($"GuaWith5Lines.g.cs", this.GenerateCode(5, null));
            context.AddSource($"GuaHexagram.g.cs", this.GenerateCode(6, "GuaHexagram"));
            context.AddSource($"GuaWith7Lines.g.cs", this.GenerateCode(7, null));
            context.AddSource($"GuaWith8Lines.g.cs", this.GenerateCode(8, null));
            context.AddSource($"GuaWith9Lines.g.cs", this.GenerateCode(9, null));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
