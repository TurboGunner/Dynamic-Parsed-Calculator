using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculatorOnRoids
{
    public class Parser
    {
        public string input { get; set; }
        public float output { get; set; }

        List<float> list;

        public Parser(string inp)
        {
            input = inp;
            output = 1f;
            list = Form1.values;
        }

        public async Task<float> Execute(string code)
        {
            Task<object> task = CSharpScript.EvaluateAsync(code, ScriptOptions.Default.WithImports(new string[] { "System" }));
            return (float) (await task);
        }

        public async Task<float> RunCode()
        {
            string code = "";
            for(int i = 0; i < Form1.values.Count; i++)
            {
                code += "float value" + i + " = " + list[i] + "f; ";
            }
            code += "(float) " + input;
            return await Execute(code);
        }
    }
}